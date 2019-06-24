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
            //build hub connection using 10.0.2.2 address, which will be mapped to 172.0.0.1 on an emulator
            _hubConnection = new HubConnectionBuilder().WithUrl("http://10.0.2.2:49970/ProductsHub").Build();
            ConnectCommand = new RelayCommand(Connect);

        }
        private async void Connect()
        {
            //add handlers which will be called from server-side
            _hubConnection.On<Product>("Add", p => Device.BeginInvokeOnMainThread(() => Products.Add(new ProductViewModel(p))));
            _hubConnection.On<int>("Delete", id => Device.BeginInvokeOnMainThread(() => Products.Remove(_products.FirstOrDefault(p => p.Id == id))));
            _hubConnection.On<int, int>("ChangeQuantity", (id, quantity) => Device.BeginInvokeOnMainThread(() => Products.FirstOrDefault(p => p.Id == id).Quantity = quantity));
            //start connection
            await _hubConnection.StartAsync();
        }
        public RelayCommand ConnectCommand { get; set; }
        private ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set => this.SetField(ref _products, value);
        }
    }
    //Wrapper for Product model used to bind to a View
    public class ProductViewModel : ViewModelBase
    {
        private Product _product;
        public ProductViewModel(Product product)
        {
            _product = product;
        }
        public int Id
        {
            get => _product.Id;
            set
            {
                _product.Id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get => _product.Name;
            set
            {
                _product.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Quantity
        {
            get => _product.Quantity;
            set
            {
                _product.Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public decimal Price
        {
            get => _product.Price;
            set
            {
                _product.Price = value;
                OnPropertyChanged("Price");
            }
        }
    }
}
