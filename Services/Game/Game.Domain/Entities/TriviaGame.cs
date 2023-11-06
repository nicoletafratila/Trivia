using Game.Domain.Common;

namespace Game.Domain.Entities
{
    public abstract class TriviaGame : Entity<long>, IAggregateRoot
    {
        public string? Name { get; set; }

        private readonly List<ChatMessage> _chatMessages = new();
        public IReadOnlyCollection<ChatMessage> ChatMessages => _chatMessages.AsReadOnly();
        
        private readonly HashSet<GameParticipant>? _gameParticipants = new();
        public virtual IReadOnlyCollection<GameParticipant>? GameParticipants => _gameParticipants;
        
        public GameQuestion? CurrentGameQuestion { get; set; }
        
        protected TriviaGame() {
        }

        public void AddChatMessage(string playerNickName, string message)
        {
            var chatMsg = new ChatMessage(playerNickName, message, DateTime.UtcNow);
            _chatMessages.Add(chatMsg);
        }

        public void AdvanceToNextQuestion()
        {
            if (CurrentGameQuestion?.NextGameQuestion != null)
            CurrentGameQuestion = CurrentGameQuestion?.NextGameQuestion;
        }

        //give the ability to add new questions to an already created/running game
        public abstract void AddGameQuestion(Question question);
    }
}