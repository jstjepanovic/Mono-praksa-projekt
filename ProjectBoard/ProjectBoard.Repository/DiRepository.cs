using Autofac;
using ProjectBoard.Repository.Common;
using System;
using System.Collections.Generic;

namespace ProjectBoard.Repository
{
    public class DiRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ListingRepository>().As<IListingRepository>();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>();

            //ovdje idu builder.RegisterType
            builder.RegisterType<BoardGameRepository>().As<IBoardGameRepository>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            base.Load(builder);
        }
    }
}
