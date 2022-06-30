using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicio.Models
{
    public class Doctors : BaseEntity
    {
        public string Name { get; set; }
        public string Duty { get; set; }
        public string ImgUrl { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
