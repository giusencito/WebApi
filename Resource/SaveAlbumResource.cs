using System.ComponentModel.DataAnnotations;

namespace WebApi.Resource
{
    public class SaveAlbumResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
