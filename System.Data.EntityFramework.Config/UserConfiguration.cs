using Reolin.Data.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

/// <summary>
/// Configuration for User table.
/// </summary>
namespace Reolin.Data.EntityFramework.Config
{
    public class UserConfiguration: EntityTypeConfiguration<User>
    {
        const int USERNAME_LENGTH = 50;
        const int EMAIL_LENGTH = 254;

        public UserConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Password).IsRequired();
            this.Property(u => u.UserName).HasMaxLength(USERNAME_LENGTH).IsRequired();
            this.Property(u => u.Email).HasMaxLength(EMAIL_LENGTH).IsRequired();

            // username has to be unique
            this.Property(u => u.UserName)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                     new IndexAnnotation(new IndexAttribute("IX_USERNAME", 1)
                     {
                         IsUnique = true
                     }));

            // user email has to be unique
            this.Property(u => u.Email)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                     new IndexAnnotation(new IndexAttribute("IX_EMAIL", 2)
                     {
                         IsUnique = true
                     }));


            // 1:* intellisence is necessary
            this.HasMany(u => u.Certificates)
                .WithMany(c => c.Users)
                .Map(t => t.MapLeftKey("CertificateId")
                        .MapRightKey("UserId")
                        .ToTable("User_Certificates"));

            // 1:* Intelisence is necessary
            this.HasMany(u => u.Skills)
                .WithMany(s => s.Users)
                .Map(t => t.MapLeftKey("UserId")
                        .MapRightKey("SkillId")
                        .ToTable("UserSkill"));

            // user must set a location
            // but an address object might not be accuired just by a user
            // may be university or a restaurent profile
            // allow the user to log once.
            //this.HasOptional(u => u.Address)
            //    .WithOptionalDependent(a => a.User);

            // *:*
            // maybe a certificate like mcsd is earned by multiple users
            // providing intelisence to user when selecting certificate is necessary
            this.HasMany(u => u.Certificates)
                .WithMany(c => c.Users)
                .Map(t => t.MapLeftKey("UserId")
                        .MapRightKey("CertificateId")
                        .ToTable("UserCertificate"));
            
            // the user might have multiple profile entries
            // such as one personal profile and multiple business profile
            // so 1:*
            this.HasMany(u => u.Profiles)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId);
            
            // keeping track of where the users likes
            this.HasMany(u => u.Likes)
                .WithRequired(l => l.Sender)
                .HasForeignKey(l => l.SenderId)
                .WillCascadeOnDelete(true);

            // 1:* Intelissence is necessary
            this.HasMany(u => u.Tags)
                .WithMany(t => t.Users)
                .Map(t => t.MapLeftKey("UserId")
                        .MapRightKey("TagId")
                        .ToTable("UserTag"));


            // 1:* Intelissence is necessary
            this.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(t => t.MapLeftKey("UserId")
                        .MapRightKey("RoleId")
                        .ToTable("UserRole"));
        }
    }
}
