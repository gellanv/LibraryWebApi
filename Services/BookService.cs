using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Library.Data;
using Library.Data.Models;
using Library.DTO.Request;
using Library.DTO.Response;
using LibraryWebApi.Exceptions;
using LibraryWebApi.Validation;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService
    {
        private readonly ApiDBContext context;
        private readonly IConfiguration config;
        readonly IMapper mapper;
        private readonly BookValidation bookValidation;
        private readonly CustomValidation customValidation;
        public BookService(IMapper _mapper, ApiDBContext _context, IConfiguration _config, BookValidation _bookValidation, CustomValidation _customValidation)
        {
            this.mapper = _mapper;
            this.context = _context;
            this.config = _config;
            this.bookValidation = _bookValidation;
            this.customValidation = _customValidation;
        }

        public async Task<List<GetAllBooksDto>> GetAllBooks(string order)
        {
            List<Book> result = null;

            if (order == "title")
                result = await context.Books.Include(b => b.Ratings).Include(b => b.Reviews).OrderBy(x => x.Title).ToListAsync();
            else
                result = await context.Books.Include(b => b.Ratings).Include(b => b.Reviews).OrderBy(x => x.Author).ToListAsync();

            if (result.Count != 0)
            {
                List<GetAllBooksDto> getAllBooksDto = new List<GetAllBooksDto>();
                result.ForEach((book) =>
                {
                    GetAllBooksDto tempBook = mapper.Map<GetAllBooksDto>(book);
                    tempBook.ReviewNumber = book.Reviews.Count();
                    tempBook.Rating = book.Ratings.Select(x => x.Score).Average();

                    getAllBooksDto.Add(tempBook);
                });
                return getAllBooksDto;
            }
            else return null;
        }

        public async Task<List<GetAllBooksDto>> GetRecommendBooks(string? genre)
        {
            var allBook = genre != null ?
                         await context.Books.Include(b => b.Ratings).Include(b => b.Reviews).Where(x => x.Genre == genre).ToListAsync() :
                         await context.Books.Include(b => b.Ratings).Include(b => b.Reviews).ToListAsync();

            if (allBook.Count != 0)
            {
                List<GetAllBooksDto> getAllBooksDto = new List<GetAllBooksDto>();
                allBook.ForEach((book) =>
                {
                    GetAllBooksDto tempBook = mapper.Map<GetAllBooksDto>(book);
                    tempBook.ReviewNumber = book.Reviews.Count();
                    tempBook.Rating = book.Ratings.Select(x => x.Score).Average();

                    getAllBooksDto.Add(tempBook);
                });

                var res = getAllBooksDto.Where(x => x.ReviewNumber > 10).OrderByDescending(x => x.Rating).Take(10).ToList();

                return res;
            }
            else return null;
        }

        public async Task<GetBookByIdDto> GetBookById(int id)
        {
            customValidation.CheckId(id, $"BookId ");

            var result = await context.Books.Where(b => b.Id == id).Include(b => b.Reviews).Include(b => b.Ratings).FirstOrDefaultAsync();
            customValidation.CheckObjectForNull(result, $"book with id - {id}");

            GetBookByIdDto book = mapper.Map<GetBookByIdDto>(result);
            book.Rating = result.Ratings.Select(x => x.Score).Average();

            if (result.Reviews.Count > 0)
            {
                book.reviewDtos = new List<GetBookByIdDto.ReviewDto>();
                result.Reviews.ToList().ForEach((rev) =>
                {
                    GetBookByIdDto.ReviewDto reviewDto = mapper.Map<GetBookByIdDto.ReviewDto>(rev);
                    book.reviewDtos.Add(reviewDto);
                });
            }

            return book;
        }

        public async Task DellBookById(int id, string secret)
        {
            customValidation.CheckId(id, $"BookId ");

            var SecretKey = config.GetSection("Key:KeyForDell").Value;
            if (secret.Trim() != SecretKey)
                throw new InvalidSecretKeyException("SecretKey was wrong");

            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            customValidation.CheckObjectForNull(book, $"book with id - {id}");

            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task<int> AddBook(SaveBookDto saveBookDto, IFormFile image)
        {
            ValidationResult results = await bookValidation.ValidateAsync(saveBookDto);
            if (!results.IsValid)
            {
                throw new ValidationException(results.ToString("~"));
            }

            string base64Image = ConverImage(image);

            Book book = mapper.Map<Book>(saveBookDto);
            book.Cover = base64Image;
            context.Books.Add(book);
            await context.SaveChangesAsync();

            return book.Id;
        }
        public async Task<int> UpdateBook(SaveBookDto saveBookDto, IFormFile image)
        {
            ValidationResult results = await bookValidation.ValidateAsync(saveBookDto);
            if (!results.IsValid)
            {
                throw new ValidationException(results.ToString("~"));
            }

            string base64Image = ConverImage(image);

            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == saveBookDto.Id);
            customValidation.CheckObjectForNull(book, $"book with id - {saveBookDto.Id}");

            mapper.Map(saveBookDto, book);
            book.Cover = base64Image;
            await context.SaveChangesAsync();
            return book.Id;
        }

        public string ConverImage(IFormFile photo)
        {
            string base64Image = "";

            if (photo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    photo.CopyTo(ms);
                    base64Image = Convert.ToBase64String(ms.ToArray());
                }
            }
            return base64Image;
        }
    }
}
