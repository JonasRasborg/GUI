using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DTO;
using BusinessLogicLayer;
using DataAccesLayer;
using TransponderReceiver;

namespace GUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Renderer Renderer;
        public ObservableCollection<Track> ConsoleTracks { get; set; }
        public ObservableCollection<Event> MonitorEvents { get; set; }
        public ObservableCollection<Event> WarningEvents { get; set; }
        private List<Track> allTracks;
        private List<Event> notificationEvents;
        private List<Event> warningEvents;
        private List<Track> tracksInArea;
        private List<Track> tracksNotInArea;

        public List<KeyValuePair<int, int>> PositionsIn { get; set; }
        public List<KeyValuePair<int, int>> PositionsOut { get; set; }


        public ViewModel()
        {
            Renderer = new Renderer(new Controller(new Relay(TransponderReceiverFactory.CreateTransponderDataReceiver()), new Area(), new CompassCourse(), new Conflict(),
                new Speed(), new EventDetector(new FileLog())));

            Renderer.RenderTracks += RenderAllEventHandler;
            Renderer.RenderEvent += RenderEventHandler;
            notificationEvents = new List<Event>();
            warningEvents = new List<Event>();

        }


        private void RenderEventHandler(object sender, EventEventArgs e)
        {
            warningEvents = new List<Event>();
            foreach (var item in e.Events)
            {
                if (item.Category == "Notification")
                {
                    notificationEvents.Add(item);
                    }
                else if (item.Category == "Warning")
                {
                    warningEvents.Add(item);
                }
            }
            UpdateWarnings();

            
        }

      
        private void UpdateNotification()
        {
            MonitorEvents = new ObservableCollection<Event>();

            foreach (var item in notificationEvents)
            {
                MonitorEvents.Add(item);
            }
            NotifyPropertyChanged("MonitorEvents");
        }

        private void UpdateWarnings()
        {
            WarningEvents = new ObservableCollection<Event>();

            foreach (var item in warningEvents)
            {
                WarningEvents.Add(item);
            }
            NotifyPropertyChanged("WarningEvents");
        }

        private void EventTimer()
        {
            
                for (int i = 0; i < notificationEvents.Count; i++)
                {
                if (DateTime.Now - notificationEvents[i].Time > new TimeSpan(0, 0, 5) || notificationEvents[i].Category == "Warning" && DateTime.Now - notificationEvents[i].Time > new TimeSpan(0, 0, 0,800))
                    {
                        notificationEvents.RemoveAt(i);
                        i--;
                    }
                }
                UpdateNotification();
        }


        private void RenderAllEventHandler(object sender, TrackEventArgs e)
        {
            UpdateChart(e.TracksInArea, e.TracksNotInArea);
            UpdateConsole(e.TracksInArea);
            if (notificationEvents.Count != 0)
            {
                EventTimer();
            }

        }

        private void UpdateConsole(List<Track> tracks)
        {
            allTracks = tracks;
            ConsoleTracks = new ObservableCollection<Track>();

            foreach (var t in allTracks)
            {
                ConsoleTracks.Add(t);
            }

            NotifyPropertyChanged("ConsoleTracks");
        }

        private void UpdateChart(List<Track> inArea, List<Track> notInArea)
        {
            tracksInArea = inArea;
            tracksNotInArea = notInArea;

            List<KeyValuePair<int, int>> tracksIn = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> tracksOut = new List<KeyValuePair<int, int>>();

            foreach (var t in tracksInArea)
            {
                KeyValuePair<int, int> k = new KeyValuePair<int, int>(t.Position.X, t.Position.Y);
                tracksIn.Add(k);
            }

            foreach (var t in tracksNotInArea)
            {
                KeyValuePair<int, int> k = new KeyValuePair<int, int>(t.Position.X, t.Position.Y);
                tracksOut.Add(k);
            }

            PositionsIn = tracksIn;
            PositionsOut = tracksOut;
            NotifyPropertyChanged("PositionsIn");
            NotifyPropertyChanged("PositionsOut");
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }



    public class ViewModelLocator
    {
        public ViewModel ViewModel
        {
            get { return new ViewModel(); }
        }
    }
}
