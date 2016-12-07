using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.OS;

namespace HelloWorld.Droid
{
    using Android.Support.Design.Widget;
    using Android.Support.V4.App;
    using Android.Support.V4.View;

    [Activity(Icon = "@drawable/icon2")]
    public class MainActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
                                {
                                    new MovieInputFragment(),
                                };
            var titles = CharSequence.ArrayFromStringArray(new[]
                                                               {
                                                                   "Movie",
                                                                   "Other"
                                                               });

            var viewPager = this.FindViewById<ViewPager>(Resource.Id.viewpager);

			viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            // Give the TabLayout the ViewPager
            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "My Toolbar";
        }
    }
}