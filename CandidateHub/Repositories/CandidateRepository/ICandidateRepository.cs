using CandidateHub.Domain.Entities;

namespace CandidateHub.Repositories.CandidateRepository
{
    public interface ICandidateRepository
    {
        Task InsertAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);

        Task<Candidate> GetById(string Id);
        Task<Candidate> GetByEmail(string email);
        Task<List<Candidate>> GetAll();

    }
}
