using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace HelloWorld.Droid
{
	public class MovieListAdapter : BaseAdapter<Movie>
    {
        private readonly Activity _context;
        private const string ImageUrl = "http://image.tmdb.org/t/p/w92";

        private readonly List<Movie> _movieList;

        public MovieListAdapter(Activity context, List<Movie> movieList)
        {
            _context = context;
            _movieList = movieList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
            }

            var movie = _movieList[position];
            view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title + " (" + movie.Year + ")";
            view.FindViewById<TextView>(Resource.Id.actors).Text = movie.Actors;

            var imageview = view.FindViewById<ImageView>(Resource.Id.image);

            if (movie.ImageName == "not_found")
            {
                var resourceId = _context.Resources.GetIdentifier(
                    movie.ImageName,
                    "drawable",
                    _context.PackageName);

                imageview.SetBackgroundResource(resourceId);
            }
            else
            {
                var im = ImageUrl + movie.ImageName;
                Picasso.With(_context).Load(im).Into(imageview);

            }

            return view;
        }

		public override int Count
        {
            get
            {
                return _movieList.Count;
            }
        }

        public override Movie this[int position]
        {
            get
            {
                return _movieList[position];
            }
        }


    }
}