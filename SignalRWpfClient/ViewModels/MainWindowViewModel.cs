using CustomerCrudReduxSample.Mvvm;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRWpfClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SignalRWpfClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly HubConnection _hubConnection;
        public MainWindowViewModel()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:49970/ProductsHub").Build();
            ConnectCommand = new RelayCommand(s => true, s => Connect());

        }
        private async void Connect()
        {
            _hubConnection.On<Product>("Add", p =>
            {
                Application.Current.Dispatcher.Invoke(() => Products.Add(p)
                );
            }
            );
            _hubConnection.On<int>("Delete", id => Application.Current.Dispatcher.Invoke(() => Products.Remove(_products.FirstOrDefault(p => p.Id == id))));
            await _hubConnection.StartAsync();
        }
        public RelayCommand ConnectCommand { get; set; }
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => this.SetField(ref _products, value);
        }

    }
}
