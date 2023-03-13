using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;
using PetShelter.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.BusinessLayer.Builders
{
    public class BuilderRescuePet : IBuilderRescuePet
    {
        private RescuePetRequest pet = new();

        public BuilderRescuePet()
        {
            this.reset();
        }

        public RescuePetRequest build()
        {
            return pet;
        }

        public void reset()
        {
            pet = new();
        }

        public void setDescription(string description)
        {
            pet.Description = description;
        }

        public void setImageUrl(string url)
        {
            pet.ImageUrl = url;
        }

        public void setIsHealty(bool isHeal)
        {
            pet.IsHealthy = isHeal;
        }

        public void setName(string name)
        {
            pet.PetName = name;
        }

        public void setPerson(Models.Person person)
        {
            pet.Person = person;
        }

        public void setPetType(PetType type)
        {
            pet.Type = type;
        }

        public void setWeightInKg(decimal weight)
        {
            pet.WeightInKg = weight;
        }
    }
}
