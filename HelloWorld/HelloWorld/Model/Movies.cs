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

			string actorList;

			if (credit.Item.CastMembers.Count == 0)
			{
				actorList = "";
			}
			else
			{
				actorList = credit.Item.CastMembers[0].Name;
			}

			for (var j = 1;(j<3) && (j < credit.Item.CastMembers.Count); j++)
			{
				if (!credit.Item.CastMembers[j].Equals(null))
				{
					actorList += ", " + credit.Item.CastMembers[j].Name;
				}
			}


            var imagelink = i.PosterPath ?? "not_found";


            tmpmovie.Id = i.Id;
            tmpmovie.Title = i.Title;
            tmpmovie.Year = i.ReleaseDate.Year;
            tmpmovie.Actors = actorList;
            tmpmovie.ImageName = imagelink;
            tmpmovie.Runtime = info.Item.Runtime;
            tmpmovie.Genre = genreList;
            tmpmovie.Review = info.Item.Overview;

            _movies.Add(tmpmovie);
        }

        public List<Movie> AllMovies => _movies;
    }

}
