using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using TransponderReceiver;

namespace DataAccesLayer
{
    public class Relay : IRelay
    {
        private List<string> _transponderData;
        private List<Track> _tracks;
        private ITransponderReceiver Tr;

        public event EventHandler<TrackEventArgs> RelayEvent;

        public Relay(ITransponderReceiver tr)
        {
            Tr = tr;
            Tr.TransponderDataReady += TransponderDataReadyEventHandler;

            _tracks = new List<Track>();
        }

        private void SortData()
        {
            if (_transponderData.Count != 0)
            {
                foreach (var item in _transponderData)
                {
                    string[] s = item.Split(';');
                    string tag = s[0];
                    int x = Convert.ToInt32(s[1]);
                    int y = Convert.ToInt32(s[2]);
                    Position position = new Position(x, y);
                    int altitude = Convert.ToInt32(s[3]);

                    int year = Convert.ToInt32(s[4].Substring(0, 4));
                    int month = Convert.ToInt32(s[4].Substring(4, 2));
                    int day = Convert.ToInt32(s[4].Substring(6, 2));
                    int hour = Convert.ToInt32(s[4].Substring(8, 2));
                    int minute = Convert.ToInt32(s[4].Substring(10, 2));
                    int second = Convert.ToInt32(s[4].Substring(12, 2));
                    int millisecond = Convert.ToInt32(s[4].Substring(14, 3));

                    DateTime time = new DateTime(year, month, day, hour, minute, second, millisecond);

                    Track track = new Track(tag, altitude, time, position);

                    _tracks.Add(track);

                }
                RelayEvent(this, new TrackEventArgs() {Tracks = _tracks});
            }
        }

        private void TransponderDataReadyEventHandler(object sender, RawTransponderDataEventArgs e)
        {
            _transponderData = e.TransponderData;
            _tracks.Clear();
            SortData();
        }
    }
}
