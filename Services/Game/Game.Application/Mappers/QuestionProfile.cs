using AutoMapper;
using Common.ViewModels;
using Game.Domain.Entities;

namespace Game.Application.Mappers
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionModel>()
                .ForMember(model => model.CorrectAnswer, opt => opt.MapFrom(data => data.Answers.First(a => a.Id == data.CorrectAnswerId).Text))
                .ForMember(model => model.IncorrectAnswers, opt => opt.MapFrom(data => data.Answers.Where(a => a.Id != data.CorrectAnswerId).Select(a => a.Text)))
                .ForMember(model => model.CategoryName, opt => opt.MapFrom(data => data.Category != null ? data.Category.Name : string.Empty))
                .ReverseMap();
        }
    }
}
