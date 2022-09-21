using Autofac;
using AutoMapper;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;

namespace ProjectBoard.WebApi.Models
{
    public class DiAutoMapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Listing>().AsSelf();
            builder.RegisterType<ListingRest>().AsSelf();
            builder.RegisterType<List<Listing>>().AsSelf();
            builder.RegisterType<List<ListingRest>>().AsSelf();
            builder.RegisterType<List<ListingGetRest>>().AsSelf();
            builder.RegisterType<Genre>().AsSelf();
            builder.RegisterType<GenreRest>().AsSelf();
            builder.RegisterType<Category>().AsSelf();
            builder.RegisterType<CategoryRest>().AsSelf();
            builder.RegisterType<BoardGame>().AsSelf();
            builder.RegisterType<BoardGameRest>().AsSelf();
            builder.RegisterType<Review>().AsSelf();
            builder.RegisterType<ReviewRest>().AsSelf();
            builder.RegisterType<Order>().AsSelf();
            builder.RegisterType<OrderRest>().AsSelf();
            builder.RegisterType<User>().AsSelf();
            builder.RegisterType<UserRest>().AsSelf();
            builder.RegisterType<Group>().AsSelf();
            builder.RegisterType<GroupRest>().AsSelf();


            builder.RegisterType<BoardGameGetRest>().AsSelf();
            builder.RegisterType<ReviewGetRest>().AsSelf();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ListingRest, Listing>();
                cfg.CreateMap<Listing, ListingRest>();
                cfg.CreateMap<Listing, ListingGetRest>();
                cfg.CreateMap<Genre, GenreRest>();
                cfg.CreateMap<Category, CategoryRest>();
                cfg.CreateMap<BoardGameRest, BoardGame>();
                cfg.CreateMap<ReviewRest, Review>();
                cfg.CreateMap<Order, OrderRest>();
                cfg.CreateMap<OrderRest, Order>();
                cfg.CreateMap<User, UserRest>();
                cfg.CreateMap<UserRest, User>();
                cfg.CreateMap<BoardGame, BoardGameGetRest>();
                cfg.CreateMap<Review, ReviewGetRest>();
                cfg.CreateMap<Group, GroupRest>();
                cfg.CreateMap<GroupRest, Group>();


            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}