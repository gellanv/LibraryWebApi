using Library.Data;
using Library.Data.Models;
using LibraryWebApi.Validation;

namespace Library.Services
{
    public class RatingService
    {
        private readonly ApiDBContext context;
        private readonly CustomValidation customValidation;
        public RatingService(ApiDBContext _context, CustomValidation _customValidation)
        {
            this.context = _context;
            this.customValidation = _customValidation;
        }

        public async Task AddRaiting(int idBook, int score)
        {
            customValidation.CheckId(idBook, $"BookId ");
            customValidation.CheckScore(score);

            Book book = context.Books.FirstOrDefault(x => x.Id == idBook);
            customValidation.CheckObjectForNull(book, $"book with id - {idBook}");

            Rating raiting = new Rating() { BookId = idBook, Score = score };
            context.Ratings.Add(raiting);
            await context.SaveChangesAsync();
        }
    }
}
