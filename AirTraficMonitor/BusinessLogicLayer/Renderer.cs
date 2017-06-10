using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer;
using DTO;

namespace BusinessLogicLayer
{
    public class Renderer : IRenderer
    {
        public IController _controller;
        public event EventHandler<TrackEventArgs> RenderTracks;
        public event EventHandler<EventEventArgs> RenderEvent;

        private List<Track> tracks;
        private List<Event> events;
        private List<Track> tracksInArea;
        private List<Track> tracksNotInArea;

        public Renderer(IController controller)
        {
            _controller = controller;

            _controller.ControllerEvent += ControllerEventHandler;
            _controller.DetectEvent += DetectEventHandler;
        }


        private void ControllerEventHandler(object sender, TrackEventArgs e)
        {
            tracks = e.Tracks;
            tracksInArea = new List<Track>();
            tracksNotInArea = new List<Track>();

            for (int i = 0; i < tracks.Count; i++)
            {
                if (tracks[i].InArea)
                {
                    tracksInArea.Add(tracks[i]);
                }
                else
                {
                    tracksNotInArea.Add(tracks[i]);
                }
            }

            RenderTracks(this, new TrackEventArgs() {Tracks = tracks, TracksInArea = tracksInArea, TracksNotInArea = tracksNotInArea});


 

        }


        private void DetectEventHandler(object sender, EventEventArgs e)
        {
            events = e.Events;
            RenderEvent(this, new EventEventArgs() {Events = events});
        }
    }
}
