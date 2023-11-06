using AutoMapper;
using Common.ViewModels;
using Game.Domain.Entities;

namespace Game.Application.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>()
                .ReverseMap();
        }
    }
}
