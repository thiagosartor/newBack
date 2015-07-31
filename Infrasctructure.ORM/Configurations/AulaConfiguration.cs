using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.DAO.ORM
{
    public class AulaConfiguration : EntityTypeConfiguration<Aula>
    {
        public AulaConfiguration()
        {
            ToTable("TBAula");

            HasKey(a => a.Id);

            HasRequired(a => a.Turma)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasMany(a => a.Presencas);

            Property(a => a.Data)
                .HasColumnType("Date");
        }
    }
}