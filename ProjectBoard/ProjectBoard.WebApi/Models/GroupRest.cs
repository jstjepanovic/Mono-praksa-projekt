using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBoard.WebApi.Models
{
    public class GroupRest
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime TimeCreated { get; set; }

        public GroupRest() { }
    }
}