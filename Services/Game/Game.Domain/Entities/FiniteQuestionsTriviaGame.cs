namespace Game.Domain.Entities;
public class FiniteQuestionsTriviaGame: TriviaGame
{
    public override void AddGameQuestion(Question question)
    {
        if (CurrentGameQuestion is null)
        {
            var newGameQuestion = new GameQuestion(question.Id, question.CorrectAnswerId);
            CurrentGameQuestion = newGameQuestion;
        }
        //most probably changes won't be made to the selected questions for a running finite game, but to be safe
        //add a new question between the current question and it's next question to not override the current question
        else
        {
            var temp = CurrentGameQuestion.NextGameQuestion;
            var newGameQuestion = new GameQuestion(question.Id, question.CorrectAnswerId);
            newGameQuestion.NextGameQuestion = temp;
            CurrentGameQuestion.NextGameQuestion = newGameQuestion;
        }
    }
    
}