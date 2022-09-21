using ProjectBoard.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model
{
    public class Group:IGroup
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public string CategoryName { get; set; }

        public Group() { }

        public Group(string name, Guid categoryId, string categoryName )
        {
            Name = name;
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}
