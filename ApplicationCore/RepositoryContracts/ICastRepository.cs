using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface ICastRepository
    {
        Task<MovieCast> GetById(int id);

    }
}
