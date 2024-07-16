using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.MVVM.Models;

namespace Turismo.Services
{
    public class DatabaseService
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Sale>().Wait();
        }

        public Task<List<Sale>> GetSalesAsync()
        {
            return _database.Table<Sale>().ToListAsync();
        }

        public Task<Sale> GetSaleAsync(int id)
        {
            return _database.Table<Sale>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSaleAsync(Sale sale)
        {
            if (sale.Id != 0)
            {
                return _database.UpdateAsync(sale);
            }
            else
            {
                return _database.InsertAsync(sale);
            }
        }

        public Task<int> DeleteSaleAsync(Sale sale)
        {
            return _database.DeleteAsync(sale);
        }
    }
}
