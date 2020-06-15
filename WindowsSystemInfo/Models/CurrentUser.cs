using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsSystemInfo.Models
{
    public class CurrentUser
    {
        public string Username { get; set; }

        public string AuthType { get; set; }

        public string IsGuest { get; set; }
    }
}
