using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;

namespace Prototype
{
    public partial class InventoryForm : ContentPage
    {
        IToastNotificator notificator;
        Inventory inventory;
        List<Store> stores;

        public InventoryForm()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();
            inventory = new Prototype.Inventory();
            this.BindingContext = inventory;
        }

        protected async override void OnAppearing()
        {
            stores = await App.Manager.FetchStores();
            if (stores == null || stores.Count < 1)
                return;
            foreach (Store s in stores)
            {
                pkr_store.Items.Add(s.name);
            }
        }

        private async void OnAddStore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StoreForm());
        }

        private void OnStoreChanged(object sender, EventArgs e)
        {
            if (pkr_store.SelectedIndex == -1)
                return;
            else
                RestService.selectedStore = stores.ElementAt<Store>(pkr_store.SelectedIndex);
        }

        private async void OnSave(object sender, EventArgs e)
        {
            inventory = (Inventory)this.BindingContext;

            if(pkr_store.SelectedIndex != -1)
            {
                inventory.stores_idstores = RestService.selectedStore.idstores;
                var response = await App.Manager.AddInventory(inventory);

                if (response != null)
                {
                    await notificator.Notify(ToastNotificationType.Success, "FREEPOS", "Inventario creada con éxito", TimeSpan.FromSeconds(2));
                }
                else
                {
                    await notificator.Notify(ToastNotificationType.Error, "FREEPOS", "Ooops, ocurrió un error en el registro", TimeSpan.FromSeconds(2));
                }
            }
        }
    }
}
