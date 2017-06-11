using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace FerieApp
{
    public class PacketLists : ObservableCollection<PacketList>, INotifyPropertyChanged
    {
        const string MainTitle = "Pakkeliste - Ferie Applikation";
        string filter;
        string filename = "";
        string filePath = "";
        bool unsavedData = false;

        public PacketLists()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                // In Design mode
                Add(new PacketList(7, "Strømper", "Mallorce 2014", true));
                Add(new PacketList(5, "T-Shirts", "Smuk Fest 2014", false));
                Title = "Ikke-navngivet - " + MainTitle;
            }

            Title = "Ikke-navngivet - " + MainTitle;
        }

        # region Commands

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddPacketList, AddVacationPackageList_CanExecute)); }
        }


        private void AddPacketList()
        {
            // Show Modal Dialog
            var dlg = new PacketListWindow();
            dlg.Title = "Add New Packet List";
            PacketList temppktList = new PacketList();
            temppktList.Amount = CurrentPacketList.Amount;
            temppktList.Subject = CurrentPacketList.Subject;
            temppktList.Vacation = CurrentPacketList.Vacation;
            dlg.DataContext = temppktList;

            if (dlg.ShowDialog() == true)
            {
                CurrentPacketList.Amount = temppktList.Amount;
                CurrentPacketList.Subject = temppktList.Subject;
                CurrentPacketList.Vacation = temppktList.Vacation;
                unsavedData = true;
            }
        }

        ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand(EditPacketList, DeleteSubject_CanExecute)); }
        }

        private void EditPacketList()
        {
            // Show Modal Dialog
            var dlg = new PacketListWindow();
            dlg.Title = "Edit PacketList";
            // Need a temp package list in case of cancel
            PacketList temppktList = new PacketList();
            temppktList.Amount = CurrentPacketList.Amount;
            temppktList.Subject = CurrentPacketList.Subject;
            temppktList.Vacation = CurrentPacketList.Vacation;

            dlg.DataContext = temppktList;
            if (dlg.ShowDialog() == true)
            {
                CurrentPacketList.Amount = temppktList.Amount;
                CurrentPacketList.Subject = temppktList.Subject;
                CurrentPacketList.Vacation = temppktList.Vacation;
                unsavedData = true;
            }
        }

        ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteSubject, DeleteSubject_CanExecute)); }
        }

        private void DeleteSubject()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete this subject?", "Warning", 
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Remove(CurrentPacketList);
                NotifyPropertyChanged("Count");
                unsavedData = true;
            }
        }

        private bool DeleteSubject_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ?? (_nextCommand = new RelayCommand(
                    () => ++CurrentIndex,
                    () => CurrentIndex < (Count - 1)));
            }
        }

        ICommand _PreviusCommand;
        public ICommand PreviusCommand
        {
            get { return _PreviusCommand ?? (_PreviusCommand = new RelayCommand(PreviusCommandExecute, PreviusCommandCanExecute)); }
        }

        private void PreviusCommandExecute()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        private bool PreviusCommandCanExecute()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        ICommand _addVacPackageListCommand;

        public ICommand AddVacPackageListCommand
        {
            get { return _addVacPackageListCommand ?? (_addVacPackageListCommand = new RelayCommand(AddVacationPackageList)); }
        }

        private void AddVacationPackageList()
        {
            // Show Modal Dialog
            var dlg = new VacationPackageListWindow();
            dlg.Title = "Add New Vacationpackage List";
            var newpklist = new PacketList();
            dlg.DataContext = newpklist;
            if (dlg.ShowDialog() == true)
            {
                Add(newpklist);
                CurrentSubjectIndex = 0;
                CurrentPacketList = newpklist;
                unsavedData = true;
            }
        }

        private bool AddVacationPackageList_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new RelayCommand(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute()
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "Ferie pakkeliste-applikation dokumenter|*.dl|All Files|*.*";
            dialog.DefaultExt = "dl";
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                filename = Path.GetFileName(filePath);
                SaveFile();
                Title = filename + " - " + MainTitle;
            }
        }

        ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get { return _SaveCommand ?? (_SaveCommand = new RelayCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)); }
        }

        private void SaveFileCommand_Execute()
        {
            SaveFile();
        }

        private void SaveFile()
        {
            List<PacketList> tempPacketList;

            try
            {
                // Copy the packagelist to a List. 
                tempPacketList = this.ToList();
                Repository.SaveFile(filePath, tempPacketList);
                unsavedData = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Count > 0);
        }

        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new RelayCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            if (unsavedData)
            {
                MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to initiate a new file without saving data first?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (res == MessageBoxResult.No)
                {
                    return;
                }
            }

            Clear();
            filename = "";
            Title = "Ikke-navngivet - " + MainTitle;
            unsavedData = false;
        }


        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new RelayCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            List<PacketList> tempPckList;
            var dialog = new OpenFileDialog();

            if (unsavedData)
            {
                MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to open a new file without saving data first?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (res == MessageBoxResult.No)
                {
                    return;
                }
            }

            dialog.Filter = "Ferie pakkeliste-applikation dokumenter|*.dl|All Files|*.*";
            dialog.DefaultExt = "dl";
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                filename = Path.GetFileName(filePath);
                try
                {
                    Repository.ReadFile(filePath, out tempPckList);

                    // We have to insert the packaglist in the existing collection. 
                    Clear();
                    foreach (var pkg in tempPckList)
                        Add(pkg);
                    Title = filename + " - " + MainTitle;
                    unsavedData = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        ICommand _CloseAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _CloseAppCommand ?? (_CloseAppCommand = new RelayCommand(CloseCommand_Execute)); }
        }

        private void CloseCommand_Execute()
        {
            bool regret = false;

            if (unsavedData)
            {
                MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to close the application without saving data first?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (res == MessageBoxResult.No)
                {
                    regret = true;
                }
            }
            if (!regret)
                Application.Current.MainWindow.Close();
        }

        /* Alternativ */
        //ICommand _ColorCommand;
        //public ICommand ColorCommand
        //{
        //    get { return _ColorCommand ?? (_ColorCommand = new RelayCommand<String>(ColorCommand_Execute)); }
        //}

        //private void ColorCommand_Execute(String colorStr)
        //{
        //    SolidColorBrush newBrush = SystemColors.WindowBrush; // Default color

        //    try
        //    {
        //        if (colorStr != null)
        //        {
        //            if (colorStr != "Default")
        //                newBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorStr));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Unknown color name, default color is used", "Program error!");
        //    }

        //    Application.Current.MainWindow.Resources["BackgroundBrush"] = newBrush;
        //}

        # endregion

        # region Functions
       
        public IReadOnlyCollection<string> FilterSubjects
        {
            get
            {
                ObservableCollection<string> result = new ObservableCollection<string>();
                result.Add("Alle");
                foreach (var s in new Subjects())
                    result.Add(s);
                return result;
            }
        }

        private bool CollectionViewSource_Filter(object pklst)
        {
            var pklist = pklst as PacketList;
            return (pklist.Subject == filter);
        }

        # endregion

        # region Properties


        int currentSubjectIndex = 0;

        public int CurrentSubjectIndex
        {
            get { return currentSubjectIndex; }
            set
            {
                if (currentSubjectIndex != value)
                {
                    ICollectionView cv = CollectionViewSource.GetDefaultView(this);
                    currentSubjectIndex = value;
                    if (currentSubjectIndex == 0)
                        cv.Filter = null; // Index 0 is no filter (show all)
                    else
                    {
                        filter = FilterSubjects.ElementAt(currentSubjectIndex);
                        cv.Filter = CollectionViewSource_Filter;
                    }
                    NotifyPropertyChanged();
                }
            }
        }

 
        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        PacketList currentPacketList = null;

        public PacketList CurrentPacketList
        {
            get { return currentPacketList; }
            set
            {
                if (currentPacketList != value)
                {
                    currentPacketList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        # endregion

        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

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
