using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface IMovieRepository
    {

        //CRUD operation regaring movie table
        // use movie entity

        //method to get the top 30 highes grossing movies
        Task<List<Movie>> GetTop30HighestRevenueMovies();

        //method
        Task <List<Movie>> GetTop30RatedMovies();


        Task<Movie> GetById(int id);

    }
}
