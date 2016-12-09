using System.Collections.Generic;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace HelloWorld.Model
{
    public class Movies
    {
        private readonly List<Movie> _movies;


        public Movies()
        {
            _movies = new List<Movie>();
        }

        public void AddMovie(MovieInfo i, ApiQueryResponse<MovieCredit> credit, ApiQueryResponse<DM.MovieApi.MovieDb.Movies.Movie> info, Movie tmpmovie)
        {
            string genreList;
			string actorList;

			if (credit == null)
			{
				actorList = "";
			}
			else 
			{
				if (credit.Item == null)
				{
					actorList = "";
				}
				else
				{
					if (credit.Item.CastMembers.Count == 0)
					{
						actorList = "";
					}
					else
					{
						actorList = credit.Item.CastMembers[0].Name;
					}

					for (var j = 1; (j < 3) && (j < credit.Item.CastMembers.Count); j++)
					{
						if (!credit.Item.CastMembers[j].Equals(null))
						{
							actorList += ", " + credit.Item.CastMembers[j].Name;
						}
					}

				}
			}

			if (i == null)
			{
				tmpmovie.ImageName = "not_found";
				tmpmovie.Id = 0;
				tmpmovie.Year = 0;
				tmpmovie.Title = "Title not found!";
			}
			else
			{
				tmpmovie.ImageName = i.PosterPath ?? "not_found";
				tmpmovie.Id = i.Id;
				tmpmovie.Year = i.ReleaseDate.Year;
				tmpmovie.Title = i.Title ?? "Title not found!";
			}
            
            tmpmovie.Actors = actorList;

			if (info == null)
			{
				tmpmovie.Runtime = 0;
				tmpmovie.Review = "Overview not found!";
				genreList = "";
			}
			else 
			{
				if (info.Item == null)
				{
					genreList = "";
					tmpmovie.Runtime = 0;
					tmpmovie.Review = "Overview not found!";
				}
				else
				{
					if (info.Item.Genres.Count == 0)
					{
						genreList = "";
					}
					else
					{
						genreList = info.Item.Genres[0].Name;
					}

					for (var j = 1; j < info.Item.Genres.Count; j++)
					{
						if (!info.Item.Genres[j].Equals(null))
						{
							genreList += ", " + info.Item.Genres[j].Name;
						}
					}
					tmpmovie.Runtime = info.Item.Runtime;
					tmpmovie.Review = info.Item.Overview ?? "Overview not found!";
				}
			}
            
            tmpmovie.Genre = genreList;

            _movies.Add(tmpmovie);
        }

        public List<Movie> AllMovies => _movies;
    }

}
