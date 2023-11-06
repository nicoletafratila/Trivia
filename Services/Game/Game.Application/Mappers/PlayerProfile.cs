using AutoMapper;
using Common.ViewModels;
using Game.Domain.Entities;

namespace Game.Application.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerModel>()
                .ReverseMap();
        }    
    }
}
