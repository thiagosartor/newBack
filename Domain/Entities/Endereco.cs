namespace Domain.Entities
{
    public class Endereco
    {
        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Uf { get; set; }

        public Endereco(string cep, string bairro, string localidade, string uf)
        {
            Cep = cep;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }

        public Endereco()
        {
        }
    }
}