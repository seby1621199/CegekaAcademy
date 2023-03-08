export class PersonInformation {
    name: string;
    dateOfBirth: Date;
    idNumber: string;
    constructor(name: string, dateOfBirth: Date, idNumber: string) {
        this.name = name;
        this.dateOfBirth = dateOfBirth;
        this.idNumber = idNumber;
    }
}