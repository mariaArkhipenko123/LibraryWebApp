using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain;

namespace Library.Persistense.EntityTypeConfigurations
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Book>
    {
            public void Configure(EntityTypeBuilder<Book> builder)
            {
                builder.HasKey(book => book.Id);
                builder.HasIndex(book => book.Id).IsUnique();
                builder.Property(book => book.Title).HasMaxLength(250);
                builder.Property(book => book.Genre).HasMaxLength(250);
                builder.Property(book => book.Description).HasMaxLength(250);

                builder.HasOne(book => book.Author)
                    .WithMany(author => author.Books)
                    .HasForeignKey(book => book.AuthorId);
            }
        }
}
