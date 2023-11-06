﻿// <auto-generated />
using System;
using Game.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Game.Infrastructure.Migrations
{
    [DbContext(typeof(TriviaDbContext))]
    partial class TriviaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Game.Domain.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Game.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Game.Domain.Entities.ChatMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("TriviaGameId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TriviaGameId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Game.Domain.Entities.GameParticipant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<long>("LastAnsweredGameQuestionId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("Score")
                        .HasColumnType("bigint");

                    b.Property<long>("TriviaGameId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TriviaGameId");

                    b.ToTable("GameParticipants");
                });

            modelBuilder.Entity("Game.Domain.Entities.GameQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("CorrectAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("NextGameQuestionId")
                        .HasColumnType("bigint");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NextGameQuestionId");

                    b.ToTable("GameQuestions");
                });

            modelBuilder.Entity("Game.Domain.Entities.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("CurrentStreak")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("FastestAnswerTime")
                        .HasColumnType("time");

                    b.Property<int>("HighestStreak")
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalPoint")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Game.Domain.Entities.PlayerHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("AnswerTime")
                        .HasColumnType("time");

                    b.Property<bool>("AnsweredCorrectly")
                        .HasColumnType("bit");

                    b.Property<long?>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("QuestionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("PlayerHistories");
                });

            modelBuilder.Entity("Game.Domain.Entities.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<Guid>("CorrectAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Game.Domain.Entities.TriviaGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CurrentGameQuestionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentGameQuestionId");

                    b.ToTable("Games");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TriviaGame");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Game.Domain.Entities.FiniteQuestionsTriviaGame", b =>
                {
                    b.HasBaseType("Game.Domain.Entities.TriviaGame");

                    b.HasDiscriminator().HasValue("FiniteQuestionsTriviaGame");
                });

            modelBuilder.Entity("Game.Domain.Entities.InfiniteQuestionsTriviaGame", b =>
                {
                    b.HasBaseType("Game.Domain.Entities.TriviaGame");

                    b.HasDiscriminator().HasValue("InfiniteQuestionsTriviaGame");
                });

            modelBuilder.Entity("Game.Domain.Entities.Answer", b =>
                {
                    b.HasOne("Game.Domain.Entities.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Domain.Entities.ChatMessage", b =>
                {
                    b.HasOne("Game.Domain.Entities.TriviaGame", null)
                        .WithMany("ChatMessages")
                        .HasForeignKey("TriviaGameId");
                });

            modelBuilder.Entity("Game.Domain.Entities.GameParticipant", b =>
                {
                    b.HasOne("Game.Domain.Entities.TriviaGame", null)
                        .WithMany("GameParticipants")
                        .HasForeignKey("TriviaGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Domain.Entities.GameQuestion", b =>
                {
                    b.HasOne("Game.Domain.Entities.GameQuestion", "NextGameQuestion")
                        .WithMany()
                        .HasForeignKey("NextGameQuestionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("NextGameQuestion");
                });

            modelBuilder.Entity("Game.Domain.Entities.PlayerHistory", b =>
                {
                    b.HasOne("Game.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("Game.Domain.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.Navigation("Player");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Game.Domain.Entities.Question", b =>
                {
                    b.HasOne("Game.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Game.Domain.Entities.TriviaGame", b =>
                {
                    b.HasOne("Game.Domain.Entities.GameQuestion", "CurrentGameQuestion")
                        .WithMany()
                        .HasForeignKey("CurrentGameQuestionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CurrentGameQuestion");
                });

            modelBuilder.Entity("Game.Domain.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Game.Domain.Entities.TriviaGame", b =>
                {
                    b.Navigation("ChatMessages");

                    b.Navigation("GameParticipants");
                });
#pragma warning restore 612, 618
        }
    }
}
