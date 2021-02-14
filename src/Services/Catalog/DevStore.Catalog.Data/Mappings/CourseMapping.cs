using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevStore.Catalog.Domain;

namespace DevStore.Catalog.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Video)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.OwnsOne(c => c.Specification, cm =>
            {
                cm.Property(c => c.TotalTime)
                    .HasColumnName("Duration")
                    .HasColumnType("int");

                cm.Property(c => c.NumberOfClasses)
                    .HasColumnName("NumberOfClasses")
                    .HasColumnType("int");
            });

            builder.OwnsOne(c => c.Period, cm =>
            {
                cm.Property(c => c.StartDate)
                    .HasColumnName("StartDate");

                cm.Property(c => c.EndDate)
                    .HasColumnName("EndDate");
            });

            builder.ToTable("Courses");
        }
    }
}