using BusinessLogic.Entities.Filter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.EntitiesConfigs
{
    public class AdvertValueConfig : IEntityTypeConfiguration<AdvertValue>
    {
        public void Configure(EntityTypeBuilder<AdvertValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.FilterValue).WithMany(x => x.Values).HasForeignKey(x => x.FilterValueId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Advert).WithMany(x => x.Values).HasForeignKey(x => x.AdvertId);
        }
    }
}
