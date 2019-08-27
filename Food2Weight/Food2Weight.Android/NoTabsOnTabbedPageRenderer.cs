using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using Food2Weight.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(NoTabsOnTabbedPageRenderer))]
namespace Food2Weight.Droid
{
    /// <summary>
    /// See this page for more details:
    /// https://stackoverflow.com/questions/48658921/tabbedpage-hide-all-tabs
    /// Currently this disable all tabs for all TabbedPages.
    /// Consider rework to only hide tabs for special kind of TabbedPage (derived).
    /// </summary>
    public class NoTabsOnTabbedPageRenderer : TabbedPageRenderer
    {
        private TabLayout tabsLayout;

        public NoTabsOnTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            for (int i = 0; i < ChildCount; ++i)
            {
                Android.Views.View view = GetChildAt(i);
                if (view is TabLayout layout)
                {
                    tabsLayout = layout;
                }
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            tabsLayout.Visibility = ViewStates.Gone;
            base.OnLayout(changed, l, t, r, b);
        }
    }
}