using Turismo.MVVM.ViewModels;

namespace Turismo.MVVM.Views;

public partial class SalesList : ContentPage
{
    public SalesList()
    {
        InitializeComponent();
        var viewModel = BindingContext as SalesListViewModel;
        viewModel!.LoadSalesCommand.Execute(null);
    }

    //private void OnFilterDateChanged(object sender, DateChangedEventArgs e)
    //{
    //    var viewModel = (SalesListViewModel)BindingContext;
    //    viewModel.FilterSalesCommand.Execute(null);
    //}
    private void OnFilterDateChanged(object sender, DateChangedEventArgs e)
    {
        var viewModel = (SalesListViewModel)BindingContext;
        viewModel.FilterSalesByDayCommand.Execute(null);
    }
}