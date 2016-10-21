using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;
using ZXing.Net.Mobile.Forms;

namespace Prototype
{
    public partial class Login : ContentPage
    {
        IToastNotificator notificator;
        public Login()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            await Navigation.PushAsync(scanPage);
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            User user = await App.Manager.Login(username.Text, password.Text);
            if (user != null)
            {
                notificator.Notify(ToastNotificationType.Success, "FREEPOS", "Ingreso exitoso", TimeSpan.FromSeconds(2));
                await Navigation.PushAsync(new StoreForm());
            }
            else
            {
                await notificator.Notify(ToastNotificationType.Error, "FREEPOS", "Correo o contraseña incorrectos", TimeSpan.FromSeconds(2));
            }
        }

        private async void OnSignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

    }
}
