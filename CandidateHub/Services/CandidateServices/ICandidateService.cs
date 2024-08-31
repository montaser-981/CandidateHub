using CandidateHub.Domain.Entities;

namespace CandidateHub.Services.CandidateServices
{
    public interface ICandidateService
    {
        Task<Candidate> CreateOrUpdateAsync(Candidate candidate);
        Task<List<Candidate>> GetAllAsync();
    }
}
