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
    class UUT_Area
    {
        private IArea _uut;
        [SetUp]
        public void SetUp()
        {
            _uut = new Area();
        }

        [Test]
        [TestCase(85000, 95000, false)]
        [TestCase(75000, 95000, false)]
        [TestCase(60000, 60000, true)]
        [TestCase(10000, 10000, true)]
        [TestCase(90000, 90000, true)]
        [TestCase(90001, 90000, false)]
        [TestCase(90000, 90001, false)]
        [TestCase(10000, 9999, false)]
        [TestCase(9999, 10000, false)]
        [TestCase(10001, 10001, true)]
        public void UutSetsTracksToOutOfArea(int x, int y, bool expectedResult)
        {
            List<Track> tracks = new List<Track>();
            tracks.Add(new Track("A1", 5000, DateTime.Now, new Position(x, y)));

            tracks = _uut.CheckArea(tracks);
            Assert.That(tracks[0].InArea, Is.EqualTo(expectedResult));
        }
    }
}
