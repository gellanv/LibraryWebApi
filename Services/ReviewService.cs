using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Library.Data;
using Library.Data.Models;
using Library.DTO.Request;
using LibraryWebApi.Validation;

namespace Library.Services
{
    public class ReviewService
    {
        private readonly ApiDBContext context;
        private readonly IMapper mapper;
        private readonly ReviewValidation reviewValidation;
        private readonly CustomValidation customValidation;
        public ReviewService(IMapper _mapper, ApiDBContext _context, ReviewValidation _reviewValidation, CustomValidation _customValidation)
        {
            this.mapper = _mapper;
            this.context = _context;
            this.reviewValidation = _reviewValidation;
            this.customValidation = _customValidation;
        }

        public async Task<int> SaveReview(int id, SaveReviewDto saveReviewDtoRequest)
        {
            customValidation.CheckId(id, $"BookId ");
            ValidationResult results = await reviewValidation.ValidateAsync(saveReviewDtoRequest);
            if (!results.IsValid)
                throw new ValidationException(results.ToString("~"));

            Book book = context.Books.FirstOrDefault(x => x.Id == id);
            customValidation.CheckObjectForNull(book, $"book with id - {id}");

            Review review = mapper.Map<Review>(saveReviewDtoRequest);
            review.BookId = id;
            context.Reviews.Add(review);
            await context.SaveChangesAsync();

            return review.Id;
        }
    }
}
