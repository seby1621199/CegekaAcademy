using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration
{
    public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
    {
        public void Configure(EntityTypeBuilder<Fundraiser> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Owner).WithMany(p => p.Fundraisers).HasForeignKey(p => p.OwnerId).IsRequired();

        }
    }
}
