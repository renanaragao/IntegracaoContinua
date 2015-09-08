using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaleMaisTelZir.Dominio.Chamada;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using FaleMaisTelZir.Infraestrutura.Interfaces;

namespace FaleMaisTelZir.Infraestrutura.Contexto
{
    public class FaleMaisTelZirContexto : DbContext, IDbContexto
    {
        public FaleMaisTelZirContexto()
            : base("FaleMaisTelZirDB")
        {

        }

        public virtual DbSet<Chamada> Chamadas { get; set; }    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(x => x.Name == "Codigo")
                .Configure(x => x.IsKey());

            modelBuilder.Properties<string>()
                .Configure(x => x.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(x => x.HasMaxLength(100));

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }

        public IDbSet<T> RetornarDbSet<T>() where T : class
        {
            return Set<T>();
        }

        public void DefinirComoModificado(object model)
        {
            Entry(model).State = EntityState.Modified;
        }

        public int SalvarModificacoes()
        {
            return SaveChanges();
        }
    }
}
