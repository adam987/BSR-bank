﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server.Database.Schema
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="BankDatabase")]
	public partial class DatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAccount(Account instance);
    partial void UpdateAccount(Account instance);
    partial void DeleteAccount(Account instance);
    partial void InsertCustomer(Customer instance);
    partial void UpdateCustomer(Customer instance);
    partial void DeleteCustomer(Customer instance);
    partial void InsertHistory(History instance);
    partial void UpdateHistory(History instance);
    partial void DeleteHistory(History instance);
    #endregion
		
		public DatabaseDataContext() : 
				base(global::Server.Properties.Settings.Default.BankDatabaseConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Account> Accounts
		{
			get
			{
				return this.GetTable<Account>();
			}
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
		
		public System.Data.Linq.Table<History> Histories
		{
			get
			{
				return this.GetTable<History>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Account")]
	public partial class Account : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Number;
		
		private decimal _Balance;
		
		private int _Customer;
		
		private EntitySet<History> _Histories;
		
		private EntityRef<Customer> _Customer1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnNumberChanging(string value);
    partial void OnNumberChanged();
    partial void OnBalanceChanging(decimal value);
    partial void OnBalanceChanged();
    partial void OnCustomerChanging(int value);
    partial void OnCustomerChanged();
    #endregion
		
		public Account()
		{
			this._Histories = new EntitySet<History>(new Action<History>(this.attach_Histories), new Action<History>(this.detach_Histories));
			this._Customer1 = default(EntityRef<Customer>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Number", DbType="NChar(26) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if ((this._Number != value))
				{
					this.OnNumberChanging(value);
					this.SendPropertyChanging();
					this._Number = value;
					this.SendPropertyChanged("Number");
					this.OnNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Balance", DbType="Decimal(18,2) NOT NULL")]
		public decimal Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				if ((this._Balance != value))
				{
					this.OnBalanceChanging(value);
					this.SendPropertyChanging();
					this._Balance = value;
					this.SendPropertyChanged("Balance");
					this.OnBalanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Customer", DbType="Int NOT NULL")]
		public int Customer
		{
			get
			{
				return this._Customer;
			}
			set
			{
				if ((this._Customer != value))
				{
					if (this._Customer1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCustomerChanging(value);
					this.SendPropertyChanging();
					this._Customer = value;
					this.SendPropertyChanged("Customer");
					this.OnCustomerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_History", Storage="_Histories", ThisKey="Number", OtherKey="AccountNumber")]
		public EntitySet<History> Histories
		{
			get
			{
				return this._Histories;
			}
			set
			{
				this._Histories.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Customer_Account", Storage="_Customer1", ThisKey="Customer", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Customer Customer1
		{
			get
			{
				return this._Customer1.Entity;
			}
			set
			{
				Customer previousValue = this._Customer1.Entity;
				if (((previousValue != value) 
							|| (this._Customer1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Customer1.Entity = null;
						previousValue.Accounts.Remove(this);
					}
					this._Customer1.Entity = value;
					if ((value != null))
					{
						value.Accounts.Add(this);
						this._Customer = value.Id;
					}
					else
					{
						this._Customer = default(int);
					}
					this.SendPropertyChanged("Customer1");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Histories(History entity)
		{
			this.SendPropertyChanging();
			entity.Account = this;
		}
		
		private void detach_Histories(History entity)
		{
			this.SendPropertyChanging();
			entity.Account = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Customer")]
	public partial class Customer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Username;
		
		private string _Password;
		
		private EntitySet<Account> _Accounts;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    #endregion
		
		public Customer()
		{
			this._Accounts = new EntitySet<Account>(new Action<Account>(this.attach_Accounts), new Action<Account>(this.detach_Accounts));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Customer_Account", Storage="_Accounts", ThisKey="Id", OtherKey="Customer")]
		public EntitySet<Account> Accounts
		{
			get
			{
				return this._Accounts;
			}
			set
			{
				this._Accounts.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Accounts(Account entity)
		{
			this.SendPropertyChanging();
			entity.Customer1 = this;
		}
		
		private void detach_Accounts(Account entity)
		{
			this.SendPropertyChanging();
			entity.Customer1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.History")]
	public partial class History : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _AccountNumber;
		
		private string _Title;
		
		private decimal _Amount;
		
		private decimal _Result;
		
		private string _ConnectedAccount;
		
		private string _OperationType;
		
		private System.DateTime _Date;
		
		private EntityRef<Account> _Account;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnAccountNumberChanging(string value);
    partial void OnAccountNumberChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnAmountChanging(decimal value);
    partial void OnAmountChanged();
    partial void OnResultChanging(decimal value);
    partial void OnResultChanged();
    partial void OnConnectedAccountChanging(string value);
    partial void OnConnectedAccountChanged();
    partial void OnOperationTypeChanging(string value);
    partial void OnOperationTypeChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    #endregion
		
		public History()
		{
			this._Account = default(EntityRef<Account>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountNumber", DbType="NChar(26) NOT NULL", CanBeNull=false)]
		public string AccountNumber
		{
			get
			{
				return this._AccountNumber;
			}
			set
			{
				if ((this._AccountNumber != value))
				{
					if (this._Account.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAccountNumberChanging(value);
					this.SendPropertyChanging();
					this._AccountNumber = value;
					this.SendPropertyChanged("AccountNumber");
					this.OnAccountNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Decimal(18,2) NOT NULL")]
		public decimal Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Result", DbType="Decimal(18,2) NOT NULL")]
		public decimal Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				if ((this._Result != value))
				{
					this.OnResultChanging(value);
					this.SendPropertyChanging();
					this._Result = value;
					this.SendPropertyChanged("Result");
					this.OnResultChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConnectedAccount", DbType="NChar(26)")]
		public string ConnectedAccount
		{
			get
			{
				return this._ConnectedAccount;
			}
			set
			{
				if ((this._ConnectedAccount != value))
				{
					this.OnConnectedAccountChanging(value);
					this.SendPropertyChanging();
					this._ConnectedAccount = value;
					this.SendPropertyChanged("ConnectedAccount");
					this.OnConnectedAccountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperationType", DbType="VarChar(20)")]
		public string OperationType
		{
			get
			{
				return this._OperationType;
			}
			set
			{
				if ((this._OperationType != value))
				{
					this.OnOperationTypeChanging(value);
					this.SendPropertyChanging();
					this._OperationType = value;
					this.SendPropertyChanged("OperationType");
					this.OnOperationTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_History", Storage="_Account", ThisKey="AccountNumber", OtherKey="Number", IsForeignKey=true)]
		public Account Account
		{
			get
			{
				return this._Account.Entity;
			}
			set
			{
				Account previousValue = this._Account.Entity;
				if (((previousValue != value) 
							|| (this._Account.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Account.Entity = null;
						previousValue.Histories.Remove(this);
					}
					this._Account.Entity = value;
					if ((value != null))
					{
						value.Histories.Add(this);
						this._AccountNumber = value.Number;
					}
					else
					{
						this._AccountNumber = default(string);
					}
					this.SendPropertyChanged("Account");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
