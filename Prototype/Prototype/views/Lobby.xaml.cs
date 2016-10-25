using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;

namespace Prototype
{
    public partial class Lobby : ContentPage
    {
        IToastNotificator notificator;
        List<Company> companies;
        List<Store> stores;
        List<Inventory> inventories;

        public Lobby()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();
        }

        protected async override void OnAppearing()
        {
            pkr_company.Items.Clear();
            pkr_store.IsEnabled = false;
            AddStore.IsEnabled = false;
            pkr_inventory.IsEnabled = false;
            AddInventory.IsEnabled = false;
            Continue.IsEnabled = false;
            companies = await App.Manager.FetchCompanies();
            if (companies == null || companies.Count < 1)
            {
                await notificator.Notify(ToastNotificationType.Warning, "FREEPOS", "No hay compañías registradas, crea una para continuar", TimeSpan.FromSeconds(2));
                return;
            }
            foreach (Company c in companies)
            {
                pkr_company.Items.Add(c.name);
            }
        }

        private async void OnCompanyChanged(object sender, EventArgs e)
        {
            pkr_store.Items.Clear();
            pkr_inventory.Items.Clear();
            pkr_inventory.IsEnabled = false;
            AddInventory.IsEnabled = false;
            Continue.IsEnabled = false;
            if (pkr_company.SelectedIndex == -1)
            {
                pkr_store.IsEnabled = false;
            }
            else
            {
                RestService.selectedCompany = companies.ElementAt<Company>(pkr_company.SelectedIndex);
                stores = await App.Manager.FetchStores();
                if(stores == null || stores.Count < 1)
                {
                    pkr_store.IsEnabled = false;
                    AddStore.IsEnabled = true;
                    await notificator.Notify(ToastNotificationType.Warning, "FREEPOS", "No hay tiendas registradas para la compañía seleccionada, crea una para continuar", TimeSpan.FromSeconds(2));
                    return;
                }
                foreach(Store s in stores)
                {
                    pkr_store.Items.Add(s.name);
                }
                pkr_store.IsEnabled = true;
                AddStore.IsEnabled = true;
            }
        }

        private async void OnAddCompany(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanyForm());
        }

        private async void OnStoreChanged(object sender, EventArgs e)
        {
            pkr_inventory.Items.Clear();
            Continue.IsEnabled = false;
            if (pkr_company.SelectedIndex == -1)
            {
                pkr_inventory.IsEnabled = false;
            }
            else
            {
                RestService.selectedStore = stores.ElementAt<Store>(pkr_store.SelectedIndex);
                inventories = await App.Manager.FetchInventories();
                if (inventories == null || inventories.Count < 1)
                {
                    pkr_inventory.IsEnabled = false;
                    AddInventory.IsEnabled = true;
                    await notificator.Notify(ToastNotificationType.Warning, "FREEPOS", "No hay inventarios registradss para la tienda seleccionada, crea uno para continuar", TimeSpan.FromSeconds(2));
                    return;
                }
                foreach (Inventory i in inventories)
                {
                    pkr_inventory.Items.Add(i.name);
                }
                pkr_inventory.IsEnabled = true;
                AddInventory.IsEnabled = true;
            }
        }

        private async void OnAddStore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StoreForm());
        }

        private async void OnInventoryChanged(object sender, EventArgs e)
        {
            if(pkr_inventory.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                RestService.selectedInventory = inventories.ElementAt<Inventory>(pkr_inventory.SelectedIndex);
                Continue.IsEnabled = true;
            }
        }

        private async void OnAddInventory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryForm());
        }

        private async void OnContinue(object sender, EventArgs e)
        {
            await notificator.Notify(ToastNotificationType.Success, "FREEPOS", "Configuración terminada, listo para inventario", TimeSpan.FromSeconds(2));
            await Navigation.PushAsync(new ScanPage());
        }

    }
}
