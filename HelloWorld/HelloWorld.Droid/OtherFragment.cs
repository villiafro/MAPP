using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using DM.MovieApi;
using Newtonsoft.Json;
using HelloWorld.Model;
using System.Threading.Tasks;
using Android.App;

using System.Collections.Generic;

using Fragment = Android.Support.V4.App.Fragment;

namespace HelloWorld.Droid
{
	public class OtherFragment : Fragment
	{
		private Movies _movies;
		private ProgressBar spinner;

		public override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			MovieDbFactory.RegisterSettings(new ApiConnectionClass());
			_movies = new Movies();

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			var rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);

			//var button = rootView.FindViewById<Button>(Resource.Id.topButton);
			spinner = rootView.FindViewById<ProgressBar>(Resource.Id.marker_progress2);

			//button.Click += async delegate
			//{

			//	button.Enabled = false;
			//	_movies.AllMovies.Clear();

				spinner.Visibility = ViewStates.Visible;

			/*	var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;
				DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.GetTopRatedAsync();

				foreach (var i in response.Results)
				{
					var resp = await movieApi.GetCreditsAsync(i.Id);
					var response2 = await movieApi.FindByIdAsync(i.Id);
					var tmpmovie = new Movie();

					_movies.AddMovie(i, resp, response2, tmpmovie);
				}*/

				/*var intent = new Intent(Context, typeof(MovieListActivity));
				intent.PutExtra("movieList", JsonConvert.SerializeObject(_movies.AllMovies));
				StartActivity(intent);*/

				//spinner.Visibility = ViewStates.Gone;
			//};
			return rootView;
		}

		public async Task FetchTopRatedMovies()
		{


			_movies.AllMovies.Clear();

			var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;
			DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.GetTopRatedAsync();

			foreach (var i in response.Results)
			{

				var resp = await movieApi.GetCreditsAsync(i.Id);
				var response2 = await movieApi.FindByIdAsync(i.Id);

				var tmpmovie = new Movie();

				_movies.AddMovie(i, resp, response2, tmpmovie);
			}

			var intent = new Intent(Context, typeof(MovieListActivity));
			intent.PutExtra("movieList", JsonConvert.SerializeObject(_movies.AllMovies));
			StartActivity(intent);

			spinner.Visibility = ViewStates.Gone;
		}
	}
}