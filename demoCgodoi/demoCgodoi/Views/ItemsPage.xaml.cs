using System;

using demoCgodoi.Models;
using demoCgodoi.ViewModels;

using Xamarin.Forms;

namespace demoCgodoi.Views
{
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;

		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var dispositivo = args.SelectedItem as Dispositivo;
			if (dispositivo == null)
				return;

			await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(dispositivo)));

			// Manually deselect item
			ItemsListView.SelectedItem = null;
		}

		async void AddItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewItemPage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Dispositivos.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
	}
}
