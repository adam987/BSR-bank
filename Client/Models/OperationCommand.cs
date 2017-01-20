using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Windows.Controls;
using System.Windows.Input;
using Client.ViewModels;
using Common.Contracts;
using Common.Utils;

namespace Client.Models
{
    /// <summary>
    ///     Client operation executor
    /// </summary>
    public class OperationCommand : ICommand
    {
        private readonly CommandType? _commandType;
        private readonly MainWindowViewModel _operation;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="operation">viewmodel</param>
        /// <param name="commandType">command type if null then get current from viewmodel</param>
        public OperationCommand(MainWindowViewModel operation, CommandType? commandType = null)
        {
            _operation = operation;
            _commandType = commandType;
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        ///     Execute command
        /// </summary>
        /// <param name="password">command parameter</param>
        public void Execute(object password)
        {
            try
            {
                var passwordBox = (PasswordBox) password;

                using (var client = new SoapServiceClient())
                {
                    client.ClientCredentials.UserName.UserName = _operation.Username;
                    client.ClientCredentials.UserName.Password = passwordBox.Password;

                    switch (_commandType ?? _operation.CommandType)
                    {
                        case CommandType.Charge:
                            client.Charge(new OperationDetails
                            {
                                AccountNumber = _operation.Account.AccountNumber,
                                Amount = _operation.Amount.ToServiceString(),
                                Title = _operation.Title
                            });
                            _operation.RefreshCommand.Execute(password);
                            break;
                        case CommandType.Withdraw:
                            client.Withdraw(new OperationDetails
                            {
                                AccountNumber = _operation.Account.AccountNumber,
                                Amount = _operation.Amount.ToServiceString(),
                                Title = _operation.Title
                            });
                            _operation.RefreshCommand.Execute(password);
                            break;
                        case CommandType.Deposit:
                            client.Deposit(new OperationDetails
                            {
                                AccountNumber = _operation.Account.AccountNumber,
                                Amount = _operation.Amount.ToServiceString(),
                                Title = _operation.Title
                            });
                            _operation.RefreshCommand.Execute(password);
                            break;
                        case CommandType.History:
                            _operation.DisplayHistory(client.GetHistory(_operation.Account.AccountNumber));
                            _operation.RefreshCommand.Execute(password);
                            break;
                        case CommandType.Transfer:
                            client.Transfer(new TransferDetails
                            {
                                Amount = _operation.Amount.ToServiceString(),
                                ReceiverAccount = _operation.RecieverAccount,
                                SenderAccount = _operation.Account.AccountNumber,
                                Title = _operation.Title
                            });
                            _operation.RefreshCommand.Execute(password);
                            break;
                        case CommandType.Refresh:
                            _operation.Accounts = new ObservableCollection<AccountRow>(client.GetAccounts());
                            break;
                    }
                }
            }
            catch (MessageSecurityException)
            {
                _operation.Accounts = new ObservableCollection<AccountRow>();
                _operation.HandleCommandException(new Exception("Not authorized"));
            }
            catch (FaultException ex)
            {
                _operation.HandleCommandException(ex);
            }
            catch (EndpointNotFoundException)
            {
                _operation.HandleCommandException(new Exception("Could not connect to server"));
            }
            catch (Exception)
            {
                _operation.HandleCommandException(new Exception("Inner client exception"));
            }
        }

        /// <summary>
        ///     Not used
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}