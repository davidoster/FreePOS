using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;

namespace Prototype
{
    public partial class StoreForm : ContentPage
    {
        IToastNotificator notificator;
        Store store;
        List<Company> companies;

        public StoreForm()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();
            store = new Store();
            this.BindingContext = store;
        }

        protected async override void OnAppearing()
        {
            companies = await App.Manager.FetchCompanies();
            if (companies == null || companies.Count < 1)
                return;
            foreach (Company c in companies)
            {
                pkr_company.Items.Add(c.name);
            }
        }

        private async void OnAddCompany(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanyForm());
        }

        private async void OnSave(object sender, EventArgs e)
        {
            store = (Store)this.BindingContext;

            if (pkr_company.SelectedIndex != -1)
            {
                store.companies_idcompanies = RestService.selectedCompany.idcompanies;
                var response = await App.Manager.AddStore(store);

                if (response != null)
                {
                    await notificator.Notify(ToastNotificationType.Success, "FREEPOS", "Tienda creada con éxito", TimeSpan.FromSeconds(2));
                }
                else
                {
                    await notificator.Notify(ToastNotificationType.Error, "FREEPOS", "Ooops, ocurrió un error en el registro", TimeSpan.FromSeconds(2));
                }
            }
        }

        private void OnCompanyChanged(object sender, EventArgs e)
        {
            if (pkr_company.SelectedIndex == -1)
                return;
            else
            {
                RestService.selectedCompany = companies.ElementAt<Company>(pkr_company.SelectedIndex);
            }
        }
    }
}
