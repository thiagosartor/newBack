using Domain.Entities;

namespace Infrastructure.DAO.DTOs
{
    public class TurmaDTO
    {
        public TurmaDTO()
        {
        }

        public TurmaDTO(int id)
        {
            // TODO: Complete member initialization
            this.Id = id;
        }

        public TurmaDTO(Turma turma)
        {
            Id = turma.Id;
            Ano = turma.Ano;
        }

        public string Descricao { get { return "Academia do Programador " + Ano; } }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var turma = obj as TurmaDTO;

            if (turma == null)
                return false;

            return this.Id == turma.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int Ano { get; set; }

        public override string ToString()
        {
            return "Academia do Programador " + Ano;
        }
    }
}