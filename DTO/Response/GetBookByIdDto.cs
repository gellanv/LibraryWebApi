namespace Library.DTO.Response
{
    public class GetBookByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public decimal Rating { get; set; }
        public List<ReviewDto> reviewDtos { get; set; }
        public class ReviewDto
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string Reviewer { get; set; }
        }
    }
}