using System.Reflection;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Data
{
    public class TriviaDbContext : DbContext
    {
        public TriviaDbContext(DbContextOptions options) : base(options)
        {
        }
        private DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<GameParticipant> GameParticipants => Set<GameParticipant>();
        private DbSet<GameQuestion> GameQuestions => Set<GameQuestion>();
        public DbSet<TriviaGame> Games => Set<TriviaGame>();
        private DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<PlayerHistory> PlayerHistories => Set<PlayerHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfiniteQuestionsTriviaGame>();
            modelBuilder.Entity<FiniteQuestionsTriviaGame>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
