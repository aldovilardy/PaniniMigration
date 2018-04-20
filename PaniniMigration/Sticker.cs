using System;
using System.ComponentModel.DataAnnotations;

namespace PaniniMigration
{
    public class Sticker
    {
        [Key]
        [Required]
        [Range(1,465,ErrorMessage ="Los Id de la mona deben ser entre 1 y 465 unicamente")]
        public int StickerId { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string LargeImageUrl { get; set; }
        public string Position { get; set; }
        public DateTime? DoB { get; set; }
        public string Club { get; set; }
        public string Heigth { get; set; }
        public string Weigth { get; set; }
        public int? Debut { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
