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
            //build connection with products hub
            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:49970/ProductsHub").Build();
            ConnectCommand = new RelayCommand(s => true, s => Connect());

        }
        private async void Connect()
        {
            //add handlers which will be called from server-side
            _hubConnection.On<Product>("Add", p => Application.Current.Dispatcher.Invoke(() => Products.Add(new ProductViewModel(p))));
            _hubConnection.On<int>("Delete", id => Application.Current.Dispatcher.Invoke(() => Products.Remove(_products.FirstOrDefault(p => p.Id == id))));
            _hubConnection.On<int, int>("ChangeQuantity", (id, quantity) => App.Current.Dispatcher.Invoke(() => Products.FirstOrDefault(p => p.Id == id).Quantity = quantity));
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
