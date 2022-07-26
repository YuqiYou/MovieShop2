using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TrailerUrl { get; set; }
        public int MovieId { get; set; }

        //navigation property
        //1 movie has multiple trailer
        //1 trailer belongs 1 movie
        public Movie Movie { get; set; }
    }
}
