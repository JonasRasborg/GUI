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

            // Assertions
            Assert.That((results.Tracks.Count > 0).Equals(true));
        }

        [Test]
        public void UutNoDataFromTransponderReceiver()
        {

            // Make List of strings
            List<string> data = new List<string>();

            // Make "fake" listener on Relay class, that has the TrackEventArgs, that can be Asserted on
            _uut.RelayEvent += (sender, output) => results = output;

            // Make event on controller class with above made List<Track>
            var args = new RawTransponderDataEventArgs(data);

            // Raise Event
            _transponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            // Assertions
            Assert.That((results.Tracks == null).Equals(true));
        }

        [Test]
        public void UutMultipleDataFromTransponderReceiver()
        {

            // Make List of strings
            string track1 = "TUB788;73035;0;33000;20170427171454571";
            string track2 = "ABT408;64800;0;50000;20170427171454571";
            string track3 = "AOU789;46000;5000;45000;20170427171454571";
            List<string> data = new List<string>();
            data.Add(track1);
            data.Add(track2);
            data.Add(track3);

            // Make "fake" listener on Relay class, that has the TrackEventArgs, that can be Asserted on
            _uut.RelayEvent += (sender, output) => results = output;

            // Make event on controller class with above made List<Track>
            var args = new RawTransponderDataEventArgs(data);

            // Raise Event
            _transponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            // Assertions
            Assert.That((results.Tracks.Count == 3).Equals(true));
        }
    }
}
