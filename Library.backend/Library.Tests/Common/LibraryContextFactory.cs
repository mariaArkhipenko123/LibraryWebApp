using System;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.Persistense;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Tests.Common
{
    public class LibraryContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid BookIdForDelete = Guid.NewGuid();
        public static Guid BookIdForUpdate = Guid.NewGuid();

        public static LibraryDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new LibraryDbContext(options);

            ApplyMigrations(context);

            context.Books.AddRange(
                new Book
                {
                    UserId = UserAId,
                    Id = Guid.Parse("C47CE9F3-DD6A-4938-A027-7F8756A09536"),
                    Isbn = 12345,
                    Title = "Book1",
                    Genre = "Fiction",
                    Description = "Description1",
                    AuthorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C"),
                    TimeTaken = DateTime.Today,
                    TimeToReturn = null
                },
                new Book
                {
                    UserId = UserBId,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    Isbn = 54321,
                    Title = "Book2",
                    Genre = "Non-Fiction",
                    Description = "Description2",
                    AuthorId = Guid.Parse("14DE2712-4A34-44A2-A974-D9CAACDACBBD"),
                    TimeTaken = DateTime.Today,
                    TimeToReturn = null
                },
                new Book
                {
                    UserId = UserAId,
                    Id = BookIdForDelete,
                    Isbn = 67890,
                    Title = "Book3",
                    Genre = "Science Fiction",
                    Description = "Description3",
                    AuthorId = Guid.Parse("14DE2712-4A34-44A2-A974-D9CAACDACBBD"),
                    TimeTaken = DateTime.Today,
                    TimeToReturn = null
                },
                new Book
                {
                    UserId = UserBId,
                    Id = BookIdForUpdate,
                    Isbn = 98765,
                    Title = "Book4",
                    Genre = "Fantasy",
                    Description = "Description4",
                    AuthorId = Guid.Parse("14DE2712-4A34-44A2-A974-D9CAACDACBBD"),
                    TimeTaken = DateTime.Today,
                    TimeToReturn = null
                }

            );

            context.Authors.AddRange(
                new Author
                {
                    Id = Guid.Parse("14DE2712-4A34-44A2-A974-D9CAACDACBBD"),
                    FirstName = "Author",
                    LastName = "Jon",
                    Country="Belarus",
                    DateOfBirth = new DateTime(1980, 5, 14),
                }
            );

            context.SaveChanges();
            return context;
        }

        public static void Destroy(LibraryDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void ApplyMigrations(LibraryDbContext context)
        {
            var databaseCreator = context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null && !databaseCreator.HasTables())
            {
                databaseCreator.CreateTables();
            }
        }
    }
}
