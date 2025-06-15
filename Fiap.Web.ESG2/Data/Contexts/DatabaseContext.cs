using Fiap.Web.ESG2.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.ESG2.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<CompensacaoCarbonoModel> CompensacoesCarbono { get; set; }
        public DbSet<HistoricoEmissoesModel> HistoricosEmissoes { get; set; }
        public DbSet<RelotorioEmissaoModel> RelatoriosEmissao { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração opcional para evitar nomes pluralizados nas tabelas (caso queira manter o nome exato das tabelas SQL)
            modelBuilder.Entity<EmpresaModel>().ToTable("empresa");
            modelBuilder.Entity<CompensacaoCarbonoModel>().ToTable("compensacao_carbono");
            modelBuilder.Entity<HistoricoEmissoesModel>().ToTable("historico_emissoes");
            modelBuilder.Entity<RelotorioEmissaoModel>().ToTable("relatorio_emissao");
            modelBuilder.Entity<UsuarioModel>().ToTable("tbl_usuarios");

            // Exemplo de como configurar a sequence (para o Oracle, se quiser garantir que o EF saiba dela)
            modelBuilder.HasSequence<long>("SEQ_USUARIOS")
                .StartsAt(1)
                .IncrementsBy(1);
        }
    }
}
