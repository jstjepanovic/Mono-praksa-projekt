using Autofac;
using ProjectBoard.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model
{
    public class DiModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Listing>().As<IListing>();
            builder.RegisterType<Genre>().As<IGenre>();
            builder.RegisterType<Category>().As<ICategory>();
            builder.RegisterType<Group>().As<IGroup>();

            //ovdje idu builder.RegisterType
            builder.RegisterType<BoardGame>().As<IBoardGame>();
            builder.RegisterType<Review>().As<IReview>();
            base.Load(builder);
        }
    }
}
