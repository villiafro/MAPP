using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using DM.MovieApi;
using Newtonsoft.Json;
using HelloWorld.Model;

using Fragment = Android.Support.V4.App.Fragment;

namespace HelloWorld.Droid
{
    public class MovieInputFragment : Fragment
    {
        private Movies _movies;

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            MovieDbFactory.RegisterSettings(new ApiConnectionClass());
            _movies = new Movies();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);
            var rootView = inflater.Inflate(Resource.Layout.MovieInput, container, false);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = rootView.FindViewById<Button>(Resource.Id.searchButton);
            var search = rootView.FindViewById<EditText>(Resource.Id.searchTextField);
            var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.marker_progress);

            button.Click += async delegate {

				button.Enabled = false;
				search.Enabled = false;

                _movies.AllMovies.Clear();
                var manager = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(search.WindowToken, 0);

                spinner.Visibility = ViewStates.Visible;

                //get movies corresponding to request
                var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;
                DM.MovieApi.ApiResponse.ApiSearchResponse<DM.MovieApi.MovieDb.Movies.MovieInfo> response = await movieApi.SearchByTitleAsync(search.Text);


                //insert results into arrays
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

            };
            return rootView;
        }
    }
}


