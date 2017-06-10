using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccesLayer;
using DTO;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafikMonitor.Test.Integration
{
    class SUT_EventDetector
    {
        private IEventDetector _sut;
        private IFileLog _fileLog;

        [SetUp]
        public void SetUp()
        {
            _fileLog = Substitute.For<IFileLog>();
            _sut = new EventDetector(_fileLog);
        }

        [Test]
        public void SUTFileLogReceivesEvents()
        {
            List<Event> events1 = new List<Event>();
            List<Track> tracks1 = new List<Track>();

            Track track1 = new Track();
            track1.Tag = "AB408";
            track1.InArea = false;
            tracks1.Add(track1);
            events1 = _sut.DetectEvents(tracks1);

            List<Track> tracks2 = new List<Track>();
            Track track2 = new Track();
            track2.Tag = "AB408";
            track2.InArea = true;
            tracks2.Add(track2);
            events1 = _sut.DetectEvents(tracks2);

            _fileLog.Received().Log(events1);
        }
    }
}
