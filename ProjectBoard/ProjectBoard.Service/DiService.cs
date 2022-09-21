using Autofac;
using ProjectBoard.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service
{
    public class DiService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ListingService>().As<IListingService>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<GroupService>().As<IGroupService>();

            builder.RegisterType<BoardGameService>().As<IBoardGameService>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
            //ovdje idu builder.RegisterType
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<UserService>().As<IUserService>();
            base.Load(builder);
        }
    }
}
