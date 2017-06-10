using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using NUnit.Framework;
using GUI;
using NSubstitute;
using DTO;

namespace AirTraficMonitor.Test
{
    [TestFixture]
    class UUT_Render
    {
        private IRenderer _uut;
        private IController _controller;
        private TrackEventArgs _resultTracks;
        private EventEventArgs _resultEvents;

        [SetUp]

        public void Setup()
        {
            _controller = Substitute.For<IController>();
            _uut = new Renderer(_controller);
            _resultTracks = new TrackEventArgs(); // "Fake" listener
            _resultEvents = new EventEventArgs();

            // Make "fake" listener on Renderer class, that has the TrackEventArgs, that can be Asserted on
            _uut.RenderTracks += (sender, output) => _resultTracks = output;
            _uut.RenderEvent += (sender, outputEvent) => _resultEvents = outputEvent;
        }

        [TestCase(true, true, true, false)]
        [TestCase(false, true, false, true)]

        public void RendererAddsTracksToCorrectListBasedOnInAreaStatus(bool inarea, bool inTracksList, bool inTracksInAreaList, bool inTracksNotInAreaList)
        {
            // Make List of track
            List<Track> t = new List<Track>();
            t.Add(new Track("Tin", 10000, DateTime.Now, new Position(0, 0)));
            t[0].InArea = inarea;

            // Make event args
            var args = new TrackEventArgs() { Tracks = t };

            // Raise Event
            _controller.ControllerEvent += Raise.EventWith(this, args);

            // Assertions
            Assert.That(_resultTracks.Tracks.Count > 0 == inTracksList);
            Assert.That(_resultTracks.TracksInArea.Count > 0 == inTracksInAreaList);
            Assert.That(_resultTracks.TracksNotInArea.Count > 0 == inTracksNotInAreaList);
        }


        [TestCase("t1", 10000, 0, 0, true)]
        [TestCase("t2", 500, 600, -700, false)]
        public void RendererAddsCorrectInfoToList(string tag, int altitude, int xpos, int ypos, bool inarea)
        {
            List<Track> t = new List<Track>();
            t.Add(new Track(tag, altitude, DateTime.Now, new Position(xpos, ypos)));
            t[0].InArea = inarea;

            // Make event args
            var args = new TrackEventArgs() { Tracks = t };
            // Raise Event
            _controller.ControllerEvent += Raise.EventWith(this, args);

            Assert.That(_resultTracks.Tracks[0].Tag == tag);
            Assert.That(_resultTracks.Tracks[0].Altitude == altitude);
            Assert.That(_resultTracks.Tracks[0].Position.X == xpos);
            Assert.That(_resultTracks.Tracks[0].Position.Y == ypos);
            Assert.That(_resultTracks.Tracks[0].InArea == inarea);
        }

        [Test]
        public void UutRenderEventsFromController()
        {

            List<Event> e = new List<Event>();
            e.Add(new Event(DateTime.Now,"Entered Area","Notification",new List<string>()));
            var args = new EventEventArgs() {Events = e};
            _controller.DetectEvent += Raise.EventWith(this, args);

            Assert.That(_resultEvents.Events.Count == 1);
        }

    }
}
