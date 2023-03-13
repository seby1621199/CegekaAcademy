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
    public interface IBuilderRescuePet
    {
        void setName(string name);
        void setDescription(string description);
        void setImageUrl(string url);
        void setIsHealty(bool isHeal);
        void setWeightInKg(decimal weight);
        void setPerson(Models.Person person);
        void setPetType(PetType type);
        RescuePetRequest build();
        void reset();
    }
}
