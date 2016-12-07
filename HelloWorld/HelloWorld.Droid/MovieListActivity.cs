using System.Collections.Generic;
using Android.App;
using Android.OS;
using Newtonsoft.Json;

namespace HelloWorld.Droid
{
    [Activity(Label = "List of Movies")]
    public class MovieListActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //get info from bundle or intent

            var jsonStr = Intent.GetStringExtra("movieList");
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
            ListAdapter = new MovieListAdapter(this, movieList);
        }
    }
}