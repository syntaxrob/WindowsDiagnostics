using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using WindowsSystemInfo.Models;
using Microsoft.Win32;

namespace WindowsSystemInfo
{
    public class SystemInfo
    {
        public OS GetOperatingSystemInfo()
        {
            OS os = new OS();
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    os.Name = "  Operating System Name: " + managementObject["Caption"];
                }
                else
                {
                    os.Name = "  No OS name available";
                }

                if (managementObject["OSArchitecture"] != null)
                {
                    os.Architecture = "  Operating System Architecture: " + managementObject["OSArchitecture"];
                }
                else
                {
                    os.Architecture = "  No Architecture name available";
                }
                if (managementObject["CSDVersion"] != null)
                {
                    os.Version = "  Operating System Service Pack: " + managementObject["CSDVersion"];
                }
                else
                {
                    os.Version = " No OS version available";
                }
            }
            return os;
        }

        public string GetProcessorInfo()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    return ("  " + processor_name.GetValue("ProcessorNameString"));
                }
            }

            return "  Processor name not available";
        }

        public string GetLocalMachineName()
        {
            return ("  " + Environment.MachineName);
        }

        public CurrentUser GetCurrentUserInfo()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string authenticationType = System.Security.Principal.WindowsIdentity.GetCurrent().AuthenticationType;
            bool isGuestUser = System.Security.Principal.WindowsIdentity.GetCurrent().IsGuest;

            CurrentUser currentUser = new CurrentUser
            {
                Username = userName,
                AuthType = authenticationType,
                IsGuest = isGuestUser.ToString()
            };

            return currentUser;
        }

        public List<Drive> GetDriveInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            List<Drive> listOfDrives = new List<Drive>();

            foreach (var drive in allDrives)
            {
                if (drive.IsReady)
                {
                    Drive driveInfo = new Drive
                    {
                        Name = drive.Name,
                        Label = drive.VolumeLabel,
                        Format = drive.DriveFormat,
                        Type = drive.DriveType.ToString(),
                        FreeSpace = Calculators.DiskSpaceBytesCalc(drive.TotalFreeSpace, false)
                    };

                    listOfDrives.Add(driveInfo);
                }

            }

            return listOfDrives;
        }

        public List<Ram> GetRamInfo()
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            List<Ram> rams = new List<Ram>();

            foreach (ManagementObject result in results)
            {
                Ram ram = new Ram
                {
                    TotalVisable =
                        Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["TotalVisibleMemorySize"]), true),
                    FreeVisable = Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["FreePhysicalMemory"]), true),
                    TotalVirtual =
                        Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["TotalVirtualMemorySize"]), true),
                    freeVirtual = Calculators.DiskSpaceBytesCalc(Convert.ToDecimal(result["FreeVirtualMemory"]), true)
                };
                rams.Add(ram);
            };

            return rams;
        }
    }
}
