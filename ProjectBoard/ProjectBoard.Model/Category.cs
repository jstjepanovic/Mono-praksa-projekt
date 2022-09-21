using ProjectBoard.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model
{
    public class Category : ICategory
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category(Guid categoryId, string name, string description)
        {
            this.CategoryId = categoryId;
            this.Name = name;
            this.Description = description;
        }
        public Category()
        {

        }
    }
}
