using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace WindowsSystemInfo.Models
{
    public class Drive
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Format { get; set; }
        public string Type { get; set; }
        public string FreeSpace { get; set; }
    }
}
