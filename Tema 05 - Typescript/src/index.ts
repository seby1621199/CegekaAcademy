import { Fundraiser } from './models/Fundraiser';
import { Person } from './models/Person';
import { PersonInformation } from './models/PersonInformation';
import { Pet } from './models/Pet';
import { FundraiserService } from './services/FundraiserService';
import { PetService } from './services/PetService';

let service = new FundraiserService();

service.CreateFundraiser(new Fundraiser("Fundraiser 1", "Fundraiser 1 description", 1000, new Date(), new PersonInformation(
    "SS",
    new Date(),
    "6040213445271"))).then(response => console.log(response));
// var button = document.createElement("button");

// button.innerHTML = "Close Fundraiser 1";
// button.onclick = function () {
//     service.CloseFundraiser(1).then(response => console.log(response));
// }

// document.body.appendChild(button);