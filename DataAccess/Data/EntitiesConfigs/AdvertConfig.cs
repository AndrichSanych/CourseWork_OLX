using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.EntitiesConfigs
{
    public class AdvertConfig : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.Description).HasMaxLength(3000);
            builder.Property(x => x.Date);
            builder.Property(x => x.IsNew);
            builder.Property(x => x.IsVip);
            builder.Property(x => x.Price).HasPrecision(12, 2);
            builder.HasOne(x => x.User).WithMany(x => x.Adverts).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.City).WithMany(x => x.Adverts).HasForeignKey(x => x.CityId);
            builder.HasOne(x => x.Category).WithMany(x => x.Adverts).HasForeignKey(x => x.CategoryId);
        }
    }
}
