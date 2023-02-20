using FluentValidation;
using Library.DTO.Request;

namespace LibraryWebApi.Validation
{
    public class BookValidation : AbstractValidator<SaveBookDto>
    {
        public BookValidation() {
            RuleFor(book => book.Title).NotEmpty().Length(3, 20);
            RuleFor(book => book.Content).NotEmpty().Length(20, 200);
            RuleFor(book => book.Genre).NotEmpty().Length(3, 20);
            RuleFor(book => book.Author).NotEmpty().Length(3, 50);
        } 
    }
}
