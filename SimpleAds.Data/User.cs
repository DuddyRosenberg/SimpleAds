using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAds.Data
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
