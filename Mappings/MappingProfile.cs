using AutoMapper;
using Library.Data.Models;
using Library.DTO.Request;
using Library.DTO.Response;

namespace Library.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, GetAllBooksDto>().ReverseMap();
            CreateMap<Book, GetBookByIdDto>().ReverseMap();
            CreateMap<Review, GetBookByIdDto.ReviewDto>().ReverseMap();
            CreateMap<Book, SaveBookDto>().ReverseMap();
            CreateMap<Review, SaveReviewDto>().ReverseMap();
        }
    }
}