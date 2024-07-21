using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turismo.MVVM.Models;

namespace Turismo.Services
{
    public class ExportService
    {
        public void ExportSalesToExcel(IEnumerable<dynamic> sales, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Weekly Sales");
                worksheet.Cell(1, 1).Value = "Artisan";
                worksheet.Cell(1, 2).Value = "Total Sales";
                worksheet.Cell(1, 3).Value = "Total Commission";
                worksheet.Cell(1, 4).Value = "Total Taxes";
                worksheet.Cell(1, 5).Value = "Total Earnings";

                int row = 2;
                foreach (var sale in sales)
                {
                    worksheet.Cell(row, 1).Value = sale.Artisan;
                    worksheet.Cell(row, 2).Value = sale.TotalSales;
                    worksheet.Cell(row, 3).Value = sale.TotalCommission;
                    worksheet.Cell(row, 4).Value = sale.TotalTaxes;
                    worksheet.Cell(row, 5).Value = sale.TotalEarnings;
                    row++;
                }

                workbook.SaveAs(filePath);
            }
        }
    }
}
