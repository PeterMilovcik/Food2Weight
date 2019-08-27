using System;
using System.ComponentModel;
using Food2Weight.ViewModels;
using Xamarin.Forms;

namespace Food2Weight.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void FloatingActionButton_OnClicked(object sender, EventArgs e)
        {
            Animate();
        }

        private void AddFoodButton_OnClicked(object sender, EventArgs e)
        {
            Animate();
        }

        private void AddWeightButton_OnClicked(object sender, EventArgs e)
        {
            Animate();
        }

        private void Animate()
        {
            if (BindingContext is MainViewModel viewModel && viewModel.IsAddButtonVisible)
            {
                uint length = 300;
                var easing = Easing.SinOut;
                AddButton.RotateTo(viewModel.IsActionMenuVisible ? 135 : 0, length, easing);
                AddButton.ScaleTo(viewModel.IsActionMenuVisible ? 0.66 : 1, length, easing);
                var red = (Color) Application.Current.Resources["Red"];
                var black = (Color) Application.Current.Resources["Black"];
                AddButton.BackgroundColor = viewModel.IsActionMenuVisible ? red : black;
                AddFoodButton.TranslateTo(viewModel.IsActionMenuVisible ? -75 : 0, 0, length, easing);
                AddWeightButton.TranslateTo(0, viewModel.IsActionMenuVisible ? -75 : 0, length, easing);
            }
        }
    }
}
