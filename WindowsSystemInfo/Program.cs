using System;

namespace WindowsSystemInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemInfo sysInfo = new SystemInfo();
            sysInfo.GetOperatingSystemInfo();
            sysInfo.GetProcessorInfo();   
            sysInfo.GetLocalMachineName();
            sysInfo.GetCurrentUserInfo();
            sysInfo.GetDriveInfo();
            sysInfo.GetRamInfo();
            Console.WriteLine("\nDiagnostics run at " + DateTime.Now.ToShortTimeString() + " on " + DateTime.Now.ToShortDateString());
            Console.ReadLine();
        }
    }
}