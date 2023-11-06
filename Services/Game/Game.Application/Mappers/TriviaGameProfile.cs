using AutoMapper;
using Common.ViewModels;
using Game.Domain.Entities;

namespace Game.Application.Mappers
{
    public class TriviaGameProfile : Profile
    {
        public TriviaGameProfile()
        {
            CreateMap<TriviaGame, TriviaGameModel>()
                .ForMember(model => model.Name, opt => opt.MapFrom(data => data.Name))
                .ReverseMap();
        }
    }
}
