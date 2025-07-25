using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroTecnicoss.DAL;

#nullable disable

namespace RegistroTecnicoss.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("RegistroTecnicoss.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LimiteCredito")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Rnc")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClienteId");

                    b.HasIndex("TecnicoId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("RegistroTecnicoss.Models.Tecnico", b =>
            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RegistroTecnicoss.Models.Clientes", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<double>("LimiteCredito")
                        .HasColumnType("float");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rnc")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("int");

                    b.HasKey("ClienteId");

                    b.HasIndex("TecnicoId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("RegistroTecnicoss.Models.Tecnicos", b =>
                {
                    b.Property<int>("TecnicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TecnicoId"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SueldoHora")
                        .HasColumnType("float");

                    b.HasKey("TecnicoId");

                    b.ToTable("Tecnico");
                });

            modelBuilder.Entity("RegistroTecnicoss.Models.Cliente", b =>
                {
                    b.HasOne("RegistroTecnicoss.Models.Tecnico", "Tecnico")
                    b.ToTable("Prestamos");
                    b.ToTable("Tecnico");
                });

            modelBuilder.Entity("RegistroTecnicoss.Models.Clientes", b =>
                {
                    b.HasOne("RegistroTecnicoss.Models.Tecnicos", "Tecnico")
                        .WithMany()
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tecnico");

                });
#pragma warning restore 612, 618
        }
    }
}
