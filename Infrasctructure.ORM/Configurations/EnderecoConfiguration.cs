using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.DAO.ORM
{
    public class EnderecoConfiguration : ComplexTypeConfiguration<Endereco>
    {
        public EnderecoConfiguration()
        {
            Property(a => a.Bairro);
            Property(a => a.Cep);
            Property(a => a.Localidade);
            Property(a => a.Uf).HasMaxLength(2);
        }
    }
}