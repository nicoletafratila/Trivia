using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;

namespace Game.Application.Factories
{
    public class InfiniteGameFactory : TriviaGameFactory
    {
        public InfiniteGameFactory(ITriviaGamesRepository gamesRepository, IQuestionsRepository questionsRepository)
            : base(gamesRepository, questionsRepository) { }

        protected override async Task<TriviaGame> Save(TriviaGame triviaGame)
        {
            var head = triviaGame.CurrentGameQuestion;

            //move to the last element in the list
            while (triviaGame.CurrentGameQuestion != null && head != triviaGame.CurrentGameQuestion.NextGameQuestion)
                triviaGame.AdvanceToNextQuestion();

            //break the circular list so that the records could be saved. Save
            var temp = triviaGame.CurrentGameQuestion;
            if (temp != null)
            {
                temp.NextGameQuestion = null;
                var game = await GamesRepository.AddAsync(triviaGame);

                //redo the link between last and first node.Update
                temp.NextGameQuestion = head;
                await GamesRepository.UpdateAsync(game);

                return game;
            }

            return triviaGame;
        }

        protected override TriviaGame NewGame()
        {
            return new InfiniteQuestionsTriviaGame();
        }
    }
}
