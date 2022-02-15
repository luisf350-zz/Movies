using Movies.Domain.Contract;
using Movies.Entities.Entities;
using Movies.Repositories.Repositories;

namespace Movies.Domain.Implementation
{
    public class MovieDomain : DomainBase<Movie>, IMovieDomain
    {
        public MovieDomain(IMovieRepository repository) : base(repository)
        {

        }
    }
}