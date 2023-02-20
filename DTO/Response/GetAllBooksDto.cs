namespace Library.DTO.Response
{
    public class GetAllBooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Rating { get; set; }
        public decimal ReviewNumber { get; set; }
    }
}