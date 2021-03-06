﻿using Domain.Entities;
using Infrastructure.DAO.ORM;
using Infrastructure.DAO.ORM.Common;
using System.Data.Entity;

namespace Infrasctructure.DAO.ORM.Contexts
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext()
            : base("DiarioContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityFrameworkContext>());
        }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Aula> Aulas { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        public DbSet<Presenca> Presencas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlunoConfiguration());
            modelBuilder.Configurations.Add(new EnderecoConfiguration());
            modelBuilder.Configurations.Add(new AulaConfiguration());
            modelBuilder.Configurations.Add(new TurmaConfiguration());
            modelBuilder.Configurations.Add(new PresencaConfiguration());
        }
    }
}