using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocVoiture.Models
{
    public class Reservation
    {
        [Key]
        public int LocationID { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [ForeignKey("Voiture")]
        public int VoitureID { get; set; }
        public virtual Voiture Voiture { get; set; }

        public Boolean LongueDuree { get; set; }

        [Required(ErrorMessage = "Date début est requise")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "Date fin est requise")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

        private double PrixLongueDuree()
        {
            return Voiture.PrixLocation * 0.6 * 30;
        }
    }
}