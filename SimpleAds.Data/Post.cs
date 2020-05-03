using System;

namespace SimpleAds.Data
{
    public class Post
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DatePosted { get; set; }
        public string Text { get; set; }
        public int UserID { get; set; }
    }
}
