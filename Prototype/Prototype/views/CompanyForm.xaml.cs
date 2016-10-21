using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;

namespace Prototype
{
    public partial class CompanyForm : ContentPage
    {
        IToastNotificator notificator;
        Company company;
        public CompanyForm()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();
            company = new Company();
            this.BindingContext = company;
        }

        private async void OnSave(object sender, EventArgs e)
        {
            company = (Company)this.BindingContext;

            var response = await App.Manager.AddCompany(company);

            if (response != null)
            {
                notificator.Notify(ToastNotificationType.Success, "FREEPOS", "Empresa creada con éxito", TimeSpan.FromSeconds(2));
                await Navigation.PopAsync();
            }
            else
            {
                await notificator.Notify(ToastNotificationType.Error, "FREEPOS", "Ooops, ocurrió un error en el registro", TimeSpan.FromSeconds(2));
            }
        }
    }
}
