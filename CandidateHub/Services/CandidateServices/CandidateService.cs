using CandidateHub.Domain.Entities;
using CandidateHub.Repositories.CandidateRepository;

namespace CandidateHub.Services.CandidateServices
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        public async Task<Candidate> CreateOrUpdateAsync(Candidate candidate)
        {
            var response =await candidateRepository.GetByEmail(candidate.Email);
            if (response == null)
            {

                await candidateRepository.InsertAsync(candidate);
            }
            else
            {
                await candidateRepository.UpdateAsync(candidate);
            }

            return candidate;
        }

        public async Task<List<Candidate>> GetAllAsync()
        {
            var response = await candidateRepository.GetAll();

            return response;
        }
    }
}
