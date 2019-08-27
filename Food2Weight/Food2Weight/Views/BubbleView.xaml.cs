using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food2Weight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BubbleView : ContentView
    {
        public BubbleView()
        {
            InitializeComponent();
        }

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly BindableProperty HeaderProperty =
                BindableProperty.Create("Header", typeof(string), typeof(View), string.Empty, validateValue: IsValidValue);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(View), string.Empty, validateValue: IsValidValue);

        static bool IsValidValue(BindableObject view, object value) => true;
    }
}