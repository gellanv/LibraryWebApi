using System.ComponentModel.DataAnnotations;

namespace Library.DTO.Request
{
    public class SaveReviewDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Reviewer { get; set; }
    }
}
