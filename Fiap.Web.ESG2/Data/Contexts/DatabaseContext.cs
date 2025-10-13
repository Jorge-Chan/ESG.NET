using Microsoft.EntityFrameworkCore;
using Fiap.Web.ESG2.Models;

namespace Fiap.Web.ESG2.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        // DbSets
        public DbSet<EmpresaModel> Empresas { get; set; } = null!;
        public DbSet<CompensacaoCarbonoModel> CompensacoesCarbono { get; set; } = null!;
        public DbSet<HistoricoEmissaoModel> HistoricoEmissoes { get; set; } = null!;
        public DbSet<RelatorioEmissaoModel> RelatoriosEmissao { get; set; } = null!;
        public DbSet<UsuarioModel> Usuarios { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================= EMPRESA =========================
            modelBuilder.Entity<EmpresaModel>(entity =>
            {
                entity.ToTable("empresa");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                      .HasColumnName("nome")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Cnpj)
                      .HasColumnName("cnpj")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Setor)
                      .HasColumnName("setor")
                      .HasMaxLength(255)
                      .IsRequired();

                // Evita CNPJ duplicado
                entity.HasIndex(e => e.Cnpj).IsUnique();
            });

            // ============== COMPENSAÇÃO DE CARBONO =====================
            modelBuilder.Entity<CompensacaoCarbonoModel>(entity =>
            {
                entity.ToTable("compensacao_carbono");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.TipoIniciativa)
                      .HasColumnName("tipo_iniciativa")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(c => c.QuantidadeCompensada)
                      .HasColumnName("quantidade_compensada");

                // Oracle DATE
                entity.Property(c => c.DataCompensacao)
                      .HasColumnName("data_compensacao")
                      .HasColumnType("DATE");

                entity.Property(c => c.EmpresaId)
                      .HasColumnName("empresa_id");

                // FK e navegação
                entity.HasOne(c => c.Empresa)
                      .WithMany(e => e.CompensacoesCarbono)
                      .HasForeignKey(c => c.EmpresaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ================ HISTÓRICO DE EMISSÕES ====================
            modelBuilder.Entity<HistoricoEmissaoModel>(entity =>
            {
                entity.ToTable("historico_emissoes");
                entity.HasKey(h => h.Id);

                // Se 'Ano' for inteiro, troque para .HasColumnType(null) e remova DATE
                entity.Property(h => h.Ano)
                      .HasColumnName("ano")
                      .HasColumnType("DATE");

                entity.Property(h => h.TotalEmitidoTonCO2)
                      .HasColumnName("total_emitido_ton_co2");

                entity.Property(h => h.EmpresaId)
                      .HasColumnName("empresa_id");

                entity.HasOne(h => h.Empresa)
                      .WithMany(e => e.HistoricoEmissoes)
                      .HasForeignKey(h => h.EmpresaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ================== RELATÓRIO DE EMISSÃO ===================
            // DbSet

        // ModelBuilder
        modelBuilder.Entity<RelatorioEmissaoModel>(entity =>
{
    entity.ToTable("relatorio_emissao");
    entity.HasKey(r => r.Id);

    entity.Property(r => r.DataEnvio)
          .HasColumnName("data_envio")
          .HasColumnType("DATE");

        entity.Property(r => r.QuantidadeEmissao)
          .HasColumnName("quantidade_emissao");

        entity.Property(r => r.ArquivoPdfUrl)
          .HasColumnName("arquivo_pdf_url")
          .HasMaxLength(255);

        entity.Property(r => r.EmpresaId)
          .HasColumnName("empresa_id");

        entity.HasOne(r => r.Empresa)
          .WithMany(e => e.RelatoriosEmissao)
          .HasForeignKey(r => r.EmpresaId)
          .OnDelete(DeleteBehavior.Cascade);
    });

            // ========================= USUÁRIO =========================
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                // Padrão que definimos nos patches anteriores
                entity.ToTable("usuario");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nome)
                      .HasColumnName("nome")
                      .HasMaxLength(120)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(120)
                      .IsRequired();

                entity.Property(u => u.SenhaHash)
                      .HasColumnName("senha_hash")
                      .IsRequired();

                entity.Property(u => u.Role)
                      .HasColumnName("role")
                      .HasMaxLength(30)
                      .IsRequired();

                entity.Property(u => u.Ativo)
                      .HasColumnName("ativo");

                // e-mail único
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}
