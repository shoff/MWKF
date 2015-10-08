namespace MWKF.Api.Entities.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;
    using MWKF.Api.Entities.Identity;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(r => r.Roles);
            HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            Property(u => u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index",
               new IndexAnnotation(new IndexAttribute("UserNameIndex")
               {
                   IsUnique = true
               }));
            Property(u => u.Email).HasMaxLength(256);
            ToTable("Users");

        }
    }
}