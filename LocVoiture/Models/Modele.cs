using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocVoiture.Models
{
    public class Modele
    {
        [Key]
        public int ModeleID { get; set; }

        [Required(ErrorMessage = "Marque est requis"), MaxLength(100)]
        public string Marque { get; set; }

        [Required(ErrorMessage = "Série est requis"), MaxLength(50)]
        public string Serie { get; set; }
    }
}