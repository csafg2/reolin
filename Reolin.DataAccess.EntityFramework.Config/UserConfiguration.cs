using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

/// <summary>
/// Configuration for User table.
/// </summary>
namespace Reolin.DataAccess.EntityFramework.Config
{
    public class UserConfiguration: EntityTypeConfiguration<User>
    {

        public UserConfiguration()
        {
            this.HasKey(u => u.Id);
            
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
                        .ToTable("User_Skill"));

            // user must set a location
            // but an address object might not be accuired just by a user
            // may be university or a restaurent profile
            this.HasRequired(u => u.Address)
                .WithOptional(a => a.User);


            // many:many 
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
                .HasForeignKey(l => l.UserId);

            // 1:* Intelissence is necessary
            this.HasMany(u => u.Tags)
                .WithMany(t => t.Users)
                .Map(t => t.MapLeftKey("UserId")
                        .MapRightKey("TagId")
                        .ToTable("User_Tag"));
        }
    }
}
