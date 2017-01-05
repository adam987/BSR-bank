using System.Collections.ObjectModel;
using System.Linq;
using Server.SoapServices;

namespace Client.ViewModel
{
    internal class MainWindowViewModel
    {
        public ObservableCollection<AccountRow> Accounts { get; set; } = new ObservableCollection<AccountRow>();

        public ObservableCollection<string> AccountNumbers
            => new ObservableCollection<string>(Accounts.Select(row => row.AccountNumber));
    }
}