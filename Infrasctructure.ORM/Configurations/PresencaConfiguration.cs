using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.DAO.ORM
{
    public class PresencaConfiguration : EntityTypeConfiguration<Presenca>
    {
        public PresencaConfiguration()
        {
            ToTable("TBPresenca");

            HasKey(p => p.Id);

            HasRequired(p => p.Aluno);

            HasRequired(p => p.Aula);

            Property(p => p.StatusPresenca);
        }
    }
}