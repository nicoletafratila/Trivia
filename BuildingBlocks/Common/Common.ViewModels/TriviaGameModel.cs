using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels
{
    public class TriviaGameModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category for the game.")]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Please select a type for the game.")]
        public GameType Type { get; set; }
    }
}
