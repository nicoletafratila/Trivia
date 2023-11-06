using AutoMapper;
using Common.ViewModels;
using Game.Domain.Entities;

namespace Game.Application.Mappers
{
    public class PlayerHistoryProfile : Profile
    {
        public PlayerHistoryProfile()
        {
            CreateMap<PlayerHistory, PlayerHistoryModel>()
                .ForMember(model => model.Question, opt => opt.MapFrom(data => data.Question!.Id))
                .ForMember(model => model.Player, opt => opt.MapFrom(data => data.Player!.Id))
                .ReverseMap();
        }
    }
}
