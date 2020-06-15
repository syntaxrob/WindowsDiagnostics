using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using WindowsSystemInfo.Models;

namespace WindowsSystemInfo
{
    public class ResultsModel
    {
        public OS OperatingSystem { get; set; }

        public string ProcessorInfo { get; set; }

        public string LocalMachineName { get; set; }

        public CurrentUser CurrentUserInfo { get; set; }

        public Drive DriveInfo { get; set; }

        public Ram RamInfo { get; set; }

        public DateTime RunOn { get; set; }
    }
}
