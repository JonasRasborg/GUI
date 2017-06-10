using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Event
    {
        public Event(DateTime time, string type, string category, List<string> involved)
        {
            Time = time;
            Type = type;
            Category = category;
            Involved = involved;
        }

        public DateTime Time
        { get; set; }

        public string Type
        { get; set; }

        public string Category
        { get; set; }

        public List<string> Involved
        { get; set; }
    }
}
