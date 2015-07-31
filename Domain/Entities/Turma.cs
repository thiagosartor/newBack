namespace Domain.Entities
{
    public class Turma : Entity
    {
        public Turma()
        {
        }

        public Turma(int ano)
        {
            Ano = ano;
        }

        public int Ano { get; set; }
    }
}