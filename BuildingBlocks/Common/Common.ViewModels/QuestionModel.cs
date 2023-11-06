using Common.ViewModels.Validators;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels
{
    public class QuestionModel
    {
        public long Id { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category for the question.")]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        [Required]
        public string? CorrectAnswer { get; set; }

        [MinIncorrectAnswersCount(1, ErrorMessage = "The question requires at least one incorrect answer.")]
        public IEnumerable<string?>? IncorrectAnswers { get; set; }
    }
}