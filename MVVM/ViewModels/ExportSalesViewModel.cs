using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Turismo.MVVM.Models;
using Turismo.Services;

namespace Turismo.MVVM.ViewModels
{
    public class ExportSalesViewModel : BindableObject
    {
        private DateTime _selectedDate;

        public ObservableCollection<Sale> Sales { get; set; }
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }
        public ICommand ExportCommand { get; }

        public ExportSalesViewModel()
        {
            Sales = new ObservableCollection<Sale>();
            ExportCommand = new Command(ExportSales);
            SelectedDate = DateTime.Today;
        }

        private void ExportSales()
        {
            var startOfWeek = SelectedDate.StartOfWeek(DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);

            var weeklySales = Sales
                .Where(s => s.DateTime >= startOfWeek && s.DateTime < endOfWeek)
                .GroupBy(s => s.Artisan)
                .Select(g => new
                {
                    Artisan = g.Key,
                    TotalSales = g.Sum(s => s.Price),
                    TotalCommission = g.Sum(s => s.Commission),
                    TotalTaxes = g.Sum(s => s.Taxes),
                    TotalEarnings = g.Sum(s => s.Total)
                })
                .ToList();

            var exportService = new ExportService();

            string downloadsPath = FileSystem.Current.AppDataDirectory;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                downloadsPath = Path.Combine(FileSystem.Current.AppDataDirectory, "..", "Download");
            }
            //else if (DeviceInfo.Platform == DevicePlatform.iOS)
            //{
            //    downloadsPath = Path.Combine(FileSystem.Current.AppDataDirectory, "..", "Documents", "Downloads");
            //}
            //else if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.macOS)
            //{
            //    downloadsPath = Path.Combine(FileSystem.Current.AppDataDirectory, "..", "Documents", "Downloads");
            //}
            //else if (DeviceInfo.Platform == DevicePlatform.Windows)
            //{
            //    downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            //}

            string filePath = Path.Combine(downloadsPath, "WeeklySales.xlsx");
            exportService.ExportSalesToExcel(weeklySales, filePath);
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
