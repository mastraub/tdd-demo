using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.Property(x => x.Name)
                .IsRequired()
                ;
            builder.Property(x => x.Email)
                .IsRequired()
                ;
        }
    }
}