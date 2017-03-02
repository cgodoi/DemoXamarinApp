using System;
using System.Diagnostics;
using System.Threading.Tasks;

using demoCgodoi.Helpers;
using demoCgodoi.Models;
using demoCgodoi.Views;

using Xamarin.Forms;

namespace demoCgodoi.ViewModels
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableRangeCollection<Dispositivo> Dispositivos { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Title = "Browse";
            Dispositivos = new ObservableRangeCollection<Dispositivo>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, Dispositivo>(this, "AddDispositivo", async (obj, dispositivo) =>
			{
				var _item = dispositivo as Dispositivo;
                Dispositivos.Add(_item);
				await DataStore.AddItemAsync(_item);
			});

            MessagingCenter.Subscribe<NewItemPage, Dispositivo>(this, "NoDispositivos", async (obj, dispositivo) =>
            {
                await ExecuteLoadItemsCommand();
            });
        }

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
                Dispositivos.Clear();
				var dispositivos = await DataStore.GetItemsAsync(true);
                Dispositivos.ReplaceRange(dispositivos);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "No fue posible desplegar los dispositivos",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}