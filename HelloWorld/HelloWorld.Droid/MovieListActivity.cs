using System.Collections.Generic;
using Android.App;
using Android.OS;
using Newtonsoft.Json;
using Android.Widget;

using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using DM.MovieApi;
using HelloWorld.Model;

namespace HelloWorld.Droid
{
	[Activity(Theme = "@style/MyTheme", Label = "Movie list")]
    public class MovieListActivity : Activity
    {
		List<Movie> movieList;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //get info from bundle or intent
			this.SetContentView(Resource.Layout.MovieList);

            var jsonStr = Intent.GetStringExtra("movieList");
            movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
            
			var listview = this.FindViewById<ListView>(Resource.Id.namelistview);
			listview.Adapter = new MovieListAdapter(this, movieList);

			listview.ItemClick += clickHandler;

			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = this.GetString(Resource.String.ToolbarTitle);
        }

		void clickHandler(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(DetailActivity));
			intent.PutExtra("movie", JsonConvert.SerializeObject(movieList[e.Position]));
			StartActivity(intent);
		}
    }
}