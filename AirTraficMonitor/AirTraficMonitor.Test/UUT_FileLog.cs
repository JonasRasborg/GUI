using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccesLayer;
using DTO;
using NSubstitute;
using NUnit.Framework;

namespace AirTraficMonitor.Test
{
    [TestFixture]
    class UUT_FileLog
    {
        private IFileLog _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new FileLog();
        }

        [Test]
        public void UutFileLogLoginFile()
        {
            string file = "log.txt";
            string tag = "AB408";

            List<string> involved = new List<string>();
            involved.Add(tag);
            DateTime now = new DateTime();
            now = DateTime.Now;

            Event e = new Event(now, "Entered Monitored Area", "Notification", involved);
            List<Event> events = new List<Event>();
            events.Add(e);

            _uut.Log(events);

            string[] text = File.ReadAllLines(file);

            string[] input = text[0].Split(' ');

            string time = input[0] +" "+ input[1];
            string loggedTag = input[input.Length-1];

            Assert.That(time.Equals(now.ToString()));
            Assert.That(loggedTag.Equals(tag));
        }
    }
}
