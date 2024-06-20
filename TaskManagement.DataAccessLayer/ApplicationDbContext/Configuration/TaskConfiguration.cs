using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.DataAccessLayer.ApplicationDbContext.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Title)
                .HasColumnType("nvarchar(1000)");

            builder.Property(_ => _.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(_ => _.DeadlineDate)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(_ => _.IsCompleted)
               .HasColumnType("bit")
               .HasDefaultValue(0);

            builder.Property(_ => _.IsImportant)
               .HasColumnType("bit")
               .HasDefaultValue(0);

            builder.Property(_ => _.CreateAt)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(_ => _.UpdateAt)
                .HasColumnType("datetime2(7)");
        }
    }
}
