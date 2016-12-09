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

using Fragment = Android.Support.V4.App.Fragment;

namespace HelloWorld.Droid
{
	public class OtherFragment : Fragment
	{
		private Movies _movies;
		private ProgressBar spinner;
		private View rootView;
		//private ListView;

		public override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			MovieDbFactory.RegisterSettings(new ApiConnectionClass());
			_movies = new Movies();

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);

			spinner = rootView.FindViewById<ProgressBar>(Resource.Id.marker_progress2);

			return rootView;
		}

		public void enableSpinner()
		{
			spinner.Visibility = ViewStates.Visible;
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

			var listview = rootView.FindViewById<ListView>(Resource.Id.namelistview);
			listview.Adapter = new MovieListAdapter(this.Activity, _movies.AllMovies);

			listview.ItemClick += clickHandler;

			spinner.Visibility = ViewStates.Gone;
		}

		void clickHandler(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(Context, typeof(DetailActivity));
			intent.PutExtra("movie", JsonConvert.SerializeObject(_movies.AllMovies[e.Position]));
			StartActivity(intent);
		}
	}
}