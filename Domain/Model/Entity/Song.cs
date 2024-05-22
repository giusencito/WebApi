using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Model.Enum;

namespace WebApi.Domain.Model.Entity
{
    public class Song

    {
      
        public long Id { get; set; }
        public string Name { get; set; }

        public string MusicUrl { get; set; }

       
        public Category Category { get; set; }

       
        public long AlbumId { get; set; }
        public  Album Album { get; set; }


    }
}
