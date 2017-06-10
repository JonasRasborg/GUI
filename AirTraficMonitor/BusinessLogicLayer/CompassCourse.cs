using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogicLayer
{
    public class CompassCourse : ICompassCourse
    {
        private List<Track> oldTracks;

        public List<Track> CalcCourse(List<Track> tracks)
        {

            // save local copy of given tracks
            if (oldTracks != null)
            {
                // iterating the new list of tracks
                for (int i = 0; i < tracks.Count; i++)
                {
                    // iterating the old list of tracks
                    for (int j = 0; j < oldTracks.Count; j++)
                    {
                        // if two Tags in the two lists are the same
                        if (tracks[i].Tag == oldTracks[j].Tag)
                        {
                            double deltaX = Convert.ToDouble(oldTracks[j].Position.X - tracks[i].Position.X);
                            double deltaY = Convert.ToDouble(oldTracks[j].Position.Y - tracks[i].Position.Y);

                            double angle = Math.Atan2(deltaY, deltaX) * (180 / Math.PI) + 90;
                            angle = (angle + 360) % 360;
                            tracks[i].CompassCourse = Convert.ToInt16(angle);
                        }
                    }
                }
            }

            oldTracks = new List<Track>();
            foreach (var track in tracks)
            {
                oldTracks.Add(track);
            }

            return tracks;
        }
    }
}