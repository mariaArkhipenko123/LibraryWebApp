using System;
using FluentValidation;

namespace Library.Application.Library.Commands.CreateLibrary
{
    public class CreateLibraryCommandValidator : AbstractValidator<CreateLibraryCommand>
    {
        public CreateLibraryCommandValidator()
        {
            RuleFor(createLibraryCommand =>
                createLibraryCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createLibraryCommand =>
                createLibraryCommand.UserId).NotEqual(Guid.Empty);
        }
    }
    
}
