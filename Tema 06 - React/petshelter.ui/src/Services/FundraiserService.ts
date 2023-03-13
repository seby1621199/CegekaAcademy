
import axios from "axios";
import { FundaiserSummaryInfo } from "../Models/FundraiserSummaryinfo";
import { FundraiserInformation } from "../Models/FundraiserInformation";
import type { Fundraiser } from "../Models/Fundraiser";

export class FundraiserService {
    private apiUrl: string = "https://localhost:7075";


    public async getAll(): Promise<FundaiserSummaryInfo[]> {
        let fundraisers: any[] = [];
        return await
            axios.get(this.apiUrl + '/Fundraisers')
                .then(response => {
                    fundraisers = response.data;
                    const mappedFundraisers = fundraisers.map(fundraiser => {
                        return new FundaiserSummaryInfo(fundraiser.id, fundraiser.name, fundraiser.status);
                    });
                    return mappedFundraisers;
                })
    }


    public getFundraiserById(id: number): Promise<FundraiserInformation> {
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
    
            return axios.post(this.apiUrl + '/Fundraisers', createdFundraiser);
    }
    
    public DonateToFundraiser(id: number, amount: number, donor: PersonDto): Promise<void> {

        return axios.post(this.apiUrl + '/Fundraisers/' + id + '/donate?amount='+amount,donor);
    }

    public CloseFundraiser(id: number): Promise<void> {

        return axios.post(this.apiUrl + '/Fundraisers/' + id + '/close');
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
