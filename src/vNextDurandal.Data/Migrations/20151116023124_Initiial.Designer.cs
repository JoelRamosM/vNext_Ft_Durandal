using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using vNextDurandal.Data.Configuration;

namespace vNextDurandal.Data.Migrations
{
    [DbContext(typeof(VNextContext))]
    [Migration("20151116023124_Initiial")]
    partial class Initiial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("vNextDurandal.Business.Models.Movimentacao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Categoria");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Observacao");

                    b.Property<int>("Tipo");

                    b.Property<double>("Valor");

                    b.HasKey("Id");
                });
        }
    }
}
