using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LocVoiture.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Nom est requis"), MaxLength(200)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Prénom est requis"), MaxLength(200)]
        [Display(Name = "Prenom")]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "CIN")]
        public string CIN { get; set; }

        [Required]
        [Display(Name = "Permis de Conduire")]
        public string PermisConduire { get; set; }

        [NotMapped]
        public HttpPostedFileBase CINFile { get; set; }

        [NotMapped]
        public HttpPostedFileBase PCFile { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez que l'authenticationType doit correspondre à celui défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter des revendications utilisateur personnalisées ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}