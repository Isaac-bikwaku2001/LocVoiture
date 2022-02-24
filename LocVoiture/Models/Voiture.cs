using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocVoiture.Models
{
    public class Voiture
    {
        [Key]
        public int VoitureID { get; set; }

        [Required(ErrorMessage = "Numero d'immatriculation est requis"), MaxLength(20)]
        [Index(IsUnique = true)]
        [Display(Name = "N° Immatriculation")]
        public string NumImmatriculation { get; set; }

        [ForeignKey("Categorie")]
        public int CategorieID { get; set; }
        public virtual Categorie Categorie { get; set; }

        [ForeignKey("Modele")]
        public int ModeleID { get; set; }
        public virtual Modele Modele { get; set; }

        [Required(ErrorMessage = "Date de mise en circulation est requis")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de mise en circulation")]
        public DateTime DateMiseEnCirculation { get; set; }

        [Required(ErrorMessage = "Type de carburant est requis"), MaxLength(30)]
        [Display(Name = "Type de Carburant")]
        public string TypeCarburant { get; set; }

        [Required(ErrorMessage = "Type de transmission est requis"), MaxLength(50)]
        [Display(Name = "Type de transmission")]
        public string TypeTransmission { get; set; }

        [Required(ErrorMessage = "Kilomètrage est requis")]
        public int Kilometrage { get; set; }

        [Required(ErrorMessage = "Place est requis")]
        public int Place { get; set; }

        [Required(ErrorMessage = "Bagage est requis")]
        public int Bagage { get; set; }

        [Required(ErrorMessage = "Prix est requis")]
        [Display(Name = "Prix de location")]
        public double PrixLocation { get; set; }

        [Required]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}