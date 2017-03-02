using System;
using System.Json;
using demoCgodoi.Services;
using demoCgodoi.Models;

using Xamarin.Forms;

namespace demoCgodoi.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Dispositivo  Dispositivo { get; set; }

		public NewItemPage()
		{
			InitializeComponent();

            Dispositivo = new Dispositivo
            {
                Id_Usuario = VariablesStatic.UsuarioID,
				Nombre  = "Nombre Dispositivo",
                Descripcion = "Descripcion"
			};

			BindingContext = this;
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			

            var  webapiOk = await new WebApiConection().WebApiGuardaDispositivo(Dispositivo, true);
            if (webapiOk)
            {
                messageLabel.Text = "Dispositivo Ingresado";
                MessagingCenter.Send(this, "AddDispositivo", Dispositivo);
                await Navigation.PopAsync();
            }else
            {
                messageLabel.Text = "Error en ingreso de dispositivo, favor reintente mas tarde";
            }

            
		}
	}
}