using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccesLayer;
using NUnit.Framework;
using TransponderReceiver;
using DTO;
using NSubstitute;

namespace AirTraficMonitor.Test
{
    [TestFixture]
    class UUT_Relay
    {
        private IRelay _uut;
        private ITransponderReceiver _transponderReceiver;
        private IController _controller;
        private TrackEventArgs results;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new Relay(_transponderReceiver);

            results = new TrackEventArgs(); // "Fake" listener
        }


        [Test]
        public void UutDataFromTransponderReceiver()
        {

            // Make List of strings
            string track = "TUB788;73035;0;33000;20170427171454571";
            List<string> data = new List<string>();
            data.Add(track);

            // Make "fake" listener on Relay class, that has the TrackEventArgs, that can be Asserted on
            _uut.RelayEvent += (sender, output) => results = output;

            // Make event on controller class with above made List<Track>
            var args = new RawTransponderDataEventArgs(data);

            // Raise Event
            _transponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            // Assertions data i sorted correctly into DTO object
            Assert.That(results.Tracks[0].Tag == "TUB788");
            Assert.That(results.Tracks[0].Position.X == 73035);
            Assert.That(results.Tracks[0].Position.Y == 0);
            Assert.That(results.Tracks[0].Altitude == 33000);

            DateTime time = new DateTime(2017,04,27,17,14,54,571);

            Assert.That(results.Tracks[0].Time == time);
        }

      
    }
}
