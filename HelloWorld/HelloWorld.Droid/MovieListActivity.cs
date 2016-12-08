using System.Collections.Generic;
using Android.App;
using Android.OS;
using Newtonsoft.Json;


using Android.Widget;

namespace HelloWorld.Droid
{
	[Activity(Theme = "@style/MyTheme", Label = "Movie list")]
    public class MovieListActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //get info from bundle or intent
			this.SetContentView(Resource.Layout.MovieList);

            var jsonStr = Intent.GetStringExtra("movieList");
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
            
			var listview = this.FindViewById<ListView>(Resource.Id.namelistview);
			listview.Adapter = new MovieListAdapter(this, movieList);

			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = this.GetString(Resource.String.ToolbarTitle);
        }
    }
}