using CandidateHub.Domain.Entities;
using CandidateHub.Domain.Model.Response;
using CandidateHub.Services.CandidateServices;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _candidateService.GetAllAsync();

            return Ok(new Response<List<Candidate>>("Success", response));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Candidate candidate)
        {
            var response = await _candidateService.CreateOrUpdateAsync(candidate);

            return Ok(new Response<Candidate>("Success Creaed", response));

        }
    }
}
