import type { Person } from "./Person";
import type { PersonInformation } from "./PersonInformation";


export class Fundraiser{
    name: string;
    description: string;
    target: number;
    endDate: Date;
    owner: PersonInformation;
    constructor(name: string, description: string, target: number, endDate: Date, owner: PersonInformation) {
        this.name = name;
        this.description = description;
        this.target = target;
        this.endDate = endDate;
        this.owner = owner;
    }
}