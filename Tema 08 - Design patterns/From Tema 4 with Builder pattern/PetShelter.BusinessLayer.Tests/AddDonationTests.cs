﻿using Moq;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{


    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest
        {
            Amount = 10,
            Person = new BusinessLayer.Models.Person
            {
                DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge).AddMinutes(-3),
                IdNumber = "1111222233334",
                Name = "TestName"
            }
        };
        await donationServiceSut.AddDonation(request);

        mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == request.Amount)), Times.Once);
    }

    [Fact]
    public async Task GivenRequestWithMissingAmount_WhenAddDonation_DonationIsNotAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest();
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }
    //

    [Fact]
    public async Task GivenRequestWIthNegativeAmount_WhenAddDonation_DonationIsNotAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest
        {
            Amount = -70
        };
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    public async Task GivenRequestWithWrongPersonName_WhenAddDonation_DonationIsNotAdded(string name)
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest
        {
            Amount = 700,
            Person = new BusinessLayer.Models.Person
            {
                Name = name
            }
        };
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }


    [Fact]
    public async Task GivenRequestWithWrongPersonIdNumber_WhenAddDonation_DonationIsNotAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest
        {
            Amount = 700,
            Person = new BusinessLayer.Models.Person
            {
                IdNumber = "902906",
                Name = "salut"
            }
        };
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithWrongPersonAge_WhenAddDonation_DonationIsNotAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest
        {
            Amount = 700,
            Person = new BusinessLayer.Models.Person
            {
                DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge + 3),
                IdNumber = "1161222233335",
                Name = "TestName"
            }
        };
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);

    }

}
