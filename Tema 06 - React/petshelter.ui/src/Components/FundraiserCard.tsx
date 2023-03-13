import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, Popover } from '@mui/material';
import { FundaiserSummaryInfo } from '../Models/FundraiserSummaryinfo';
import { DonationForm } from './DonationForm';
import { Fragment, useEffect, useRef, useState } from "react";
import { FundraiserService } from '../Services/FundraiserService';

interface FundraiserCardProps {
  fundraiser: FundaiserSummaryInfo;
  
}


interface FundraiserCardProps {
  fundraiser: FundaiserSummaryInfo;
  fundraiserService: FundraiserService;
}

export default function FundraiserCard(props: FundraiserCardProps) {
  const [isPopupOpen, setIsPopupOpen] = useState(false);
  const cardActionRef = useRef(null);

  

  const handlePopupOpen = () => {
    if (props.fundraiser.status === "Active") {
      setIsPopupOpen(true);
    }
    else {
      alert("This fundraiser is not active");
    }
  }

  function handleDonationButtonClick() {
    setIsPopupOpen(false);
  }

  

  return (
    <div>
      <Card style={{ backgroundColor: '#f0f0f0' }}>
        <CardActionArea onClick={handlePopupOpen} ref={cardActionRef}>
          <CardContent>
            <Typography gutterBottom variant="h6" component="div">
              {props.fundraiser.name}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Status: 
              {props.fundraiser.status === "Active" ? <span style={{ color: "green" }}>{props.fundraiser.status}</span> : <span style={{ color: "red" }}>{props.fundraiser.status}</span>}
            </Typography>
          </CardContent>
        </CardActionArea>
      </Card>
      <Popover
        open={isPopupOpen}
        anchorEl={cardActionRef.current}
        onClose={() => setIsPopupOpen(false)}>
        <DonationForm
          fundraiserService={props.fundraiserService}
          onClose={handleDonationButtonClick}
          fundraiserId={props.fundraiser.Id}
        ></DonationForm>
        
      </Popover>
    </div>
  );
}