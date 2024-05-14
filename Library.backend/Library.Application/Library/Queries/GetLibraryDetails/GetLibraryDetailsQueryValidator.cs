using System;
using FluentValidation;

namespace Library.Application.Library.Queries.GetLibraryDetails
{
    public class GetLibraryDetailsQueryValidator : AbstractValidator<GetLibraryDetailsQuery>
    {
        public GetLibraryDetailsQueryValidator()
        {
            RuleFor(library => library.UserId).NotEqual(Guid.Empty);
            RuleFor(library => library.Id).NotEqual(Guid.Empty);
        }
    }
}
