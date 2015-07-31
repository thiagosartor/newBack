using Domain.Entities;

namespace Application.DTOs
{
    public class AlunoDTO
    {
        public AlunoDTO()
        {
        }

        public AlunoDTO(Aluno aluno)
        {
            Id = aluno.Id;
            Descricao = aluno.ToString();
            TurmaId = aluno.Turma.Id;
        }

        public int Id { get; set; }

        public int TurmaId { get; set; }

        public string Descricao { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Uf { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}