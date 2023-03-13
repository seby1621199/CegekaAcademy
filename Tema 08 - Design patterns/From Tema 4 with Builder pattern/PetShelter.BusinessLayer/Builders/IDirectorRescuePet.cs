using PetShelter.BusinessLayer.Models;
using PetShelter.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.BusinessLayer.Builders
{
    public interface IDirectorRescuePet
    {
        RescuePetRequest setDefaultCat();

    }   
}
