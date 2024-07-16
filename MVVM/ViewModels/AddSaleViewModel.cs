using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Turismo.MVVM.Models;

namespace Turismo.MVVM.ViewModels
{
    public class AddSaleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Artisans { get; set; }
        public Sale CurrentSale { get; set; }
        public ICommand SaveSaleCommand { get; }

        public Action? DisplayAlertAction { get; set; }

        public AddSaleViewModel()
        {
            Artisans = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                Artisans.Add($"Artesano {i}");
            }
            CurrentSale = new Sale
            {
                DateTime = DateTime.Today
            };

            SaveSaleCommand = new Command(async () => await SaveSaleAsync());
        }

        private async Task SaveSaleAsync()
        {
            await App.Database.SaveSaleAsync(CurrentSale);
            DisplayAlertAction?.Invoke();
            ClearFields();
        }

        private void ClearFields()
        {
            OnPropertyChanged(nameof(CurrentSale));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
