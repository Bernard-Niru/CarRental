using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarRental.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        // Navigation properties

        [ForeignKey("CarID")]
        public Car? Car { get; set; }

        [Required]  // Add this
        public string MimeType { get; set; }
    }
}
