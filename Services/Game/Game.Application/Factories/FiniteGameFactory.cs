using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;

namespace Game.Application.Factories
{
    public class FiniteGameFactory : TriviaGameFactory
    {
        public FiniteGameFactory(ITriviaGamesRepository gamesRepository, IQuestionsRepository questionsRepository)
            : base(gamesRepository, questionsRepository) { }

        protected override async Task<TriviaGame> Save(TriviaGame newGame)
        {
            //no special actions needed. Just save it.
            return await GamesRepository.AddAsync(newGame);
        }

        protected override TriviaGame NewGame()
        {
            return new FiniteQuestionsTriviaGame();
        }
    }
}
