using System;
using System.IO;
using System.Management;
using Microsoft.Win32;

namespace WindowsSystemInfo
{
    public class SystemInfo
    {
        public void GetOperatingSystemInfo()
        {
            Console.WriteLine("Operating System Info");
            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    Console.WriteLine("  Operating System Name: " + managementObject["Caption"].ToString());   //Display operating system caption
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    Console.WriteLine("  Operating System Architecture: " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
                }
                if (managementObject["CSDVersion"] != null)
                {
                    Console.WriteLine("  Operating System Service Pack: " + managementObject["CSDVersion"].ToString());     //Display operating system version.
                }
            }
        }

        public void GetProcessorInfo()
        {
            Console.WriteLine("\nProcessor Info");
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    Console.WriteLine("  " + processor_name.GetValue("ProcessorNameString"));
                }
            }
        }

        public void GetLocalMachineName()
        {
            Console.WriteLine("\nLocal Machine Name");
            Console.WriteLine("  " + Environment.MachineName);
        }

        public void GetCurrentUserInfo()
        {
            Console.WriteLine("\nCurrent User Info");

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Console.WriteLine("  Username: " + userName);

            string authenticationType = System.Security.Principal.WindowsIdentity.GetCurrent().AuthenticationType;
            Console.WriteLine("  Authentication type: " + authenticationType);

            var isGuestUser = System.Security.Principal.WindowsIdentity.GetCurrent().IsGuest;
            Console.WriteLine("  Guest user: " + isGuestUser.ToString());
        }

        public void GetDriveInfo()
        {
            Console.WriteLine("\nDisk Drive Info");

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (var drive in allDrives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine("Drive {0}", drive.Name);
                    Console.WriteLine("  Volume label: {0}", drive.VolumeLabel);
                    Console.WriteLine("  Format: {0}", drive.DriveFormat);
                    Console.WriteLine("  Type: {0}", drive.DriveType);
                    Console.WriteLine("  Total free space: {0}", Calculators.DiskSpaceBytesCalc(drive.TotalFreeSpace, false));
                }
            }
        }

        public void GetRamInfo()
        {
            Console.WriteLine("\nRAM Info");

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            { 
                var totalVisableMemory = Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["TotalVisibleMemorySize"]),true);
                var freePhysicalMemory = Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["FreePhysicalMemory"]),true);
                var totalVirtualMemorySize = Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["TotalVirtualMemorySize"]),true);
                var freeVirtualMemory = Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["FreeVirtualMemory"]),true);


                Console.WriteLine("  Total Visible Memory: {0}", totalVisableMemory);
                Console.WriteLine("  Free Physical Memory: {0}", freePhysicalMemory);
                Console.WriteLine("  Total Virtual Memory: {0}", totalVirtualMemorySize);
                Console.WriteLine("  Free Virtual Memory: {0}", freeVirtualMemory);
            }
        }

    }
}
