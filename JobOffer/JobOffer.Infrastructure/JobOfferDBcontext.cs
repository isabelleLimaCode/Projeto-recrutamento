using JobOffer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace JobOffer.infrastructure
{
    public class JobOfferDBcontext : DbContext
    {
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Candidato_vaga> Candidato_has_vagas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Nivel> Nivels { get; set; }
        public DbSet<Recrutador> Recrutadores { get; set; }
        public DbSet<Vaga> Vagas { get; set; }

        public string DbPath { get; }
        public JobOfferDBcontext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}JobOfferClassC.db"; // -->alterado

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder mb)
        {
            //candidato
            mb.Entity<Candidato>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Candidato>().Property(c => c.Nome).IsRequired().HasMaxLength(80);
            mb.Entity<Candidato>().Property(c => c.Password).IsRequired().HasMaxLength(10);
            mb.Entity<Candidato>().Property(c => c.Experiencia).IsRequired().HasMaxLength(255);
            mb.Entity<Candidato>().Property(c => c.NivelEscolaridade).IsRequired().HasMaxLength(40);
            mb.Entity<Candidato>().Property(c => c.NivelCompeteciaDigitais).IsRequired().HasMaxLength(60);
            mb.Entity<Candidato>().Property(c => c.Idiomas).IsRequired().HasMaxLength(45);
            // mb.Entity<Candidato>().HasOne(p=> p.).WithMany(c => c.)

            //nivel 
            mb.Entity<Nivel>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Nivel>().Property(c => c.Descricao).IsRequired().HasMaxLength(70);

            //empresa
            mb.Entity<Empresa>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Empresa>().Property(c => c.Nome).IsRequired().HasMaxLength(70);

            //recrutador
            mb.Entity<Recrutador>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Recrutador>().Property(c => c.Nome).IsRequired().HasMaxLength(200);
            mb.Entity<Recrutador>().Property(c => c.Password).IsRequired().HasMaxLength(10);

            //vaga
            mb.Entity<Vaga>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Vaga>().Property(c => c.Tipo).IsRequired().HasMaxLength(255);
            mb.Entity<Vaga>().Property(c => c.Regime).IsRequired().HasMaxLength(100);
            mb.Entity<Vaga>().Property(c => c.Horario).IsRequired().HasMaxLength(100);
            mb.Entity<Vaga>().Property(c => c.Tipo_contrato).IsRequired().HasMaxLength(100);
            mb.Entity<Vaga>().Property(c => c.N_vaga).IsRequired().HasMaxLength(100);

            //candidato_has_vaga
            mb.Entity<Candidato_vaga>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Candidato_vaga>().Property(c => c.Descricao).IsRequired().HasMaxLength(100);
            mb.Entity<Candidato_vaga>().Property(c => c.Estado_da_Candidatura).IsRequired().HasMaxLength(45);
            mb.Entity<Candidato_vaga>().Property(c => c.Data).IsRequired().HasMaxLength(45);
            mb.Entity<Candidato_vaga>().HasOne(c => c.Candidato).WithMany(cb => cb.Candidato_Vagas).HasForeignKey(bc => bc.CandidatoId);
            mb.Entity<Candidato_vaga>().HasOne(c => c.Vaga).WithMany(cb => cb.Candidato_Vagas).HasForeignKey(bc => bc.VagaId);
            
            // cargo
            mb.Entity<Cargo>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<Cargo>().Property(c => c.Descricao).IsRequired().HasMaxLength(70);

            //criar niveis predifinido
            mb.Entity<Nivel>().HasData(new Nivel
            {
                Id = 1,
                Descricao = "Iniciante",
            });
            mb.Entity<Nivel>().HasData(new Nivel
            {
                Id = 2,
                Descricao = "Medio",
            });
            mb.Entity<Nivel>().HasData(new Nivel
            {
                Id = 3,
                Descricao = "Master",
            });

            // criar estados predefinidos
            mb.Entity<Estado>().HasData(new Estado
            {
                Id = 1,
                Descricao = "Disponivel",
            });
            mb.Entity<Estado>().HasData(new Estado
            {
                Id = 2,
                Descricao = "Analise",
            });
            mb.Entity<Estado>().HasData(new Estado
            {
                Id = 3,
                Descricao = "Ocupado",
            });
            //admin
            mb.Entity<Recrutador>().HasData(new Recrutador
            {
                Id = 1,
                Nome = "admin",
                Password = "admin",
            });


        }

    }
}
