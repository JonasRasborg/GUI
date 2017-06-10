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
    class SUT_Renderer
    {
        private IRenderer _uut;
        private IController _controller;
        private TrackEventArgs _resultsTracks;
        private EventEventArgs _resultEvents;

        [SetUp]

        public void Setup()
        {
            _controller = Substitute.For<IController>();
            _uut = new Renderer(_controller);
            _resultsTracks = new TrackEventArgs(); // "Fake" listener
            _resultEvents = new EventEventArgs();


        }


        [Test]

        public void RendererAddsTracksToCorrectListBasedOnInAreaStatus()
        {

            // Make List of track
            List<Track> t = new List<Track>();
            t.Add(new Track("Tin", 10000, DateTime.Now, new Position(0, 0)));
            t[0].InArea = true;

            // Make "fake" listener on Renderer class, that has the TrackEventArgs, that can be Asserted on
            _uut.RenderTracks += (sender, output) => _resultsTracks = output;

            // Make event on controller class with above made List<Track>
            var args = new TrackEventArgs() { Tracks = t };

            // Raise Event
            _controller.ControllerEvent += Raise.EventWith(this, args);

            // Assertions
            Assert.That(_resultsTracks.Tracks.Count > 0 == true);
            Assert.That(_resultsTracks.TracksInArea.Count > 0 == true);
            Assert.That(_resultsTracks.TracksNotInArea.Count > 0 == false);

        }

        [Test]
        public void UutRenderEventsFromController()
        {
            _uut.RenderEvent += (sender, outputEvent) => _resultEvents = outputEvent;
            List<Event> e = new List<Event>();
            e.Add(new Event(DateTime.Now, "Entered Area", "Notification", new List<string>()));
            var args = new EventEventArgs() { Events = e };
            _controller.DetectEvent += Raise.EventWith(this, args);

            Assert.That(_resultEvents.Events.Count == 1);
        }
    }
}
