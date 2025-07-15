using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFDatabaseFirstApproachDemo.Models;

public partial class SampleDbContext : DbContext
{
    public SampleDbContext()
    {
    }

    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SampleDB;Integrated Security=True;Trust Server Certificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Ecode).HasName("PK__employee__A7219C828DD3F01D");

            entity.ToTable("employees");

            entity.Property(e => e.Ecode)
                .ValueGeneratedNever()
                .HasColumnName("ecode");
            entity.Property(e => e.Deptid).HasColumnName("deptid");
            entity.Property(e => e.Ename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ename");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
