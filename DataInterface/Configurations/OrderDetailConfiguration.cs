using DataInterface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataInterface.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder
                .HasKey(od => new { od.OrderHeaderId, od.ProductId });

            builder
                .HasOne(od => od.OrderHeader)
                .WithMany(oh => oh.OrderDetails)
                .HasForeignKey(od => od.OrderHeaderId);

            builder
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        }
    }
}
