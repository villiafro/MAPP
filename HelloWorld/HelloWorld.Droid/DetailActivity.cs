
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Square.Picasso;


namespace HelloWorld.Droid
{
	[Activity(Label = "Movie Detail")]
	public class DetailActivity : Activity
	{
		private const string ImageUrl = "http://image.tmdb.org/t/p/w92";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			this.SetContentView(Resource.Layout.Detail);

			var jsonStr = Intent.GetStringExtra("movie");
			var movie = JsonConvert.DeserializeObject<Movie>(jsonStr);

			var image = FindViewById<ImageView>(Resource.Id.image);
			var title = FindViewById<TextView>(Resource.Id.title);
			var genre = FindViewById<TextView>(Resource.Id.genre);
			var overview = FindViewById<TextView>(Resource.Id.overview);


			if (movie.ImageName == "not_found")
			{
				var resourceId = this.Resources.GetIdentifier(
					movie.ImageName,
					"drawable",
					this.PackageName);

				image.SetBackgroundResource(resourceId);
			}
			else
			{
				var im = ImageUrl + movie.ImageName;
				Picasso.With(this).Load(im).Into(image);
			}

			title.Text = movie.Title + " (" + movie.Year + ")";
			genre.Text = movie.Runtime.ToString() + " min | " + movie.Genre;
			overview.Text = movie.Review;

		}
	}
}
