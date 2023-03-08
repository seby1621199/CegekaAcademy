
import axios from "axios";
import { FundaiserSummaryInfo } from "../models/FundraiserSummaryinfo";
import { FundraiserInformation } from "../models/FundraiserInformation";
import type { Fundraiser } from "src/models/Fundraiser";

export class FundraiserService {
    private apiUrl: string = "https://localhost:7075";



    public getAll(): Promise<FundraiserInformationAllDto[]> {
            return axios
                .get(this.apiUrl + '/Fundraisers')
                .then(response => {

                    var FundraiserResponse: FundaiserSummaryInfo[] = [];
                    response.data.forEach((fundraiserFromApi: FundraiserInformationAllDto) => {


                        FundraiserResponse.push(
                            new FundaiserSummaryInfo(
                                fundraiserFromApi.Id,
                                fundraiserFromApi.name,
                                fundraiserFromApi.status
                            )
                        )
                    });

                    return FundraiserResponse;
                })
    }

    public getFundraiserById(id: number): Promise<FundraiserInformation> | undefined {
        return axios
        .get(this.apiUrl + '/Fundraisers/' + id)
        .then(response => {
            console.log(response.data);
            var fundraiserResponse: FundraiserInformation = new FundraiserInformation(
                response.data.name,
                response.data.endDate,
                response.data.target,
                response.data.status,
                response.data.amountDonated,
                response.data.donors
            )

            return fundraiserResponse;
        })
    }



    public CreateFundraiser(fundraiser: FundraiserDto): Promise<void> {
            
            let createdFundraiser: FundraiserDto = {
                name: fundraiser.name,
                description: fundraiser.description,
                endDate : fundraiser.endDate,
                target : fundraiser.target,
                owner : {
                    dateOfBirth : new Date(),
                    name: fundraiser.owner.name,
                    idNumber : fundraiser.owner.idNumber
                }
            }
    
            return axios.post(this.apiUrl + '/Fundraisers', createdFundraiser).then(response => response.data);
    }
    
    public DonateToFundraiser(id: number, amount: number, donor: PersonDto): Promise<void> {

        return axios.post(this.apiUrl + '/Fundraisers/' + id + '/donate?amount='+amount,donor).then(response => response.data);
    }

    public CloseFundraiser(id: number): Promise<void> {

        return axios.post(this.apiUrl + '/Fundraisers/' + id + '/close').then(response => response.data);
    }
}


interface PersonDto 
{
    name: string; 
    dateOfBirth: Date | undefined; 
    idNumber: string; 
}

interface FundraiserDto{
    name: string;
    description: string;
    target: number;
    endDate: Date;
    owner: PersonDto;
}

interface FundraiserInformationAllDto{
    Id: number;
    name: string;
    status: string;
}
