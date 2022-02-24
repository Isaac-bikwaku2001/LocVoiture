using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocVoiture.Models
{
    public class Categorie
    {
        [Key]
        public int CategorieID { get; set; }

        [Required(ErrorMessage = "Libelle est requis"), MaxLength(100)]
        public string Libelle { get; set; }
    }
}