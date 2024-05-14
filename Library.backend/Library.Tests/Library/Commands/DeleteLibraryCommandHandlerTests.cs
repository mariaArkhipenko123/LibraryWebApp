using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Library.Commands.DeleteCommand;
using Library.Domain;
using Library.Persistence.Repository;
using Library.Tests.Common;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Library.Commands
{
    public class DeleteLibraryCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteLibraryCommandHandler_Success()
        {
            // Arrange
            var bookId = LibraryContextFactory.BookIdForDelete;
            var userId = LibraryContextFactory.UserAId;

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Id).Returns(bookId);
            bookMock.Setup(b => b.UserId).Returns(userId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
                .ReturnsAsync(bookMock.Object);
            unitOfWorkMock.Setup(uow => uow.BookRepository.Delete(It.IsAny<Book>()))
                .Verifiable();
            unitOfWorkMock.Setup(uow => uow.Save())
                .Verifiable();

            var handler = new DeleteLibraryCommandHandler(unitOfWorkMock.Object);

            // Act
            await handler.Handle(new DeleteLibraryCommand
            {
                Id = bookId,
                UserId = userId
            }, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(uow => uow.BookRepository.GetByID(bookId), Times.Once);
            unitOfWorkMock.Verify(uow => uow.BookRepository.Delete(It.IsAny<Book>()), Times.Once);
            unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteLibraryCommandHandler_FailOnWrongId()
        {
            // Arrange
            var wrongBookId = Guid.NewGuid();
            var userId = LibraryContextFactory.UserAId;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(wrongBookId))
                .ReturnsAsync((Book)null);

            var handler = new DeleteLibraryCommandHandler(unitOfWorkMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteLibraryCommand
                    {
                        Id = wrongBookId,
                        UserId = userId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteLibraryCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var bookId = LibraryContextFactory.BookIdForDelete;
            var wrongUserId = LibraryContextFactory.UserBId;

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Id).Returns(bookId);
            bookMock.Setup(b => b.UserId).Returns(LibraryContextFactory.UserAId);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.BookRepository.GetByID(bookId))
                .ReturnsAsync(bookMock.Object);

            var handler = new DeleteLibraryCommandHandler(unitOfWorkMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new DeleteLibraryCommand
                    {
                        Id = bookId,
                        UserId = wrongUserId
                    },
                    CancellationToken.None);
            });
        }
    }
}
