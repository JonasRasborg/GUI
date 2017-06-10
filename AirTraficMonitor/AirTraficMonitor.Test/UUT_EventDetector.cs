using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit;
using NSubstitute;
using DTO;
using BusinessLogicLayer;
using DataAccesLayer;

namespace AirTraficMonitor.Test
{
    [TestFixture]
    class UUT_EventDetector
    {
        private IEventDetector _uut;
        private IFileLog _filelog;



        [SetUp]
        public void SetUp()
        {
            _filelog = Substitute.For<IFileLog>();
            _uut = new EventDetector(_filelog);
        }

        [Test]
        public void UutConflictEventNotDetected()
        {
            List<Event> events1 = new List<Event>();

            List<Track> tracks1 = new List<Track>();
            Track track1 = new Track();
            track1.Tag = "AB408";
            tracks1.Add(track1);
            events1 = _uut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            tracks2.Add(track2);
            events1 = _uut.DetectEvents(tracks2);

            Assert.That(events1.Count > 0, Is.EqualTo(false));
        }

        [Test]
        public void UutConflictEventDetected()
        {
            List<Event> events1 = new List<Event>();

            List<Track> tracks1 = new List<Track>();
            Track track1 = new Track();
            track1.Tag = "AB408";
            tracks1.Add(track1);
            events1 = _uut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            track2.Conflicts = new List<string>();
            track2.Conflicts.Add("MK409");
            tracks2.Add(track2);
            events1 = _uut.DetectEvents(tracks2);

            Assert.That(events1.Count > 0, Is.EqualTo(true));
        }

        [Test]
        public void UutEnteredAreaEvent()
        {
            List<Event> events1 = new List<Event>();
            List<Track> tracks1 = new List<Track>();

            Track track1 = new Track();
            track1.Tag = "AB408";
            track1.InArea = false;
            tracks1.Add(track1);
            events1 = _uut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            track2.InArea = true;
            tracks2.Add(track2);

            events1 = _uut.DetectEvents(tracks2);
            Assert.That(events1.Count > 0, Is.EqualTo(true));
        }

        [Test]
        public void UutLeftAreaEvent()
        {
            List<Event> events1 = new List<Event>();
            List<Track> tracks1 = new List<Track>();

            Track track1 = new Track();
            track1.Tag = "AB408";
            track1.InArea = true;
            tracks1.Add(track1);
            events1 = _uut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            track2.InArea = false;
            tracks2.Add(track2);

            events1 = _uut.DetectEvents(tracks2);
            Assert.That(events1.Count > 0, Is.EqualTo(true));
        }

        [Test]
        public void UutNotEnteredAreaEvent()
        {
            List<Event> events1 = new List<Event>();
            List<Track> tracks1 = new List<Track>();

            Track track1 = new Track();
            track1.Tag = "AB408";
            track1.InArea = false;
            tracks1.Add(track1);
            events1 = _uut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            track2.InArea = false;
            tracks2.Add(track2);

            events1 = _uut.DetectEvents(tracks2);
            Assert.That(events1.Count > 0, Is.EqualTo(false));
        }

        [Test]
        public void UutNotLeftAreaEvent()
        {
            List<Event> events1 = new List<Event>();
            List<Track> tracks1 = new List<Track>();

            Track track1 = new Track();
            track1.Tag = "AB408";
            track1.InArea = true;
            tracks1.Add(track1);
            events1 = _uut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            track2.InArea = true;
            tracks2.Add(track2);

            events1 = _uut.DetectEvents(tracks2);
            Assert.That(events1.Count > 0, Is.EqualTo(false));
        }

       
    }
}
