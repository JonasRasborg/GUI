using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Track
    {
        public Track(string tag, int altitude, DateTime time, Position position)
        {
            Tag = tag;
            Altitude = altitude;
            Time = time;
            Position = position;
        }

        public Track()
        {
            
        }

        public string Tag
        { get; set; }

        public Position Position
        { get; set; }

        public int Altitude
        { get; set; }

        public DateTime Time
        { get; set; }

        public double Speed { get; set; }
        public int CompassCourse { get; set; }
        public List<string> Conflicts { get; set; }
        public bool InArea { get; set; }

    }
}
