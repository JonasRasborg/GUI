using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccesLayer;
using NSubstitute;
using NUnit.Framework;
using DTO;

namespace AirTrafikMonitor.Test.Integration
{
    [TestFixture]
    class UUT_Ctrl
    {
        private IRelay _relay;
        private IArea _area;
        private IConflict _conflict;
        private ISpeed _speed;
        private ICompassCourse _compasscourse;
        private IEventDetector _eventdetector;
        private IController _uut;

        private TrackEventArgs args;
        private List<Track> t;

        private TrackEventArgs _results; // Fake listener "Renderer"

        [SetUp]
        public void SetUp()
        {
            _relay = Substitute.For<IRelay>();
            _area = Substitute.For<IArea>();
            _conflict = Substitute.For<IConflict>();
            _speed = Substitute.For<ISpeed>();
            _compasscourse = Substitute.For<ICompassCourse>();
            _eventdetector = Substitute.For<IEventDetector>();
            _relay = Substitute.For<IRelay>();
            _results = new TrackEventArgs();

            // Arbitrary List og tracks 
            t = new List<Track>();
            t.Add(new Track("A", 500, DateTime.Now, new Position(1, 1)));

            // Make event on controller class with above made List<Track>
            args = new TrackEventArgs() { Tracks = t };

            // Make methods return data
            _compasscourse.CalcCourse(t).Returns(t);
            _area.CheckArea(t).Returns(t);
            _speed.CalcSpeed(t).Returns(t);
            _conflict.DetectConflict(t).Returns(t);

            // Make uut
            _uut = new Controller(_relay, _area, _compasscourse, _conflict, _speed, _eventdetector);

            // Make "fake" listener on Controller class, that has the TrackEventArgs, that can be Asserted on, also an event needs to have a listener to not break the code
            _uut.ControllerEvent += (sender, output) => _results = output;


        }

        [Test]
        public void ControllerCallsCompassCourse_CalcCourse()
        {
            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            _compasscourse.Received().CalcCourse(t);
        }

        [Test]
        public void ControllerCallsArea_CheckArea()
        {
            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            _area.Received().CheckArea(t);
        }

        [Test]
        public void ControllerCallsSpeed_CalcSpeed()
        {
            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            _speed.Received().CalcSpeed(t);
        }

        [Test]
        public void ControllerCallsConflict_DetectConflict()
        {
            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            _conflict.Received().DetectConflict(t);
        }

        [Test]
        public void ControllerRaisersControllerEvent()
        {
            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            Assert.That(_results.Tracks.Count.Equals(1));
        }

        [Test]
        public void ControllerCallsEventDetector_DetectEvent()
        {
            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            _eventdetector.Received().DetectEvents(t);
        }

        [Test]
        public void ControllerRaisesDetectEvent()
        {
            List<string> tags = new List<string>();
            tags.Add("T1");
            Event e = new Event(DateTime.Now, "entered area", "Notification", tags);
            List<Event> events = new List<Event>();
            events.Add(e);


            EventEventArgs _eResults = new EventEventArgs() { Events = events };

            //Fake listener 
            _uut.DetectEvent += (sender, output2) => _eResults = output2;


            _eventdetector.DetectEvents(t).Returns(events);

            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            Assert.That(_eResults.Events.Count.Equals(1));
        }

        [Test]
        public void ControllerDoesntRaiseDetectEvent()
        {
            //List<string> tags = new List<string>();
            //tags.Add("T1");
            //Event e = new Event(DateTime.Now, "entered area", "Notification", tags);
            List<Event> events = new List<Event>();
            //events.Add(e);


            EventEventArgs _eResults = new EventEventArgs() {Events = events};

            _eventdetector.DetectEvents(t).Returns(events);

            // Raise Event
            _relay.RelayEvent += Raise.EventWith(this, args);

            // Assertions
            Assert.That(_eResults.Events.Count.Equals(0));
        }


    }
}