using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Timers;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;
using Common.ViewModels;

namespace Game.Blazor.Pages
{
    public partial class GamePage
    {
        [Parameter]
        public int GameId { get; set; }

        [Inject]
        public IQuestionService? QuestionService { get; set; }

        [Inject]
        public IPlayerService? PlayerService { get; set; }

        [Inject]
        public IPlayerHistoryService? PlayerHistoryService { get; set; }

        string? currentQuestion { get; set; }
        float progressbarValue { get; set; }
        float progressbarValuePercentage { get; set; }
        string? result { get; set; }
        List<string?>? answerList { get; set; }
        string? correctAnswerId { get; set; }
        float questionTime { get; set; }

        private QuestionModel? question;
        System.Timers.Timer? timer;
        Stopwatch stopWatch = new Stopwatch();
        bool buttonsDisabled = false;
        string[]? btnClass;
        bool render = false;
        string warningVisible = "hidden";

        HubConnection? hubConnection;

        private static Random rng = new Random();

        string userID = string.Empty;
        bool answered = true;

        public PlayerHistoryModel? PlayerHistoryModel { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            questionTime = 0.0f;
            question = await QuestionService!.GetCurrentQuestion(GameId);
            answerList = question!.IncorrectAnswers!.ToList();
            answerList.Add(question.CorrectAnswer);
            currentQuestion = question.Text;

            correctAnswerId = question.CorrectAnswer;

            btnClass = ResetBtnClasses();

            progressbarValuePercentage = 100;
            progressbarValue = questionTime;

            hubConnection = new HubConnectionBuilder()
           .WithUrl("https://localhost:7065/newcurrentquestionhub")
           .Build();

            hubConnection.On<float>("NewCurrentQuestion", timeLeft =>
            {
                questionTime = timeLeft;
                InvokeAsync(GetNewQuestion);

            });
            hubConnection.On("UserConnected", () => {
                DisableFormUntilNextQuestion();
                return Task.CompletedTask;
            });

            await hubConnection.StartAsync();

            render = true;


            var authstate = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            userID = authstate.User.Claims.First(c => c.Type == "sub")?.Value!;
        }
        private void DisableFormUntilNextQuestion()
        {
            buttonsDisabled = true;
            warningVisible = "visible";
            StateHasChanged();
        }

        private void DecrementCount()
        {
            if (progressbarValue > 0)
            {
                progressbarValue--;
                progressbarValuePercentage = 100 / (questionTime / progressbarValue);
                StateHasChanged();

            }
        }

        private async Task GetNewQuestion()
        {
            //get new question from API
            question = await QuestionService!.GetCurrentQuestion(GameId);
            answerList = question!.IncorrectAnswers!.ToList();
            answerList.Add(question.CorrectAnswer);
            currentQuestion = question.Text;
            correctAnswerId = question.CorrectAnswer;

            //randmoize answer list
            ShuffleAnswers();

            //reset progress bar
            progressbarValuePercentage = 100;
            progressbarValue = questionTime;
            render = true;

            //update player if no response was given
            if (!answered)
            {
                await PlayerService!.UpdatePerformance(userID, false, TimeSpan.Zero);                
            }

            //reset buttons
            btnClass = ResetBtnClasses();
            buttonsDisabled = false;

            //reset warning message
            warningVisible = "hidden";

            //reset stopwatch
            stopWatch.Restart();

            //reset response flag
            answered = false;

            //reset player history
            PlayerHistoryModel = new PlayerHistoryModel();
            StateHasChanged();
        }

        private void ShuffleAnswers()
        {
            int n = answerList!.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = answerList[k]!;
                answerList[k] = answerList[n];
                answerList[n] = value;
            }
        }

        private string[] ResetBtnClasses()
        {
            var btnClasses = new string[answerList!.Count];
            for (int i = 0; i < answerList.Count; i++)
            {
                btnClasses[i] = "btn btn-secondary";
            }
            return btnClasses;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += OnTimerInterval!;
                timer.AutoReset = true;
                // Start the timer
                timer.Enabled = true;
            }
            base.OnAfterRender(firstRender);
        }

        private void OnTimerInterval(object sender, ElapsedEventArgs e)
        {
            DecrementCount();
        }

        async void AnswerButtonPressed(int nrOfButton)
        {

            buttonsDisabled = true;
            PlayerHistoryModel = new PlayerHistoryModel();
            PlayerHistoryModel.Player = userID;
            PlayerHistoryModel.AnswerDate = DateTime.UtcNow;
            PlayerHistoryModel.AnswerTime = stopWatch.Elapsed;
            PlayerHistoryModel.Question = question!.Id;

            if (answerList![nrOfButton] == correctAnswerId)
            {
                PlayerHistoryModel.AnsweredCorrectly = true;
                await PlayerHistoryService!.SaveAsync(PlayerHistoryModel);

                await PlayerService!.UpdatePerformance(userID, true, stopWatch.Elapsed);                

                btnClass![nrOfButton] = "btn btn-success";             
            }
            else
            {
                PlayerHistoryModel.AnsweredCorrectly = false;
                await PlayerHistoryService!.SaveAsync(PlayerHistoryModel);

                await PlayerService!.UpdatePerformance(userID, false, TimeSpan.Zero);

                var correctAnswerIndex = answerList.IndexOf(answerList.FirstOrDefault(x => x == correctAnswerId));
                btnClass![correctAnswerIndex] = "btn btn-success";
                btnClass![nrOfButton] = "btn btn-danger";
            }
            answered = true;
            StateHasChanged();
        }
    }
}
