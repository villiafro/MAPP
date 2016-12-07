﻿using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace HelloWorld.iOS.Controllers
{
	using System.Threading.Tasks;
	using HelloWorld;

	using UIKit;
	public class MovieCollectionController : UITableViewController
    {
        private List<Movie> _movieList;
        private SetMovieInfo setMovieInfo;

        public MovieCollectionController(List<Movie> movieList)
        {
            setMovieInfo = new SetMovieInfo();
            this._movieList = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
        }

		public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.FromRGB(0.714f, 0.98f, 1);
            this.Title = "Top Rated Movies";

			var spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			spinner.Frame = new CGRect(20, 10, this.View.Bounds.Width - 2 * 20, 50);
			this.View.AddSubview(spinner);
			spinner.StartAnimating();

			var movieApi = MovieDbFactory.Create<DM.MovieApi.MovieDb.Movies.IApiMovieRequest>().Value;
			ApiSearchResponse<MovieInfo> res = await movieApi.GetTopRatedAsync();

			foreach (var i in res.Results)
			{
				Movie movie = new Movie();
				await setMovieInfo.setInfo(i, movieApi, movie);
				this._movieList.Add(movie);
			}

            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);

            spinner.StopAnimating();
			this.TableView.ReloadData();
        }

        private void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new DetailController(this._movieList[row]), true);
        }
    }
}
