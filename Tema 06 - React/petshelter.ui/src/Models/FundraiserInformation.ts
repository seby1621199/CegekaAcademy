import type { Person } from "./Person";

export class FundraiserInformation {

    name: string;
    endDate: Date;
    target: number;
    status: string;
    current_Amount: number;
    Donors?: Person[];

    constructor(name: string, endDate: Date, target: number, status: string, current_Amount: number, Donors: Person[]) {
        this.name = name;
        this.endDate = endDate;
        this.target = target;
        this.status = status;
        this.current_Amount = current_Amount;
        this.Donors = Donors;
    }
}