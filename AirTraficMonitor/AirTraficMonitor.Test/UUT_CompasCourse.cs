using System;
using System.Collections.Generic;
using System.Linq;
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

    class UUT_CompasCourse
    {
        private ICompassCourse _Compasscourse;

        [SetUp]

        public void Setup()
        {
            _Compasscourse = new CompassCourse();
        }

        [TestCase(0, 0, 0, 1, 0)] // Headed North
        [TestCase(0, 0, -1, 1, 45)] // North/West
        [TestCase(0, 0, -1, 0, 90)] // West
        [TestCase(0, 0, -1, -1, 135)] // South/West
        [TestCase(0, 0, 0, -1, 180)] // Headed South
        [TestCase(0, 0, 1, -1, 225)] // South/East
        [TestCase(0, 0, 1, 0, 270)] // East
        [TestCase(0, 0, 1, 1, 315)] // North/East



        public void CompassCourseCalculatesCourseCorrectly(int x1, int y1, int x2, int y2, int expected)
        {

            List<Track> tracks = new List<Track>();

            tracks.Add(new Track("trackA", 1000, DateTime.Now, new Position(x1, y1)));
            _Compasscourse.CalcCourse(tracks);

            tracks.Clear();

            tracks.Add(new Track("trackA", 1000, DateTime.Now, new Position(x2, y2)));
            tracks = _Compasscourse.CalcCourse(tracks);

            Assert.AreEqual(expected, tracks[0].CompassCourse);

        }




    }
}