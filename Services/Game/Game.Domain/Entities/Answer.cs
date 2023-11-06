using Game.Domain.Common;

namespace Game.Domain.Entities
{
    public class Answer : Entity<Guid>
    {
        public string? Text { get; private set; }

        private Answer() { }

        public Answer(Guid id, string? text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));
            if (id == Guid.Empty) throw new ArgumentException($"{nameof(id)} should have a value other than default ");

            Text = text;
            Id = id;
        }
    }
}
