using Turismo.MVVM.ViewModels;

namespace Turismo.MVVM.Views;

public partial class AddSale : ContentPage
{
	public AddSale()
	{
		InitializeComponent();
        var viewModel = new AddSaleViewModel();
        viewModel.DisplayAlertAction = async () => await DisplayAlert("Éxito", "La venta se ha guardado correctamente.", "OK");
        BindingContext = viewModel;
    }
}