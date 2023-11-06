using Game.Domain.Common;

namespace Game.Domain.Entities
{
    public class ChatMessage : Entity<long>
    {
        public string? PlayerNickName { get; }
        public string? Message { get; }
        public DateTime Time { get; }

        public ChatMessage(string? playerNickName, string? message, DateTime time)
        {
            if (string.IsNullOrEmpty(playerNickName)) throw new ArgumentNullException(nameof(playerNickName));
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message));

            PlayerNickName = playerNickName;
            Message = message;
            Time = time;
        }

        private ChatMessage() { }
    }
}
