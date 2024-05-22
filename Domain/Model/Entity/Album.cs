using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Model.Entity
{
    public class Album
    {
       
        public long Id { get; set; }
        
        public string Name { get; set; }
       
        public string Description { get; set; }


        public virtual ICollection<Song> Songs { get; set; }

    }
}
