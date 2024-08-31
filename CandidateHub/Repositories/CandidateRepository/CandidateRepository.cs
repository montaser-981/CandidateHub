using CandidateHub.Domain.Entities;
using CandidateHub.Repositories.CacheService;
using CsvHelper;
using System.Globalization;

namespace CandidateHub.Repositories.CandidateRepository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ICacheService _candidateCache;
        private readonly string key = "Candidate";

        private readonly string filePath = "C:\\Users\\User\\source\\repos\\CandidateHub\\CandidateHub\\Candidate.csv";
        public CandidateRepository(ICacheService _candidateCache)
        {
            this._candidateCache = _candidateCache;
        }

        public Task<List<Candidate>> GetAll()
        {
            var candidateList = _candidateCache.Get<List<Candidate>>(key);
            if (candidateList != null && candidateList.Any()) return Task.FromResult(candidateList);
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Candidate>().ToList();
                if (candidateList == null) _candidateCache.Add(key, records);
                return Task.FromResult(records);
            }
        }

        public async Task<Candidate> GetByEmail(string email)
        {
            var records = await this.GetAll();
            var selected = records.FirstOrDefault(x => x.Email == email);
            return selected;

        }

        public async Task<Candidate> GetById(string Id)
        {
            var records = await this.GetAll();
            var selected = records.FirstOrDefault(x => x.Id == Id);
            return selected;
        }

        public async Task InsertAsync(Candidate candidate)
        {
            var result = await this.GetAll(); // Assuming this is an async method that fetches records
            _candidateCache.Remove(key);
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(new List<Candidate>());

                result.Add(candidate);
                csv.WriteRecords(result);

            }

            await this.GetAll();

        }

        public async Task UpdateAsync(Candidate candidate)
        {
            var records = new List<Candidate>();

            _candidateCache.Remove(key);

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var record in csv.GetRecordsAsync<Candidate>())
                {
                    records.Add(record);
                }
            }

            var recordToUpdate = records.FirstOrDefault(r => r.Email == candidate.Email);
            if (recordToUpdate != null)
            {
                recordToUpdate.TimeInterval = candidate.TimeInterval;
                recordToUpdate.FirstName = candidate.FirstName;
                recordToUpdate.LastName = candidate.LastName;
                recordToUpdate.GitHubProfile = candidate.GitHubProfile;
                recordToUpdate.LinkedInProfile = candidate.LinkedInProfile;
                recordToUpdate.Note = candidate.Note;
            }

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(records);
            }

            await this.GetAll();
        }
    }
}
