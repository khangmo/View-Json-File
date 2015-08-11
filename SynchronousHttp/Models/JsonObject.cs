using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynchronousHttp.Models
{
    public class JsonObject
    {
        public DateTime time { get; set; }
        public string timeString { get; set; }
        public List<Image> images { get; set; }
    }
}