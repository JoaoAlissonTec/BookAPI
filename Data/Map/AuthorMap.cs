using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookAPI.Data.Map
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey("AuthorId")
                .IsRequired();
        }
    }
}
