using System.ComponentModel.DataAnnotations;

namespace AnimalAPI.DTO
{
    public class AnimalDto
    {
        public int Id { get; set; }
        [Required]
        public string AnimalName { get; set; } = null!;
        [Required]
        public string? Url { get; set; }
        [Required]
        public string? VideoUrl { get; set; }
    }
}
