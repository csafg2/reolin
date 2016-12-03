using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class CertificateConfig: EntityTypeConfiguration<Certificate>
    {
        public CertificateConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Title).IsRequired();
        }
    }
}