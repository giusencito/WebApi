using WebApi.Domain.Model.Entity;
using WebApi.Domain.Model.Enum;

namespace WebApi.Resource
{
    public class MusicResource
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string MusicUrl { get; set; }


        public Category Category { get; set; }

        public AlbumResource Album { get; set; }
       
    }
}
