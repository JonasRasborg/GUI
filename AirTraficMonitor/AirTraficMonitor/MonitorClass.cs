using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DTO;

namespace AirTraficMonitor
{
    class Monitor
    {
        private static List<Track> _tracksInArea;
        private static List<Track> _tracksNotInArea;
        private static Renderer _renderer;

        static void Main(string[] args)
        {
            _tracksInArea = new List<Track>();
            _renderer = new Renderer();

            _renderer.RenderInAreaEvent += RenderAllEventHandler;
            _renderer.RenderNotInAreaEvent += RenderAllEventHandler;

            while (true)
            {
            }
        }

        public static void RenderAllEventHandler(object sender, List<Track> e)
        {
            _tracksInArea = _renderer.TracksInArea;
            _tracksNotInArea = _renderer.TracksNotInArea;
            PrintData();
        }

        private static void PrintData()
        {
            Console.Clear();
            Console.WriteLine("Tracks in Area: " + _tracksInArea.Count);

            if (_tracksInArea.Count > 0)
            {
                foreach (var track in _tracksInArea)
                {
                    //Console.WriteLine("Flight: " + track.Tag + "\t\t" + track.Positions[0].X + " ; " + track.Positions[0].Y);
                    Console.WriteLine(
                        "Flight: {0,-5} {1,10} ; {2,-10} Altitude: {3,-10} time: {4,-10} In Area: {5,5} Compass Course: {6,5} Speed: {7,5}",
                        track.Tag, track.Position.X, track.Position.Y, track.Altitude, track.Time, track.InArea,
                        track.CompassCourse, track.Speed);
                }

            }
            if (_tracksNotInArea.Count > 0)
            {
                Console.WriteLine("Tracks out of Area: " + _tracksNotInArea.Count);
                foreach (var track in _tracksNotInArea)
                {
                    //Console.WriteLine("Flight: " + track.Tag + "\t\t" + track.Positions[0].X + " ; " + track.Positions[0].Y);
                    Console.WriteLine(
                        "Flight: {0,-5} {1,10} ; {2,-10} Altitude: {3,-10} time: {4,-10} In Area: {5,5} Compass Course: {6,5} Speed: {7,5}",
                        track.Tag, track.Position.X, track.Position.Y, track.Altitude, track.Time, track.InArea,
                        track.CompassCourse, track.Speed);
                }
            }
        }
    }
}
