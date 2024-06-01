using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookAPI.Data.Map
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Publish).IsRequired();
            builder.Property(x => x.AuthorId);

            builder.HasOne(x => x.Author);
        }
    }
}
