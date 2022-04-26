using DataInterface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataInterface.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder) 
        { 
            builder
                .HasKey(cd => new { cd.CartId, cd.ProductId });

            builder
                .HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartId);

            builder
                .HasOne(cd => cd.Product)
                .WithMany(p => p.CartDetails)
                .HasForeignKey(cd => cd.ProductId);
        }
    }
}
