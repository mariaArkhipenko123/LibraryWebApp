using AutoMapper;
using Library.Application.Interfaces;
using Library.Application.Library.Queries.GetLibraryList;
using Library.Domain;
using Library.Persistence.Repository;
using Library.Tests.Common;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Library.Queries
{
    [Collection("QueryCollection")]
    public class GetLibraryListQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetLibraryListQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetLibraryListQueryHandler_Success()
        {
            // Arrange
            var userId = LibraryContextFactory.UserBId;

            var book1 = new Book
            {
                Id = LibraryContextFactory.BookIdForUpdate,
                UserId = userId,
                Title = "Book 1",
                Description = "Description 1",
                Genre = "Fiction",
                AuthorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C"),
                Author = new Author
                {
                    FirstName = "Author",
                    LastName = "1"
                },
                Isbn = 12345
            };

            var book2 = new Book
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = "Book 2",
                Description = "Description 2",
                Genre = "Non-Fiction",
                AuthorId = Guid.Parse("12345678-ABCD-EFGH-IJKL-MNOPQRSTUVWX"),
                Author = new Author
                {
                    FirstName = "Author",
                    LastName = "2"
                },
                Isbn = 54321
            };

            var books = new List<Book> { book1, book2 };

            _unitOfWorkMock.Setup(uow => uow.BookRepository.Get(
                    b => b.UserId == userId,
                    null,
                    ""))
                .ReturnsAsync(books);

            var libraryLookupDtos = new List<LibraryLookupDto>
            {
                new LibraryLookupDto
                {
                    Id = book1.Id,
                    Title = book1.Title,
                    Description = book1.Description,
                    Genre = book1.Genre,
                    AuthorId = book1.AuthorId,
                    AuthorFirstName = book1.Author.FirstName,
                    AuthorLastName = book1.Author.LastName,
                    Isbn = book1.Isbn
                },
                new LibraryLookupDto
                {
                    Id = book2.Id,
                    Title = book2.Title,
                    Description = book2.Description,
                    Genre = book2.Genre,
                    AuthorId = book2.AuthorId,
                    AuthorFirstName = book2.Author.FirstName,
                    AuthorLastName = book2.Author.LastName,
                    Isbn = book2.Isbn
                }
            };

            _mapperMock.Setup(m => m.Map<IList<LibraryLookupDto>>(books))
                .Returns(libraryLookupDtos);

            var handler = new GetLibraryListQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(new GetLibraryListQuery
            {
                UserId = userId
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LibraryListVm>(result);
            Assert.Equal(2, result.Books.Count);

            _unitOfWorkMock.Verify(uow => uow.BookRepository.Get(
                b => b.UserId == userId,
                null,
                ""), Times.Once);
            _mapperMock.Verify(m => m.Map<IList<LibraryLookupDto>>(books), Times.Once);
        }
    }
}
