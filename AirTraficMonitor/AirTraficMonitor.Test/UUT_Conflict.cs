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
    class UUT_Conflict
    {
        private IConflict _uut;


        [SetUp]
        public void SetUp()
        {
            _uut = new Conflict();
        }

        [TestCase(85000, 20000, 34000, 85000, 20000, 34300, false)] //Check z
        [TestCase(85000, 20000, 34000, 85000, 20000, 34299, true)] //Check z
        [TestCase(80000, 20000, 34000, 85000, 20000, 34000, false)] //Check x
        [TestCase(80000, 20000, 34000, 84999, 20000, 34000, true)] //Check x
        [TestCase(85000, 20000, 34000, 85000, 25000, 34000, false)] //Check y
        [TestCase(85000, 20000, 34000, 85000, 24999, 34000, true)] //Check y 
        public void UutDetectConflict(int x1, int y1, int z1, int x2, int y2, int z2, bool expectedResult)
        {
            Track track1 = new Track("track1", z1, DateTime.Now, new Position(x1, y1));
            Track track2 = new Track("track2", z2, DateTime.Now, new Position(x2, y2));

            List<Track> tracks = new List<Track>();
            tracks.Add(track1);
            tracks.Add(track2);

            _uut.DetectConflict(tracks);
            Assert.That(tracks[0].Conflicts != null, Is.EqualTo(expectedResult));
        }

        [Test]
        public void UutConflictAlreadyExists()
        {
            Track track1 = new Track("track1", 3500, DateTime.Now, new Position(5000, 5000));
            Track track2 = new Track("track2", 3500, DateTime.Now, new Position(5200, 5000));
            Track track3 = new Track("track3", 3500, DateTime.Now, new Position(5100, 5200));

            List<Track> tracks = new List<Track>();
            tracks.Add(track1);
            tracks.Add(track2);
            tracks.Add(track3);

            _uut.DetectConflict(tracks);

            Assert.That(tracks[0].Conflicts.Count == 2, Is.EqualTo(true));
        }
    }
}
