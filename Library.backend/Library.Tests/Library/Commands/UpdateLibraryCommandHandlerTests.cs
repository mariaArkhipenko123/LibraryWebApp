using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Application.Common.Exceptions;
using Library.Application.Library.Commands.UpdateLibrary;
using Library.Tests.Common;
using Xunit;
using Library.Domain;
using Library.Persistence.Repository;
using Moq;

namespace Library.Tests.Library.Commands
{
    public class UpdateLibraryCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateLibraryCommandHandler_Success()
        {
            // Arrange
            var bookId = LibraryContextFactory.BookIdForUpdate;
            var userId = LibraryContextFactory.UserBId;
            var updatedTitle = "New Book Title";
            var updatedDescription = "New Book Description";
            var updatedIsbn = 54321;
            var updatedAuthorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C");

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Id).Returns(bookId);
            bookMock.Setup(b => b.UserId).Returns(userId);
            bookMock.Setup(b => b.Description).Returns("Old Description");
            bookMock.Setup(b => b.Title).Returns("Old Title");
            bookMock.Setup(b => b.Isbn).Returns(12345);
            bookMock.Setup(b => b.AuthorId).Returns(updatedAuthorId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
                .ReturnsAsync(bookMock.Object);
            unitOfWorkMock.Setup(uow => uow.BookRepository.Update(It.IsAny<Book>()));
              unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
    .ReturnsAsync(bookMock.Object);
            unitOfWorkMock.Setup(uow => uow.SaveAsync(CancellationToken.None))
                .Returns(Task.CompletedTask);

            var handler = new UpdateLibraryCommandHandler(unitOfWorkMock.Object);

            // Act
            await handler.Handle(new UpdateLibraryCommand
            {
                Id = bookId,
                UserId = userId,
                Title = updatedTitle,
                Description = updatedDescription,
                Isbn = updatedIsbn,
                AuthorId = updatedAuthorId
            }, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(uow => uow.BookRepository.GetByID(bookId), Times.Once);
            unitOfWorkMock.Verify(uow => uow.BookRepository.Update(It.IsAny<Book>()), Times.Once);
            unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateLibraryCommandHandler_FailOnWrongId()
        {
            // Arrange
            var wrongBookId = Guid.NewGuid();
            var userId = LibraryContextFactory.UserBId;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(wrongBookId))
                .ReturnsAsync((Book)null);

            var handler = new UpdateLibraryCommandHandler(unitOfWorkMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateLibraryCommand
                    {
                        Id = wrongBookId,
                        UserId = userId,
                        Title = "New Title",
                        Description = "New Description",
                        Isbn = 12345,
                        AuthorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C")
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateLibraryCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var bookId = LibraryContextFactory.BookIdForUpdate;
            var wrongUserId = LibraryContextFactory.UserAId;

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Id).Returns(bookId);
            bookMock.Setup(b => b.UserId).Returns(LibraryContextFactory.UserBId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
                .ReturnsAsync(bookMock.Object);

            var handler = new UpdateLibraryCommandHandler(unitOfWorkMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new UpdateLibraryCommand
                    {
                        Id = bookId,
                        UserId = wrongUserId,
                        Title = "New Title",
                        Description = "New Description",
                        Isbn = 12345,
                        AuthorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C")
                    },
                    CancellationToken.None);
            });
        }
    }
}
