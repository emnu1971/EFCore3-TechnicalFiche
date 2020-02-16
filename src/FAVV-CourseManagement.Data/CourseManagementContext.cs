using FAVV_CourseManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FAVV_CourseManagement.Data
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Course management database context.
    /// </summary>
    
    public class CourseManagementContext : DbContext
    {

        public ILoggerFactory MyConsoleLoggerFactory;

        public CourseManagementContext()
        {
            Initialize();
        }

        public DbSet<Course> Courses { get; set; }

        //    //This won't work, because we only want the Teacher to be 
        //    //existing in the context of it's parent (Course), so we can't
        //    //create a DbSet for Teachers ! (due to OneToOne relationship !)
        //    //public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public CourseManagementContext(DbContextOptions<CourseManagementContext> options)
            : base(options)
        {

            Initialize();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable this when using FAVV-CourseManagement.ConsoleApp
            // Disable this when using FAVV-CourseManagement.Api 
            optionsBuilder
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = CourseManagementConsoleAppDb; Trusted_Connection = True;")
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .EnableSensitiveDataLogging(true);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure mappings for Course entity
            modelBuilder.ApplyConfiguration<Course>(new CourseEntityConfiguration());

            // Configure mappings for Teacher entity
            modelBuilder.ApplyConfiguration<Teacher>(new TeacherEntityConfiguration());

            // Configure mappings for Student entity
            modelBuilder.ApplyConfiguration<Student>(new StudentEntityConfiguration());

            // Configure mappings for Enrollment entity
            modelBuilder.ApplyConfiguration<Enrollment>(new EnrollmentEntityConfiguration());

            // Add Shadow properties for Creation and Modified date for each entity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
            }


            base.OnModelCreating(modelBuilder);
        }

        private void Initialize()
        {
            // Setup Logging
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
            .AddConsole()
            .AddFilter(DbLoggerCategory.Database.Command.Name, level => level == LogLevel.Information));
            MyConsoleLoggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Add values for Create and LastModified dates
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Property("LastModified").CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = timestamp;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


    }

    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course.HasIndex(c => c.Name)
                .IsUnique(true);
            course.Property(c => c.Name)
                .HasMaxLength(50);
        }
    }

    public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> teacher)
        {
            teacher.ToTable("Teachers");
            teacher.Property(t => t.FirstName)
                .HasMaxLength(50);
            teacher.Property(t => t.LastName)
                .HasMaxLength(50);
        }
    }

    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student.Property(t => t.FirstName)
                .HasMaxLength(50);
            student.Property(t => t.LastName)
                .HasMaxLength(50);
        }
    }

    public class EnrollmentEntityConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> enrollment)
        {
            enrollment.HasKey(e => new { e.CourseId, e.StudentId });
           
        }
    }

    public class DisenrollmentEntityConfiguration : IEntityTypeConfiguration<Disenrollment>
    {
        public void Configure(EntityTypeBuilder<Disenrollment> disenrollment)
        {
            disenrollment.HasKey(d => new { d.Course, d.StudentId });
        }
    }

}
