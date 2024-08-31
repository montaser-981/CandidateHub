using CandidateHub.Domain.Entities;
using CandidateHub.Repositories.CacheService;
using CandidateHub.Repositories.CandidateRepository;
using CandidateHub.Services.CandidateServices;
using Microsoft.Extensions.DependencyInjection;


namespace CandidateHub.UnitTest.CandidateTest
{
    [TestClass]
    public class CandidateServiceTest
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateServiceTest()
        {
            var serviceCollection = new ServiceCollection();

            // Register your services here
            serviceCollection.AddTransient<ICandidateService, CandidateService>(); // Adjust if needed
            serviceCollection.AddTransient<ICandidateRepository, CandidateRepository>(); // Adjust if needed
            serviceCollection.AddTransient<ICacheService, CacheService>(); // Adjust if needed

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task CreateOrUpdateAsyncTest()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var candidateService = scope.ServiceProvider.GetService<ICandidateService>();
                Candidate candidate = new Candidate()
                {
                    Email = "johndoe@gmail.com",
                    FirstName = "john",
                    LastName = "doe",
                    GitHubProfile = "https://www.linkedin.com/",
                    PhoneNumber = "123",
                    LinkedInProfile = "https://www.linkedin.com/",
                    Note = "note"
                };


                await candidateService.CreateOrUpdateAsync(candidate);
            }
        }
    }
}
