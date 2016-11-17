using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{
    public class CertificateConfig: EntityTypeConfiguration<Certificate>
    {
        public CertificateConfig()
        {
            this.HasKey(c => c.Id);
        }
    }
}