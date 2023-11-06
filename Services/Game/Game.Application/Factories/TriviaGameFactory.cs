using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;

namespace Game.Application.Factories
{
    public abstract class TriviaGameFactory
    {
        protected readonly ITriviaGamesRepository GamesRepository;
        private readonly IQuestionsRepository _questionsRepository;
        public TriviaGameFactory(ITriviaGamesRepository gamesRepository, IQuestionsRepository questionsRepository)
        {
            GamesRepository = gamesRepository;
            _questionsRepository = questionsRepository;
        }

        public async Task<TriviaGame> Create(string name, int categoryId)
        {
            var questions = await _questionsRepository.GetQuestionsForOneCategoryIncludeAnswersAsync(categoryId);
            var game = NewGame();
            game.Name = name;

            var questionCount = 0;
            foreach (var question in questions)
            {
                game.AddGameQuestion(question);
                questionCount++;
            }

            if (questionCount < 9)
                throw new Exception("Should have at least 10 available questions in the database");

            game = await Save(game);

            return game;
        }

        protected abstract Task<TriviaGame> Save(TriviaGame newGame);
        protected abstract TriviaGame NewGame();
    }
}
