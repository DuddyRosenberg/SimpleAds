using SimpleAds.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAds.Models
{
    public class IndexViewModel
    {
        public int userID { get; set; }
        public List<Post> Posts { get; set; }
    }
}
