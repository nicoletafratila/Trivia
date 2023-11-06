namespace Game.Domain.Entities;
public class InfiniteQuestionsTriviaGame: TriviaGame
{
    public override void AddGameQuestion(Question question)
    {
        var newGameQuestion = new GameQuestion(question.Id, question.CorrectAnswerId);
        if ( CurrentGameQuestion is null)
        {
            CurrentGameQuestion = newGameQuestion;
            CurrentGameQuestion.NextGameQuestion = CurrentGameQuestion;
        }
        else
        {
            newGameQuestion.NextGameQuestion = CurrentGameQuestion.NextGameQuestion;
            CurrentGameQuestion.NextGameQuestion = newGameQuestion;
        }
    }
}