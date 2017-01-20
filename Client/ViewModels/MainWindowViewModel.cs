using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Client.Models;
using Client.Views;
using Common.Contracts;

namespace Client.ViewModels
{
    /// <summary>
    ///     Main window viewmodel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AccountRow _account;
        private ObservableCollection<AccountRow> _accounts = new ObservableCollection<AccountRow>();
        private CommandType _commandType;

        /// <summary>
        ///     Operation command
        /// </summary>
        public ICommand OperationCommand => new OperationCommand(this);

        /// <summary>
        ///     Refresh command
        /// </summary>
        public ICommand RefreshCommand => new OperationCommand(this, CommandType.Refresh);

        /// <summary>
        ///     Accounts list
        /// </summary>
        public ObservableCollection<AccountRow> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Available operations
        /// </summary>
        public ObservableCollection<CommandType> AvailableOperations { get; set; } =
            new ObservableCollection<CommandType>(new[]
            {
                CommandType.Charge,
                CommandType.Deposit,
                CommandType.Withdraw,
                CommandType.Transfer,
                CommandType.History
            });

        /// <summary>
        ///     Current command type
        /// </summary>
        public CommandType CommandType
        {
            get { return _commandType; }
            set
            {
                _commandType = value;
                OnPropertyChanged(nameof(IsTransferOperation));
            }
        }

        /// <summary>
        ///     Current account
        /// </summary>
        public AccountRow Account
        {
            get { return _account; }
            set
            {
                _account = value;
                OnPropertyChanged(nameof(IsAccountSelected));
            }
        }

        /// <summary>
        ///     Receiver accout
        /// </summary>
        public string RecieverAccount { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Operation title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Operation amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Is transfer operation selected
        /// </summary>
        public Visibility IsTransferOperation
            => CommandType == CommandType.Transfer ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        ///     Is account selected
        /// </summary>
        public bool IsAccountSelected => Account != null;

        /// <summary>
        ///     Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Display operation exception
        /// </summary>
        /// <param name="exception">exception</param>
        public void HandleCommandException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        ///     Display account history
        /// </summary>
        /// <param name="history">account history</param>
        public void DisplayHistory(List<HistoryRow> history)
        {
            var historyWindow = new HistoryWindow {Title = Account.AccountNumber};
            ((HistoryWindowViewModel) historyWindow.Resources["ViewModel"]).History =
                new ObservableCollection<HistoryRow>(history.OrderByDescending(row => row.Date));
            historyWindow.Topmost = true;
            historyWindow.ShowDialog();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}