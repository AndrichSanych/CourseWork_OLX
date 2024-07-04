using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Entities;


namespace DataAccess.Data.EntitiesConfigs
{
    public class UserAdvertConfig : IEntityTypeConfiguration<UserAdvert>
    {
        public void Configure(EntityTypeBuilder<UserAdvert> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.UserFavouriteAdverts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Advert).WithMany(x => x.UserFavouriteAdverts).HasForeignKey(x => x.AdvertId);
        }
    }
}
