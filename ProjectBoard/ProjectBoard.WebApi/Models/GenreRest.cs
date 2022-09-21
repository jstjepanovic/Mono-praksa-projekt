using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBoard.WebApi.Models
{
    public class GenreRest
    {
        public string Name { get; set; }
        public Guid GenreId { get; set; }
        public GenreRest()
        {

        }
    }
}