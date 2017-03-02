using demoCgodoi.Models;

namespace demoCgodoi.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Dispositivo Dispositivo { get; set; }
		public ItemDetailViewModel(Dispositivo dispositivo = null)
		{
            Title = dispositivo.Nombre;
            Dispositivo = dispositivo;
		}

		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}