﻿using Library.DTO.Response;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    public class RecommendedController : ControllerBase
    {
        protected readonly BookService bookService;
        public RecommendedController(BookService _bookService)
        {
            bookService = _bookService;
        }

        //GET https://{{baseUrl}}/api/recommended?genre=horror
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllBooksDto>>> GetRecommendBooks(string? genre)
        {
            IEnumerable<GetAllBooksDto> getAllBooksDto = await bookService.GetRecommendBooks(genre);

            if (getAllBooksDto == null)
                return new NoContentResult();
            else return Ok(getAllBooksDto);
        }
    }
}