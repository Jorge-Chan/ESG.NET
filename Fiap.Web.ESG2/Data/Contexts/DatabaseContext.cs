using Microsoft.EntityFrameworkCore;
using Fiap.Web.ESG2.Models;  // Ajuste o namespace conforme onde você salvou suas Models

namespace Fiap.Web.ESG2.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<EmpresaModel> Empresas { get; set; }
        public virtual DbSet<CompensacaoCarbonoModel> CompensacoesCarbono { get; set; }
        public virtual DbSet<HistoricoEmissaoModel> HistoricoEmissoes { get; set; }
        public virtual DbSet<RelotorioEmissaoModel> RelatoriosEmissao { get; set; }
        public virtual DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EMPRESA
            modelBuilder.Entity<EmpresaModel>(entity =>
            {
                entity.ToTable("empresa");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(255);
                entity.Property(e => e.Cnpj).HasMaxLength(255);
                entity.Property(e => e.Setor).HasMaxLength(255);
            });

            // COMPENSAÇÃO DE CARBONO
            modelBuilder.Entity<CompensacaoCarbonoModel>(entity =>
            {
                entity.ToTable("compensacao_carbono");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.TipoIniciativa).HasMaxLength(255);
                entity.Property(c => c.QuantidadeCompensada);
                entity.Property(c => c.DataCompensacao).HasColumnType("date");

                entity.HasOne(c => c.Empresa)
                      .WithMany(e => e.CompensacoesCarbono)
                      .HasForeignKey(c => c.EmpresaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // HISTÓRICO DE EMISSÕES
            modelBuilder.Entity<HistoricoEmissaoModel>(entity =>
            {
                entity.ToTable("historico_emissoes");
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Ano).HasColumnType("date");
                entity.Property(h => h.TotalEmitidoTonCO2);

                entity.HasOne(h => h.Empresa)
                      .WithMany(e => e.HistoricoEmissoes)
                      .HasForeignKey(h => h.EmpresaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // RELATÓRIO DE EMISSÃO
            modelBuilder.Entity<RelotorioEmissaoModel>(entity =>
            {
                entity.ToTable("relatorio_emissao");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.DataEnvio).HasColumnType("date");
                entity.Property(r => r.QuantidadeEmissao);
                entity.Property(r => r.ArquivoPdfUrl).HasMaxLength(255);

                entity.HasOne(r => r.Empresa)
                      .WithMany(e => e.RelatoriosEmissao)
                      .HasForeignKey(r => r.EmpresaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // USUÁRIOS
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("tbl_usuarios");
                entity.HasKey(u => u.UsuarioId);
                entity.Property(u => u.Nome).HasMaxLength(255);
                entity.Property(u => u.Email).HasMaxLength(255);
                entity.Property(u => u.Senha).HasMaxLength(255);
                
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}
