using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Turismo.MVVM.Models;

namespace Turismo.MVVM.ViewModels
{
    public class SalesListViewModel : INotifyPropertyChanged
    {
        //public ObservableCollection<Sale> Sales { get; }
        //public ObservableCollection<Sale> FilteredSales { get; }
        //public ICommand LoadSalesCommand { get; }
        //public ICommand FilterSalesCommand { get; }

        //private DateTime _filterDate;
        //public DateTime FilterDate
        //{
        //    get => _filterDate;
        //    set
        //    {
        //        _filterDate = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public SalesListViewModel()
        //{
        //    Sales = new ObservableCollection<Sale>();
        //    FilteredSales = new ObservableCollection<Sale>();
        //    LoadSalesCommand = new Command(async () => await LoadSalesAsync());
        //    FilterSalesCommand = new Command(FilterSales);
        //    FilterDate = DateTime.Today;
        //}

        //private async Task LoadSalesAsync()
        //{
        //    Sales.Clear();
        //    var sales = await App.Database.GetSalesAsync();
        //    foreach (var sale in sales)
        //    {
        //        Sales.Add(sale);
        //    }

        //    FilterSales();
        //}

        //private void FilterSales()
        //{
        //    FilteredSales.Clear();
        //    var filteredSales = Sales.Where(s => s.DateTime.Date == FilterDate.Date).ToList();
        //    foreach (var sale in filteredSales)
        //    {
        //        FilteredSales.Add(sale);
        //    }
        //}

        //public event PropertyChangedEventHandler? PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        public ObservableCollection<Sale> Sales { get; }
        public ObservableCollection<Sale> FilteredSales { get; }
        public ICommand LoadSalesCommand { get; }
        public ICommand FilterSalesByDayCommand { get; }
        public ICommand FilterSalesByWeekCommand { get; }

        private DateTime _filterDate;
        public DateTime FilterDate
        {
            get => _filterDate;
            set
            {
                _filterDate = value;
                OnPropertyChanged();
            }
        }

        public SalesListViewModel()
        {
            Sales = new ObservableCollection<Sale>();
            FilteredSales = new ObservableCollection<Sale>();
            LoadSalesCommand = new Command(async () => await LoadSalesAsync());
            FilterSalesByDayCommand = new Command(FilterSalesByDay);
            FilterSalesByWeekCommand = new Command(FilterSalesByWeek);
            FilterDate = DateTime.Today;
        }

        private async Task LoadSalesAsync()
        {
            Sales.Clear();
            var sales = await App.Database.GetSalesAsync();
            foreach (var sale in sales)
            {
                Sales.Add(sale);
            }

            FilterSalesByDay();
        }

        private void FilterSalesByDay()
        {
            FilteredSales.Clear();
            var filteredSales = Sales.Where(s => s.DateTime.Date == FilterDate.Date).ToList();
            foreach (var sale in filteredSales)
            {
                FilteredSales.Add(sale);
            }
        }

        private void FilterSalesByWeek()
        {
            FilteredSales.Clear();
            var startOfWeek = FilterDate.AddDays(-(int)FilterDate.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            var filteredSales = Sales.Where(s => s.DateTime >= startOfWeek && s.DateTime < endOfWeek).ToList();
            foreach (var sale in filteredSales)
            {
                FilteredSales.Add(sale);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
