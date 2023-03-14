using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain;
using PetShelter.Domain.Services;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundraisersController : ControllerBase
    {
        private readonly IFundraiserService _fundraiserService;

        public FundraisersController(IFundraiserService fundraiserService)
        {
            this._fundraiserService = fundraiserService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFundraiser([FromBody] Resources.Fundraiser fundraiser)
        {
            var id = await _fundraiserService.CreateFundraiser(fundraiser.AsDomainModel(), fundraiser.AsDomainModel().Owner);
            return Ok(id);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<FundraiserSummaryInformation>>> GetFundraisers()
        {
            var data = await this._fundraiserService.GetFundraisersAsync();
            return this.Ok(data.ToImmutableArray());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Domain.FundraiserInformation>> Get(int id)
        {
            var fundraiser = await this._fundraiserService.GetFundraiser(id);
            if (fundraiser is null)
            {
                return this.NotFound();
            }
            return this.Ok(fundraiser);
        }

        [HttpPost("{id}/donate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Domain.FundraiserInformation>> Donate(int id, [FromBody] Resources.Person person, [Required] int amount)
        {
            var fundraiser = await this._fundraiserService.GetFundraiser(id);
            if (fundraiser is null)
            {
                return this.NotFound();
            }
            if (fundraiser.AmountDonated >= fundraiser.Target || fundraiser.Target.Equals("Ended"))
                return this.BadRequest("Ended");
            await this._fundraiserService.DonateToFundraiser(id, person.AsDomainModel(), amount);
            return this.Ok(fundraiser);

        }

        [HttpPost("{id}/close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Domain.FundraiserInformation>> Close(int id)
        {
            var fundraiser = await this._fundraiserService.GetFundraiser(id);
            if (fundraiser is null)
            {
                return this.NotFound();
            }
            if (fundraiser.Status == "Ended")
            {
                return this.BadRequest("Already closed");
            }
            await this._fundraiserService.CloseFundraiser(id);
            fundraiser = await this._fundraiserService.GetFundraiser(id);

            return this.Ok(fundraiser);
        }








    }

}
