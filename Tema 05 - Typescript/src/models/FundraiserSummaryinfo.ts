export class FundaiserSummaryInfo
{
    Id: number;
    name: string;
    status: string;
    constructor(Id: number, name: string, status: string) {
        this.Id = Id;
        this.name = name;
        this.status = status;
    }
}