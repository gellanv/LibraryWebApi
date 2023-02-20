using FluentValidation;
using Library.DTO.Request;

namespace LibraryWebApi.Validation
{
    public class ReviewValidation : AbstractValidator<SaveReviewDto>
    {
        public ReviewValidation()
        {
            RuleFor(review => review.Message).NotEmpty().Length(5, 100);
            RuleFor(review => review.Reviewer).NotEmpty().Length(5, 25);
        }
    }
}
