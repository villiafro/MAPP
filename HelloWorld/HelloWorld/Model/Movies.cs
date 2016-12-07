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

            var actors = new string[3];
            string actors3;

            if (credit.Item.CastMembers.Count == 0)
            {
                actors3 = "";
            }
            else
            {
                var j = 0;
                var k = 0;
                while ((j < 3) && (k < credit.Item.CastMembers.Count))
                {
                    if (!credit.Item.CastMembers[k].Equals(null))
                    {
                        actors[k] = credit.Item.CastMembers[k].Name;
                        j++;
                    }
                    k++;
                }

                actors3 = actors[0] + ", " + actors[1] + ", " + actors[2];

            }

            var imagelink = i.PosterPath ?? "not_found";


            tmpmovie.Id = i.Id;
            tmpmovie.Title = i.Title;
            tmpmovie.Year = i.ReleaseDate.Year;
            tmpmovie.Actors = actors3;
            tmpmovie.ImageName = imagelink;
            tmpmovie.Runtime = info.Item.Runtime;
            tmpmovie.Genre = genreList;
            tmpmovie.Review = info.Item.Overview;

            _movies.Add(tmpmovie);
        }

        public List<Movie> AllMovies => _movies;
    }

}
