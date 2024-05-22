using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Model.Enum;

namespace WebApi.Resource
{
    public class SaveSongResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string MusicUrl { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
