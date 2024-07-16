using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Turismo.MVVM.Models
{
    public class Sale : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Artisan { get; set; }

        public DateTime DateTime { get; set; }

        private float _price;
        public float Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Commission));
                OnPropertyChanged(nameof(Taxes));
                OnPropertyChanged(nameof(Total));
            }
        }

        public float Commission => Price * 0.036f;
        public float Taxes => Commission * 0.16f;
        public float Total => Price - (Commission + Taxes);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
