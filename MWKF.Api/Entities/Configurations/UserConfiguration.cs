using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using AUSKF.Api.Entities.Identity;

namespace AUSKF.Api.Entities.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasMany(r => r.Roles);
            this.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            this.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            this.Property(u => u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index",
               new IndexAnnotation(new IndexAttribute("UserNameIndex")
               {
                   IsUnique = true
               }));
            this.Property(u => u.Email).HasMaxLength(256);
            this.ToTable("Users");

        }
    }
}