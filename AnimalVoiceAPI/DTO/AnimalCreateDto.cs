

using System.ComponentModel.DataAnnotations;

namespace AnimalAPI.DTO
{
    public class AnimalCreateDto
    {
        [Required, StringLength(50)]
        public string AnimalName { get; set; } = null!;
        [Required]
        public string? Url { get; set; }

        [Required]
        public string? VideoUrl { get; set; }
    }
}
