using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogicLayer
{
    public class Conflict : IConflict
    {
        private int conflictDistance = 5000;
        private int conflictHeight = 300;
        public List<Track> DetectConflict(List<Track> tracks)
        {
            Track currentTrack;
            int numberOfTracks = tracks.Count;

            if (numberOfTracks > 1)
            {
                for (int i = 0; i < numberOfTracks; i++)
                {
                    currentTrack = tracks[i];

                    foreach (var track in tracks)
                    {
                        if (currentTrack.Tag != track.Tag)
                        {
                            if (Math.Sqrt(Math.Pow(track.Position.X - currentTrack.Position.X, 2) +
                                          Math.Pow(track.Position.Y - currentTrack.Position.Y, 2)) < conflictDistance)
                            {
                                if (Math.Abs(currentTrack.Altitude - track.Altitude) < conflictHeight)
                                {
                                    if (currentTrack.Conflicts == null)
                                    {
                                        currentTrack.Conflicts = new List<string>();
                                        currentTrack.Conflicts.Add(track.Tag);
                                    }
                                    else
                                    {
                                        currentTrack.Conflicts.Add(track.Tag);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return tracks;
        }
    }
}
