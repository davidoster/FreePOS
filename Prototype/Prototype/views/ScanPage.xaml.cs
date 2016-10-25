using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Plugin.Toasts;

namespace Prototype
{
    public partial class ScanPage : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        IToastNotificator notificator;

        public ScanPage() : base()
        {
            InitializeComponent();
            notificator = DependencyService.Get<IToastNotificator>();
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(async () =>
           {
               zxing.IsAnalyzing = false;
               await DisplayAlert("Scanned Barcode", result.Text, "OK");
               await Navigation.PopAsync();
           });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            Content = grid;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}
