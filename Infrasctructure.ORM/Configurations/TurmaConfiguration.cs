using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.DAO.ORM
{
    public class TurmaConfiguration : EntityTypeConfiguration<Turma>
    {
        public TurmaConfiguration()
        {
            ToTable("TBTurma");

            HasKey(t => t.Id);

            Property(t => t.Ano);
        }
    }
}