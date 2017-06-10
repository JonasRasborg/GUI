using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogicLayer
{
    public class Speed : ISpeed
    {
        private List<Track> oldTracks;

        public List<Track> CalcSpeed(List<Track> tracks)
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
                            double deltaX = tracks[i].Position.X - oldTracks[j].Position.X;
                            double deltaY = tracks[i].Position.Y - oldTracks[j].Position.Y;
                            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                            double time = (tracks[i].Time.Ticks - oldTracks[j].Time.Ticks)*0.0000001;
                            //double time = (tracks[i].Time.Second + tracks[i].Time.Millisecond*0.001) - (oldtracks[j].Time.Second + oldtracks[j].Time.Millisecond*0.001);
                            //Speed is in m/s
                            double speed = distance / time;
                            tracks[i].Speed = Math.Round(speed,0);
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
