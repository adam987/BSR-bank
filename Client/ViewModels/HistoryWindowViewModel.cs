using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Contracts;

namespace Client.ViewModels
{
    /// <summary>
    ///     History window viewmodel
    /// </summary>
    public class HistoryWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<HistoryRow> _history = new ObservableCollection<HistoryRow>();

        /// <summary>
        ///     Account history
        /// </summary>
        public ObservableCollection<HistoryRow> History
        {
            get { return _history; }
            set
            {
                _history = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}