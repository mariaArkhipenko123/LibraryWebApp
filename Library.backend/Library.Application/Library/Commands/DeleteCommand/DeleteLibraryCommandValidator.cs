using System;
using FluentValidation;

namespace Library.Application.Library.Commands.DeleteCommand
{
    public class DeleteLibraryCommandValidator : AbstractValidator<DeleteLibraryCommand>
    {
        public DeleteLibraryCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
