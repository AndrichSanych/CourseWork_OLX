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
    internal class FilterValueConfig : IEntityTypeConfiguration<FilterValue>
    {
        public void Configure(EntityTypeBuilder<FilterValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Filter).WithMany(x => x.Values).HasForeignKey(x => x.FilterId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
