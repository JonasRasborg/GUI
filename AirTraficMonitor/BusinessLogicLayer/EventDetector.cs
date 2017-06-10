using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataAccesLayer;

namespace BusinessLogicLayer
{


    public class EventDetector : IEventDetector
    {
        private List<Track> oldTracks;
        private List<Event> events;
        private IFileLog _fileLog;
    

        public EventDetector(IFileLog filelog)
        {
            _fileLog = filelog;
            oldTracks = new List<Track>();
        }

        public List<Event> DetectEvents(List<Track> tracks)
        {
            events = new List<Event>();

            if (oldTracks != null)
            {
                // Detect tracks leaving monitored area
                for (int i = 0; i < tracks.Count; i++)
                {
                    for (int j = 0; j < oldTracks.Count; j++)
                    {
                        if (tracks[i].Tag == oldTracks[j].Tag)
                        {
                            // if flight entered monitored area
                            if (tracks[i].InArea == true && oldTracks[j].InArea == false)
                            {
                                List<string> involved = new List<string>();
                                involved.Add(tracks[i].Tag);
                                events.Add(new Event(tracks[i].Time, "Entered monitored area", "Notification", involved));
                            }

                            // if flight left monitored area
                            if (tracks[i].InArea == false && oldTracks[j].InArea == true)
                            {
                                List<string> involved = new List<string>();
                                involved.Add(tracks[i].Tag);
                                events.Add(new Event(tracks[i].Time, "Left monitored area", "Notification", involved));
                            }

                            // if flight is in a conflict
                            if (tracks[i].Conflicts != null)
                            {
                                for (int k = 0; k < tracks[i].Conflicts.Count; k++)
                                {
                                    events.Add(new Event(tracks[i].Time, "Is in a conflict with Flight", "Warning", tracks[i].Conflicts));
                                }
                            }


                        }
                    }
                }

                // Detect conflicts

                // clear oldtracks and events
                oldTracks.Clear();
            }

            // Save local copy of tracks for next iteration
            for (int i = 0; i < tracks.Count; i++)
            {
                oldTracks.Add(tracks[i]);
            }

            //Log
            if (events.Count > 0)
            {
                _fileLog.Log(events);
            }


            // return events
            return events;
        }


    }
}