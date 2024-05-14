using System;
using FluentValidation;
using Library.Application.Library.Queries.GetLibraryDetails;

namespace Library.Application.Library.Queries.GetLibraryList
{
    public class GetLibraryListQueryValidator : AbstractValidator<GetLibraryDetailsQuery>
    {
        public GetLibraryListQueryValidator() 
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
}
