using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)
                .HasConversion(
               OStatus => OStatus.ToString(),
               OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)

                );
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");

            //builder.HasMany(o => o.Items)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
