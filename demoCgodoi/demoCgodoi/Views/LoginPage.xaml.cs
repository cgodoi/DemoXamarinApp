using System;
using demoCgodoi.Models;
using demoCgodoi.ViewModels;
using System.Json;
using Xamarin.Forms;
using System.Threading.Tasks;
using demoCgodoi.Services;

namespace demoCgodoi.Views
{
	public partial class LoginPage : ContentPage
	{
        ICredentialsService storeService;

		public LoginPage ()
		{
			InitializeComponent ();

			storeService = DependencyService.Get<ICredentialsService> ();
        }

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			string userName = usernameEntry.Text;
			string password = passwordEntry.Text;

            messageLabel.Text = "Validando credenciales...";

            var isValid = await AreCredentialsCorrect (userName, password);
			if (isValid) {
                SetHome();

            } else {
				messageLabel.Text = "Error, verifique usuario y contraseña";
				passwordEntry.Text = string.Empty;
			}
		}

		async Task<bool> AreCredentialsCorrect (string username, string password)
		{
            JsonValue mJson = await new WebApiConection().consultaWsDemo(VariablesStatic.webApiUsuario);

            return InterpretaJsonUsuario(mJson, username, password);
        }

        private Boolean InterpretaJsonUsuario(JsonValue json, string username, string password)
        {
            // Extract the array of name/value results for the field name "weatherObservation". 
            foreach (JsonValue UsuarioJson in json)
            {
                if (UsuarioJson["User"] == username && UsuarioJson["Password"]==password)
                {
                    VariablesStatic.Usuario = UsuarioJson["User"];
                    VariablesStatic.Password = UsuarioJson["Password"];
                    VariablesStatic.UsuarioID = UsuarioJson["Id"];
                    return true;
                }
            }

            return false;

        }



        async void SetHome()
        {
            messageLabel.Text = "";
            passwordEntry.Text = string.Empty;
            MessagingCenter.Send(this, "NoDispositivo", new  Dispositivo());
            var nextPage  =  new TabbedPage
            {
                Children =
                {
                    new ItemsPage() 
                    {
                        Title = "Mis Dispositvos",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new AboutPage()
                    {
                        Title = "Acerca",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    }
                }
            };
            await Navigation.PushAsync(nextPage);
        }
    }
}
