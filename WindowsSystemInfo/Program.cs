using System;
using System.Collections.Generic;
using System.IO;
using WindowsSystemInfo.Models;

namespace WindowsSystemInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemInfo sysInfo = new SystemInfo();
            
            //Operating System Info
            var osInfo = sysInfo.GetOperatingSystemInfo();
            Console.WriteLine("Operating System Info");
            Console.WriteLine("  Operating System Name: " + osInfo.Name);
            Console.WriteLine("  Operating System Architecture: " + osInfo.Architecture);
            Console.WriteLine("  Operating System Version: " + osInfo.Version);

            //Processor Info
            Console.WriteLine("\nProcessor Info");
            var processorInfo = sysInfo.GetProcessorInfo();
            Console.WriteLine("  " + processorInfo);

            //Local Machine Info
            Console.WriteLine("\nLocal Machine Name"); 
            var localMachineName = sysInfo.GetLocalMachineName();
            Console.WriteLine("  " + localMachineName);

            //Current User Info
            Console.WriteLine("\nCurrent User Info");
            var currentUserInfo = sysInfo.GetCurrentUserInfo();
            Console.WriteLine("  Username: " + currentUserInfo.Username);
            Console.WriteLine("  Authentication type: " + currentUserInfo.AuthType);
            Console.WriteLine("  Guest user: " + currentUserInfo.IsGuest);

            //Disk Drive Info
            Console.WriteLine("\nDisk Drive Info");
            var driveListInfo = sysInfo.GetDriveInfo();
            foreach (var drive in driveListInfo)
            {
                Console.WriteLine("Drive {0}", drive.Name);
                Console.WriteLine("  Volume label: {0}", drive.Label);
                Console.WriteLine("  Format: {0}", drive.Format);
                Console.WriteLine("  Type: {0}", drive.Type);
                Console.WriteLine("  Total free space: {0}", drive.FreeSpace);
            }

            //RAM Info
            Console.WriteLine("\nRAM Info");
            var ramInfo = sysInfo.GetRamInfo();
            foreach (var ram in ramInfo)
            {
                Console.WriteLine("  Total Visable: {0}", ram.TotalVisable);
                Console.WriteLine("  Free Physical: {0}", ram.FreeVisable);
                Console.WriteLine("  Total Virtual: {0}", ram.TotalVirtual);
                Console.WriteLine("  Free Virtual: {0}", ram.freeVirtual);
            }
            
            Console.WriteLine("\nDiagnostics run at " + DateTime.Now.ToShortTimeString() + " on " + DateTime.Now.ToShortDateString());
            
            Console.WriteLine("Would you like to save these results? Y/N");
            var saveStatus = Console.ReadLine();

            if (saveStatus.ToLower() == "y")
            {
                string pathString = FileName();
                if (!File.Exists(pathString))
                {
                    using (FileStream fileStream = File.Create(pathString))
                    {
                        StreamWriter streamWriter = new StreamWriter(fileStream);
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("Operating System Info");
                        streamWriter.WriteLine("{0}", osInfo.Name);
                        streamWriter.WriteLine("{0}", osInfo.Architecture);
                        streamWriter.WriteLine("{0}", osInfo.Version);
                        streamWriter.WriteLine("\nProcessor Info");
                        streamWriter.WriteLine("{0}", processorInfo);
                        streamWriter.WriteLine("\nLocal Machine Name");
                        streamWriter.WriteLine("{0}", localMachineName);
                        streamWriter.WriteLine("\nCurrent User Info");
                        streamWriter.WriteLine("  Username: {0}", currentUserInfo.Username);
                        streamWriter.WriteLine("  Authentication type: {0}", currentUserInfo.AuthType);
                        streamWriter.WriteLine("  Is guest: {0}", currentUserInfo.IsGuest);
                        streamWriter.WriteLine("\nDisk Drive Info");
                        foreach (var item in driveListInfo)
                        {
                            streamWriter.WriteLine("  {0}", item.Name);
                            streamWriter.WriteLine("  {0}", item.Label);
                            streamWriter.WriteLine("  {0}", item.Format);
                            streamWriter.WriteLine("  {0}", item.Type);
                            streamWriter.WriteLine("  {0}", item.FreeSpace);
                        }
                        streamWriter.WriteLine("\nRAM Info");
                        foreach (var item in ramInfo)
                        {
                            streamWriter.WriteLine("  {0}", item.TotalVisable);
                            streamWriter.WriteLine("  {0}", item.FreeVisable);
                            streamWriter.WriteLine("  {0}", item.TotalVirtual);
                            streamWriter.WriteLine("  {0}", item.freeVirtual);
                        }
                        streamWriter.WriteLine("\nDiagnostics run and saved at " + DateTime.Now.ToShortTimeString() + " on " + DateTime.Now.ToShortDateString());

                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream(pathString, FileMode.Append))
                    {
                        StreamWriter streamWriter = new StreamWriter(fileStream);
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("Operating System Info");
                        streamWriter.WriteLine("{0}", osInfo.Name);
                        streamWriter.WriteLine("{0}", osInfo.Architecture);
                        streamWriter.WriteLine("{0}", osInfo.Version);
                        streamWriter.WriteLine("Processor Info");
                        streamWriter.WriteLine("{0}", processorInfo);
                        streamWriter.WriteLine("Local Machine Name");
                        streamWriter.WriteLine("{0}", localMachineName);
                        streamWriter.WriteLine("Current User Info");
                        streamWriter.WriteLine("  {0}", currentUserInfo.Username);
                        streamWriter.WriteLine("  {0}", currentUserInfo.AuthType);
                        streamWriter.WriteLine("  {0}", currentUserInfo.IsGuest);
                        streamWriter.WriteLine("Disk Drive Info");
                        foreach (var item in driveListInfo)
                        {
                            streamWriter.WriteLine("{0}", item.Name);
                            streamWriter.WriteLine("{0}", item.Label);
                            streamWriter.WriteLine("{0}", item.Format);
                            streamWriter.WriteLine("{0}", item.Type);
                            streamWriter.WriteLine("{0}", item.FreeSpace);
                        }
                        streamWriter.WriteLine("\nRAM Info");
                        foreach (var item in ramInfo)
                        {
                            streamWriter.WriteLine("{0}", item.TotalVisable);
                            streamWriter.WriteLine("{0}", item.FreeVisable);
                            streamWriter.WriteLine("{0}", item.TotalVirtual);
                            streamWriter.WriteLine("{0}", item.freeVirtual);
                        }
                        streamWriter.WriteLine("\nDiagnostics run and saved at " + DateTime.Now.ToShortTimeString() + " on " + DateTime.Now.ToShortDateString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Ok, this information will be discarded");
            }

            Console.ReadLine();
        }

        private static string FileName()
        {
            string folderName = @"c:\SpeedTests";
            var today = DateTime.Now.Date.ToShortDateString();
            var todayFormatted = today.Replace("/", "_");
            string pathString = Path.Combine(folderName, todayFormatted);
            System.IO.Directory.CreateDirectory(pathString);
            var timeNow = DateTime.Now.ToLongTimeString().Replace(":", "_");
            string fileName = timeNow + ".txt";
            pathString = System.IO.Path.Combine(pathString, fileName);
            return pathString;
        }
    }
}