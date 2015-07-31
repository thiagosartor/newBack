using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Aula : Entity
    {
        private DateTime now;

        public Aula()
        {
            Presencas = new List<Presenca>();
            Turma = new Turma();
        }
        

        public bool ChamadaRealizada { get; set; }

        public DateTime Data { get; set; }

        public virtual Turma Turma { get; set; }

        public virtual List<Presenca> Presencas { get; set; }

        public Aula(DateTime dateTime, Turma turma)
            : this()
        {
            this.Data = dateTime;
            this.Turma = turma;
            Presencas = new List<Presenca>();
        }

        public void ExcluiPresencas()
        {
            //Presencas.Remove
            Presencas = null;
        }

        public override bool Equals(object obj)
        {
            Aula aula = (Aula)obj;

            return this.Data.Equals(aula.Data);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}