using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food2Weight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWeightView : ContentPage
    {
        public AddWeightView()
        {
            InitializeComponent();
        }
        
        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("SWIPED!");
        }
    }
}