using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Entities;


namespace DataAccess.Data.EntitiesConfigs
{
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x => x.Advert).WithMany(x => x.Images).HasForeignKey(x => x.AdvertId);
        }
    }
}
