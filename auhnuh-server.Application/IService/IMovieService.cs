using auhnuh_server.Domain.DTO.WebResponse.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.IService
{
    public interface IMovieService
    {
        Task<List<ListAllMovieDTO>> ListMovie();
    }
}
