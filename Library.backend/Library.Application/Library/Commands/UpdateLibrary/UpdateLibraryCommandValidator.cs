using System;
using FluentValidation;

namespace Library.Application.Library.Commands.UpdateLibrary
{
    public class UpdateLibraryCommandValidator : AbstractValidator<UpdateLibraryCommand>
    {
        public UpdateLibraryCommandValidator()
        {
            RuleFor(updateLibraryCommand => updateLibraryCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateLibraryCommand => updateLibraryCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateLibraryCommand => updateLibraryCommand.Title)
                .NotEmpty().MaximumLength(250);
        }
    }
}
