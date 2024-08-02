using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Entities.Filter;

namespace DataAccess.Data.EntitiesConfigs
{
    public class CategoryFilterConfig : IEntityTypeConfiguration<CategoryFilter>
    {
        public void Configure(EntityTypeBuilder<CategoryFilter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Filter).WithMany(x => x.Filters).HasForeignKey(x => x.FilterId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Category).WithMany(x => x.Filters).HasForeignKey(x => x.CategoryId);
        }
    }
}
