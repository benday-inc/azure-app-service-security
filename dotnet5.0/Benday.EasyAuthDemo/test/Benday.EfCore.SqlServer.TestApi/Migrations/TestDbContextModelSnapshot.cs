// <auto-generated />
using Benday.EfCore.SqlServer.TestApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Benday.EfCore.SqlServer.TestApi.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
            .HasAnnotation("ProductVersion", "3.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128)
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            
            modelBuilder.Entity("Benday.EfCore.SqlServer.TestApi.EmailNewsletterSubscription", b =>
            {
                b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                
                b.Property<string>("EmailAddress")
                .HasColumnType("nvarchar(max)");
                
                b.HasKey("Id");
                
                b.ToTable("EmailNewsletterSubscriptions");
            });
            
            modelBuilder.Entity("Benday.EfCore.SqlServer.TestApi.Person", b =>
            {
                b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                
                b.Property<string>("FirstName")
                .IsRequired()
                .HasColumnType("nvarchar(max)");
                
                b.Property<string>("LastName")
                .IsRequired()
                .HasColumnType("nvarchar(max)");
                
                b.HasKey("Id");
                
                b.ToTable("Person");
            });
            
            modelBuilder.Entity("Benday.EfCore.SqlServer.TestApi.PersonNote", b =>
            {
                b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                
                b.Property<string>("NoteText")
                .IsRequired()
                .HasColumnType("nvarchar(max)");
                
                b.Property<int>("PersonId")
                .HasColumnType("int");
                
                b.HasKey("Id");
                
                b.HasIndex("PersonId");
                
                b.ToTable("PersonNote");
            });
            
            modelBuilder.Entity("Benday.EfCore.SqlServer.TestApi.PersonNote", b =>
            {
                b.HasOne("Benday.EfCore.SqlServer.TestApi.Person", "Person")
                .WithMany("Notes")
                .HasForeignKey("PersonId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            });
            #pragma warning restore 612, 618
        }
    }
}
