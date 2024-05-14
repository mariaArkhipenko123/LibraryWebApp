using Library.Application.Library.Commands.CreateLibrary;
using Library.Domain;
using Library.Persistence.Repository;
using Library.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Library.Commands
{
    public class CreateLibraryCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateLibraryCommandHandler_Success()
        {
            // Arrange
            var authorId = Guid.Parse("EE407743-D5A8-49E3-8728-5FCA575F2F8C");
            var firstName = "Author";
            var lastName = "Name";
            var dateOfBirth = new DateTime(1980, 5, 14);
            var country = "Belarus";
            var isbn = 12345;
            var title = "Book Title";
            var genre = "Fiction";
            var description = "Book Description";
            var userId = LibraryContextFactory.UserAId;

            var authorMock = new Mock<Author>();
            authorMock.Setup(a => a.Id).Returns(authorId);
            authorMock.Setup(a => a.FirstName).Returns(firstName);
            authorMock.Setup(a => a.LastName).Returns(lastName);
            authorMock.Setup(a => a.DateOfBirth).Returns(dateOfBirth);
            authorMock.Setup(a => a.Country).Returns(country);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.AuthorRepository.GetByID(authorId))
                .ReturnsAsync(authorMock.Object);
            unitOfWorkMock.Setup(uow => uow.AuthorRepository.Insert(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(uow => uow.BookRepository.Insert(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(uow => uow.SaveAsync(CancellationToken.None))
                .Returns(Task.CompletedTask);

            var handler = new CreateLibraryCommandHandler(unitOfWorkMock.Object);

            // Act
            var bookId = await handler.Handle(
                new CreateLibraryCommand
                {
                    AuthorId = authorId,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Country = country,
                    Isbn = isbn,
                    Title = title,
                    Genre = genre,
                    Description = description,
                    UserId = userId
                },
                CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, bookId);
            unitOfWorkMock.Verify(uow => uow.AuthorRepository.GetByID(authorId), Times.Once);
            unitOfWorkMock.Verify(uow => uow.AuthorRepository.Insert(It.IsAny<Author>()), Times.Once);
            unitOfWorkMock.Verify(uow => uow.BookRepository.Insert(It.IsAny<Book>()), Times.Once);
            unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);
        }
    }
}
