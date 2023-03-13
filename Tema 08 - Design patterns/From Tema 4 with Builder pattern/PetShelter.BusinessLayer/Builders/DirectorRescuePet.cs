using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Builders
{
    public class DirectorRescuePet : IDirectorRescuePet
    {
        private readonly IBuilderRescuePet builder;

        public DirectorRescuePet(IBuilderRescuePet builder)
        {
            this.builder = builder;
        }

        public RescuePetRequest setDefaultCat()
        {
            builder.setName("Cat");
            builder.setDescription("Scottish Fold cat");
            builder.setImageUrl("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Cat03.jpg/1200px-Cat03.jpg");
            builder.setIsHealty(true);
            builder.setWeightInKg(5);
            builder.setPetType(PetType.Cat);
            builder.setPerson(new Person()
            {
                Name = "John",
                IdNumber = "1960826040573",
                DateOfBirth = new DateTime(2000, 1, 1)
            });
            return builder.build();
        }
    }
}
