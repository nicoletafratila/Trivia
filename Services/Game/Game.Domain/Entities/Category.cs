using Game.Domain.Common;

namespace Game.Domain.Entities
{
    public class Category : Entity<int>, IAggregateRoot
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
