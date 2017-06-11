using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FerieApp.Annotations;

namespace FerieApp
{
    [Serializable]
    public class PacketList : INotifyPropertyChanged
    {
        private int amount;
        private string subject;
        private string vacpktlist;
        private bool isDone;

        public PacketList()
        {
            
        }

        public PacketList(int xAmount, string aSubject, string aVacPktList, bool aIsDone)
        {
            amount = xAmount;
            subject = aSubject;
            vacpktlist = aVacPktList;
            isDone = aIsDone;
        }

        public int Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Subject
        {
            get { return subject; }
            set
            {
                if (subject != value)
                {
                    subject = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Vacation
        {
            get { return vacpktlist; }
            set
            {
                if (vacpktlist != value)
                {
                    vacpktlist = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (isDone != value)
                {
                    isDone = value;
                    NotifyPropertyChanged();
                }
            }
        }



        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
