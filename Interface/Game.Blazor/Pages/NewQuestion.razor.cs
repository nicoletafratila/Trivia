using Common.ViewModels;
using Common.ViewModels.Pagination;
using Game.Blazor.Services.Interfaces;
using Game.Blazor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Game.Blazor.Pages
{
    public partial class NewQuestion
    {
        [Inject]
        protected ICategoryService? CategoryService { get; set; }

        [Inject]
        protected IQuestionService? QuestionService { get; set; }

        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        [CascadingParameter(Name = "ErrorComponent")]
        protected IErrorComponent? ErrorComponent { get; set; }

        [Parameter]
        public QueryParameters? QueryParameters { get; set; } = new();

        [Parameter]
        public string? CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    Question!.CategoryId = !string.IsNullOrWhiteSpace(_categoryId) ? int.Parse(_categoryId) : 0;
                    StateHasChanged();
                }
            }
        }
        private string? _categoryId;
        public IEnumerable<CategoryModel>? Categories { get; set; }

        public QuestionModel? Question { get; set; }
        public PagedList<QuestionModel>? Questions { get; set; }

        private AnswerModel? _answer;
        public AnswerModel? Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                if (_answer != value)
                {
                    _answer = value;
                    StateHasChanged();
                }
            }
        }
        public IList<AnswerModel>? Answers { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ClearFormAsync();
        }

        protected async Task SaveAsync()
        {
            Question!.CategoryId = CategoryId != null ? int.Parse(CategoryId) : 0;
            if (Answers != null)
            {
                var correctAnswer = Answers.FirstOrDefault(item => item.IsCorrect);
                Question.CorrectAnswer = correctAnswer != null ? correctAnswer.Text : string.Empty;
                Question.IncorrectAnswers = Answers.Where(item => !item.IsCorrect).Select(item => item.Text);
                if (QuestionService != null)
                {
                    await QuestionService.SaveAsync(Question);
                }
                await ClearFormAsync();
            }
        }

        protected async Task DeleteAsync(QuestionModel item)
        {
            if (JSRuntime != null)
            {
                if (!await JSRuntime.Confirm($"Are you sure you want to delete the question '{item.Text}'?"))
                    return;
            }

            if (QuestionService != null && ErrorComponent != null)
            {
                var response = await QuestionService.DeleteAsync(item.Id);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    ErrorComponent.ShowError("Delete", response);
                }
            }
            await RefreshAsync();
        }

        protected bool CanAddAnswer
        {
            get
            {
                return Answer != null && !string.IsNullOrWhiteSpace(Answer.Text);
            }
        }

        protected void AddAnswer()
        {
            if (CanAddAnswer)
            {
                if (Answers != null && Answer != null && !string.IsNullOrWhiteSpace(Answer.Text))
                {
                    AnswerModel? item = Answers.FirstOrDefault(i => Answer.Text.Equals(i.Text, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(i.Text));
                    if (item == null)
                    {
                        item = new AnswerModel();
                        item.Text = Answer.Text;
                        Answers.Add(item);
                    }
                    else
                    {
                        item.Text = Answer.Text;
                    }
                    Answer = new AnswerModel();
                }
            }
        }

        protected void DeleteAnswer(AnswerModel item)
        {
            if (Answers != null && item != null && !string.IsNullOrWhiteSpace(item.Text))
            {
                AnswerModel? itemToDelete = Answers.FirstOrDefault(i => item.Text.Equals(i.Text, StringComparison.OrdinalIgnoreCase));
                if (itemToDelete != null)
                {
                    if (Answers != null)
                    {
                        Answers.Remove(itemToDelete);
                    }
                }
            }
        }

        private void OnCorrectAnswerChanged(AnswerModel answer, object checkedValue)
        {
            bool isChecked = (bool)checkedValue;
            if (Answers != null)
            {
                Answers.ToList().ForEach(item => item.IsCorrect = false);
                Question!.CorrectAnswer = string.Empty;

                if (isChecked)
                {
                    answer.IsCorrect = true;
                    Question.CorrectAnswer = answer.Text;
                }

                Question.IncorrectAnswers = Answers.Where(item => !item.IsCorrect).Select(item => item.Text);
            }
        }

        private async Task ClearFormAsync()
        {
            await RefreshAsync();
            Question = new QuestionModel();
            Answer = new AnswerModel();
            CategoryId = "0";
            Answers = new List<AnswerModel>() { new AnswerModel() };
        }

        private async Task RefreshAsync()
        {
            if (QueryParameters == null || QuestionService == null || CategoryService == null)
            {
                return;
            }

            Categories = await CategoryService.GetAllAsync();
            Questions = await QuestionService.GetAllAsync(QueryParameters);
        }

        private async Task OnPageChangedAsync(int pageNumber)
        {
            if (QueryParameters == null || QuestionService == null)
            {
                return;
            }

            QueryParameters.PageNumber = pageNumber;
            Questions = await QuestionService.GetAllAsync(QueryParameters);
            StateHasChanged();
        }

        //Calculate the the position of each user in the top. 
        //Normally the index would reset on each case so we need to handle multiple cases.
        private int CalculateIndex(int indexFromView)
        {
            var index = 0;

            if (Questions is { Metadata.PageNumber: 1 })
            {
                index = indexFromView + 1;
            }
            else
            {
                if (Questions != null && Questions.Metadata != null)
                {
                    index = Questions.Metadata.PageSize * (Questions.Metadata.PageNumber - 1) + indexFromView + 1;
                }
            }

            return index;
        }
    }
}
