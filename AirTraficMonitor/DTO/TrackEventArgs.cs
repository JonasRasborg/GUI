using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TrackEventArgs : EventArgs
    {
        public List<Track> Tracks { get; set; }
        public List<Track> TracksInArea { get; set; }
        public List<Track> TracksNotInArea { get; set; }
    }
}
