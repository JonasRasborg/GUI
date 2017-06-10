using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NSubstitute;
using NUnit.Framework;
using DTO;
using BusinessLogicLayer;
using DataAccesLayer;

namespace AirTraficMonitor.Test
{
    [TestFixture]

    class UUT_Speed
    {
        private ISpeed _uut;

        [SetUp]

        public void Setup()
        {
            _uut = new Speed();
        }

        [TestCase(0, 0, 0, 100, 1000, 100)]
        [TestCase(0, 0, 100, 100, 1000, 141)]


        public void SpeedCalculatesSpeedCorrectly(int x1, int y1, int x2, int y2, int msElapsed, double expectedResult)
        {

            List<Track> tracks1 = new List<Track>();
            List<Track> tracks2 = new List<Track>();
            DateTime now = DateTime.Now;
            tracks1.Add(new Track("A1", 3000, now, new Position(x1, y1)));

            _uut.CalcSpeed(tracks1);

            tracks2.Add(new Track("A1", 3000, now.AddMilliseconds(msElapsed), new Position(x2, y2)));

            tracks2 = _uut.CalcSpeed(tracks2);

            //Assert.That(tracks[0].InArea, Is.EqualTo(expectedResult));

            Assert.That(tracks2[0].Speed, Is.EqualTo(expectedResult));
        }

    }
}
