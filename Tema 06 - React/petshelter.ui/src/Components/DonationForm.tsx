import React, { useState } from 'react';
import { Button, TextField } from '@mui/material';
import { FundraiserService } from '../Services/FundraiserService';

interface DonatorData {
  name: string;
  dateOfBirth: string;
  idNumber: string;
}

interface DonationData {
  id: number;
  amount: number;
  donator: DonatorData;
}

interface FundraiserCardProps {
  fundraiserService: FundraiserService;
  onClose: () => void;
  fundraiserId: number;
}



function donate(fundraiserService: FundraiserService, donationData: DonationData) {
  try{
    fundraiserService.DonateToFundraiser(donationData.id, donationData.amount, { name: donationData.donator.name, dateOfBirth: new Date(donationData.donator.dateOfBirth), idNumber: donationData.donator.idNumber });
  } catch (error) {
  }
}



export function DonationForm(props: FundraiserCardProps) {
  const [amount, setAmount] = useState(0);
  const [name, setName] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState('');
  const [idNumber, setIdNumber] = useState('');
  const [showComponent, setShowComponent] = useState(true);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const donationData: DonationData = {
      id: props.fundraiserId, 
      amount,
      donator: { name, dateOfBirth, idNumber }
    };


    donate(props.fundraiserService, {
      id: props.fundraiserId,
      amount,
      donator: { name, dateOfBirth, idNumber }
    })
    setShowComponent(false);
    console.log(donationData, showComponent.valueOf());
    props.onClose();
  };


  const handleAmountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAmount(Number(event.target.value));
  };

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };

  const handleDateOfBirthChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setDateOfBirth(event.target.value);
  };

  const handleIdNumberChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setIdNumber(event.target.value);
  };


  return (

    <form onSubmit={handleSubmit} style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
      <div style={{ marginBottom: '20px' }}>
        <TextField id="amount" label="Amount:" type="number" onChange={handleAmountChange} value={amount} style={{ width: '100%' }} required />
      </div>
      <div style={{ marginBottom: '20px' }}>
        <TextField id="name" label="Name:" onChange={handleNameChange} value={name} style={{ width: '100%' }} required />
      </div>
      <div style={{ marginBottom: '20px' }}>
        <TextField id="dateOfBirth" label="Date of birth:" type="date" onChange={handleDateOfBirthChange} value={dateOfBirth} InputLabelProps={{ shrink: true }} InputProps={{ style: { paddingTop: '12px', paddingBottom: '12px', width: '100%' } }} style={{ width: '100%' }} required />
      </div>
      <div style={{ marginBottom: '20px' }}>
        <TextField id="idNumber" label="ID number:" onChange={handleIdNumberChange} value={idNumber} style={{ width: '100%' }} required />
      </div>
      <Button type="submit" style={{background:"pink"}}>Donate</Button>
      </form>
      
  );

}
