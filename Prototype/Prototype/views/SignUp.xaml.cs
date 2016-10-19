using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;

namespace Prototype
{
    public partial class SignUp : ContentPage
    {
        private IToastNotificator notificator;
        private User user;
        public SignUp()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();
            user = new User() { name = "Andrés", email="a.mejia@mill.com.co", password="123456", phone="3104770718", surname="Mejía"};
            this.BindingContext = user;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            user = (User)BindingContext;

            var response = await App.Manager.SignUp(user);

            if (response != null)
            {
                await notificator.Notify(ToastNotificationType.Success, "FREEPOS", "Bienvenido a FreePOS", TimeSpan.FromSeconds(2));
                RestService.LoggedUser = user;
            }
            else
            {
                await notificator.Notify(ToastNotificationType.Error, "FREEPOS", "Ooops, ocurrió un error en el registro", TimeSpan.FromSeconds(2));
            }
        }
    }
}
