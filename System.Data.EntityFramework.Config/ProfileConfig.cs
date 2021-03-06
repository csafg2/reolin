﻿using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{

    public class ProfileConfig : EntityTypeConfiguration<Profile>
    {
        public ProfileConfig()
        {
            this.HasKey(p => p.Id);

            this.HasMany(p => p.Experiences)
                 .WithRequired(s => s.Profile)
                 .HasForeignKey(s => s.ProfileId);

            this.HasMany(p => p.Suggestions)
                 .WithRequired(s => s.Profile)
                 .HasForeignKey(s => s.ProfileId);

            this.HasMany(p => p.ImageCategories)
                .WithRequired(imc => imc.Profile)
                .HasForeignKey(imc => imc.ProfileId)
                .WillCascadeOnDelete(false);

            this.HasMany(p => p.Networks)
                .WithRequired(pn => pn.Profile)
                .HasForeignKey(pn => pn.ProfileId)
                .WillCascadeOnDelete(false);

            // 1:* Intelisence is necessary
            this.HasMany(u => u.Skills)
                .WithMany(s => s.Profiles)
                .Map(t => t.MapLeftKey("ProfileId")
                        .MapRightKey("SkillId")
                        .ToTable("ProfileSkill"));

            // *:*
            this.HasMany(p => p.Certificates)
                .WithRequired(c => c.Profile)
                .HasForeignKey(c => c.ProfileId);

            // a profile record can have multiple #tags 
            // #tags must be extracted from profile description string
            this.HasMany(p => p.Tags)
                .WithMany(t => t.Profiles)
                .Map(t => t.MapLeftKey("ProfileId")
                        .MapRightKey("TagId")
                        .ToTable("ProfileTag"));


            // 1:*
            // a profile entry might have many images, such as a restaurent where
            // user can place many images from it.
            // but a profile of type "personal" is allowed to only have on image
            this.HasMany(p => p.Images)
                .WithRequired(img => img.Profile)
                .HasForeignKey(img => img.ProfileId);


            // 1:*
            // a profile page can have multiple comments below it.
            this.HasMany(p => p.Comments)
                .WithRequired(c => c.Profile)
                .HasForeignKey(c => c.ProfileId);

            // a profile record might have multiple certificates
            //// take a restaurent for example: it might have a certificate from fucking secretary of health or what ever
            //this.HasMany(p => p.Certificates)
            //    .WithRequired(c => c.Profile)



            // a profile record must have an address (location, etc..)
            this.HasRequired(p => p.Address)
                .WithOptional(a => a.Profile);

            // likes that profile has received
            this.HasMany(p => p.ReceivedLikes)
                .WithRequired(l => l.TargetProfile)
                .HasForeignKey(l => l.TargetProfileId)
                .WillCascadeOnDelete(false);

            this.HasMany(p => p.RelatedTos)
                .WithRequired(rt => rt.TargetProfile)
                .HasForeignKey(rt => rt.TargetProfileId)
                .WillCascadeOnDelete(false);

            this.HasMany(p => p.Relatedes)
                .WithRequired(rt => rt.SourceProfile)
                .HasForeignKey(rt => rt.SourceProfileId)
                .WillCascadeOnDelete(false);

            this.HasMany(p => p.Comments)
                .WithRequired(c => c.Profile)
                .HasForeignKey(c => c.ProfileId)
                .WillCascadeOnDelete(false);

            this.HasMany(p => p.Educations)
                .WithOptional(e => e.Profile)
                .HasForeignKey(e => e.ProfileId)
                .WillCascadeOnDelete(true);
        }
    }
}
