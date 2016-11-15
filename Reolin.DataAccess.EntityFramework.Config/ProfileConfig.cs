using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{
    public class ProfileConfig: EntityTypeConfiguration<Profile>
    {
        public ProfileConfig()
        {
            this.HasKey(p => p.Id);

            // *:*
            this.HasMany(p => p.Certificates)
                .WithMany(c => c.Profiles)
                .Map(t => t.MapLeftKey("ProfileId")
                        .MapRightKey("CertificateId")
                        .ToTable("Profile_Certificate"));

            // a profile record can have multiple #tags 
            // #tags must be extracted from profile description string
            this.HasMany(p => p.Tags)
                .WithMany(t => t.Profiles)
                .Map(t =>  t.MapLeftKey("ProfileId")
                        .MapRightKey("TagId")
                        .ToTable("Profile_Tag"));


            // 1:*
            // a profile entry might have many images, such as a restaurent where
            // user can place many images from it.
            // but a profile of type "personal" is allowed to only have on image
            this.HasMany(p => p.Images)
                .WithOptional(img => img.Profile)
                .HasForeignKey(img => img.ProfileId);


            // 1:*
            // a profile page can have multiple comments below it.
            this.HasMany(p => p.Comments)
                .WithRequired(c => c.Profile)
                .HasForeignKey(c => c.ProfileId);

            // a profile record might have multiple certificates
            // take a restaurent for example: it might have a certificate from fucking secretary of health or what ever
            this.HasMany(p => p.Certificates)
                .WithMany(c => c.Profiles)
                .Map(p => p.MapLeftKey("ProfileId")
                        .MapRightKey("CertificateId")
                        .ToTable("Profile_Certificate"));


            // a profile record must have an address (location, etc..)
            this.HasRequired(p => p.Address)
                .WithOptional(a => a.Profile);
            
        }
    }
}
