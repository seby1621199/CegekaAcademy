import FundraiserCard from "../Components/FundraiserCard";
import { FundraiserService } from "../Services/FundraiserService";
import { FundaiserSummaryInfo } from '../Models/FundraiserSummaryinfo';
import { Fragment, useEffect, useState } from "react";
import { Box, Button, Container, Grid, Link } from "@mui/material";

export const Fundraisers = () => {
    const fundraiserService = new FundraiserService();
    let dataArr: Array<FundaiserSummaryInfo> = [];
    const [fundraisers, setFundraisers] = useState<FundaiserSummaryInfo[]>([]);
    useEffect(() => {
        const fetchData = async () => {
            const fundraiserData = await fundraiserService.getAll();
            setFundraisers(fundraiserData);
        }
        fetchData();
    }, [fundraiserService]);
    return (
        <Fragment>
                <Button variant="contained" color="secondary" sx={{ mt: 3, mb: 1, ml: 1 }} >
                    <Link href="/" underline="none" color="inherit">Back</Link>
                </Button>
        <Container>

                <Grid container spacing={4} sx={{ mt: 1,mb: 4 }}>
                {
                    
                    fundraisers.map((fundraiser) => (
                        <Grid item key={fundraiser.Id} xs={12} sm={6} md={4} >
                            <FundraiserCard fundraiser={fundraiser} fundraiserService={fundraiserService} ></FundraiserCard>
                        </Grid>
                    ))
                }
            </Grid>
        </Container>
    </Fragment>
    );
}
