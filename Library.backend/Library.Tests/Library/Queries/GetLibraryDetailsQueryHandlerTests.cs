using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Library.Queries.GetLibraryDetails;
using Library.Domain;
using Library.Persistence.Repository;
using Library.Tests.Common;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Library.Queries
{
    [Collection("QueryCollection")]
    public class GetLibraryDetailsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetLibraryDetailsQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetLibraryDetailsQueryHandler_Success()
        {
            // Arrange
            var bookId = LibraryContextFactory.BookIdForUpdate;
            var userId = LibraryContextFactory.UserBId;

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Id).Returns(bookId);
            bookMock.Setup(b => b.UserId).Returns(userId);

            _unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
                .ReturnsAsync(bookMock.Object);

            var libraryDetailsVm = new LibraryDetailsVm
            {
                Id = bookId,
                UserId = userId,
                Title = "Book Title",
                Description = "Book Description",
                Isbn = 12345,
                AuthorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C"),
                AuthorFirstName = "Author Name"
            };

            _mapperMock.Setup(m => m.Map<LibraryDetailsVm>(bookMock.Object))
                .Returns(libraryDetailsVm);

            var handler = new GetLibraryDetailsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(new GetLibraryDetailsQuery
            {
                UserId = userId
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(userId, result.UserId);
            Assert.Equal("Book Title", result.Title);
            Assert.Equal("Book Description", result.Description);
            Assert.Equal(12345, result.Isbn);
            Assert.Equal(Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C"), result.AuthorId);
            Assert.Equal("Author Name", result.AuthorFirstName);

            _unitOfWorkMock.Verify(uow => uow.BookRepository.GetByID(It.IsAny<Guid>()), Times.Once);
            _mapperMock.Verify(m => m.Map<LibraryDetailsVm>(bookMock.Object), Times.Once);
        }

        [Fact]
        public async Task GetLibraryDetailsQueryHandler_FailOnWrongUserId()
        {
            // Arrange
            var bookId = LibraryContextFactory.BookIdForUpdate;
            var wrongUserId = LibraryContextFactory.UserAId;

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Id).Returns(bookId);
            bookMock.Setup(b => b.UserId).Returns(LibraryContextFactory.UserBId);

            _unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
                .ReturnsAsync(bookMock.Object);

            var handler = new GetLibraryDetailsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new GetLibraryDetailsQuery
                    {
                        UserId = wrongUserId
                    },
                    CancellationToken.None);
            });
        }
    }
}
