using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace demoCgodoi.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "Acerca";

			OpenWebCommand = new Command(() => Device.OpenUri(new Uri(VariablesStatic.webCgodoiCv)));
		}

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public ICommand OpenWebCommand { get; }
	}
}
