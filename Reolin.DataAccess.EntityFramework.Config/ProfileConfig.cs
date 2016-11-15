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

            // 1:*
            // a profile entry might have many images, such as a restaurent where
            // user can place many images from it.
            // but a profile of type "personal" is allowed to only have on image
            this.HasMany(p => p.Images)
                .WithOptional(img => img.Profile)
                .HasForeignKey(img => img.ProfileId);

            //TODO: complete configuration for profile class
        }
    }
}
