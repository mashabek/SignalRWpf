using Microsoft.AspNetCore.SignalR.Client;
using SignalRXamarin.Models;
using SignalRXamarin.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SignalRXamarin.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly HubConnection _hubConnection;
        public MainPageViewModel()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("http://10.0.2.2:49970/ProductsHub").Build();
            ConnectCommand = new RelayCommand(Connect);

        }
        private async void Connect()
        {
            _hubConnection.On<Product>("Add", p =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var product = p as Product;
                    Products.Add(product);
                });
            }
            );
            _hubConnection.On<int>("Delete", id => Device.BeginInvokeOnMainThread(() => Products.Remove(_products.FirstOrDefault(p => p.Id == id))));
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
