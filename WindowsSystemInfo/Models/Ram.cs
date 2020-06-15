using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace WindowsSystemInfo.Models
{
    public class Ram
    {
        public string TotalVisable { get; set; }
        public string FreeVisable { get; set; }
        public string TotalVirtual { get; set; }
        public string freeVirtual { get; set; }
    }
}
