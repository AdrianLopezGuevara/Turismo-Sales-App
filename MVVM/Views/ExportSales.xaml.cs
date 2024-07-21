using Turismo.MVVM.ViewModels;

namespace Turismo.MVVM.Views;

public partial class ExportSales : ContentPage
{
	public ExportSales()
	{
		InitializeComponent();
        BindingContext = new ExportSalesViewModel();
    }
}