
using demoCgodoi.ViewModels;
using demoCgodoi.Services;

using Xamarin.Forms;

namespace demoCgodoi.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

        private async  void Button_Clicked(object sender, System.EventArgs e)
        {
            messageLabelItem.Text = "Eliminando dispositivo, favor espere un momento...";

            var webapiOk = await new WebApiConection().WebApiEliminaDispositivo(this.viewModel.Dispositivo.Id);
            if (webapiOk)
            {
                messageLabelItem.Text = "";
                await Navigation.PopAsync();
            }
            else
            {
                messageLabelItem.Text = "Error en eliminado de dispositivo, favor reintente mas tarde";
            }

        }
    }
}
