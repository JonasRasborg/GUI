using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer;
using DTO;

namespace BusinessLogicLayer
{
    public class Controller : IController
    {
        public IRelay _relay;
        public IArea _area;
        public ICompassCourse _compassCourse;
        public IConflict _conflict;
        public ISpeed _speed;
        public IEventDetector _eventDetector;

        public event EventHandler<TrackEventArgs> ControllerEvent;
        public event EventHandler<EventEventArgs> DetectEvent;

        private List<Track> tracks;
        private List<Event> events;

        public Controller(IRelay relay, IArea area, ICompassCourse compassCourse, IConflict conflict, ISpeed speed, IEventDetector eventDetector)
        {
            _relay = relay;
            _area = area;
            _conflict = conflict;
            _speed = speed;
            _compassCourse = compassCourse;
            _eventDetector = eventDetector;

            _relay.RelayEvent += RelayEventHandler;
        }
        private void RelayEventHandler(object sender, TrackEventArgs e)
        {
            tracks = e.Tracks;
            tracks = _compassCourse.CalcCourse(tracks);
            tracks = _area.CheckArea(tracks);
            tracks = _speed.CalcSpeed(tracks);
            tracks = _conflict.DetectConflict(tracks);


            ControllerEvent(this, new TrackEventArgs() {Tracks = tracks});

            events = _eventDetector.DetectEvents(tracks);


            if (events != null && events.Count > 0)
            {
                DetectEvent(this, new EventEventArgs() {Events = events});
            }
        }
    }
}
