using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace GraduationProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required, RegularExpression("[a-zA-Z ]{3,}")]
        public string FName { get; set; }

        [Required, RegularExpression("[a-zA-Z ]{3,}")]
        public string LName { get; set; }
        [DefaultValue("Profile.png")]
        public string ProfilePicture { get; set; }
        public virtual ICollection<Book> Favourites { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Book>(s => s.Favourites)
                .WithMany(c => c.Favourites)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserID");
                    cs.MapRightKey("BookID");
                    cs.ToTable("Favourites");
                    
                });
           

        }
        public static ApplicationDbContext Create()
        {
          
            return new ApplicationDbContext();
        }
    }
}