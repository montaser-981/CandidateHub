using CandidateHub.Domain.Entities;
using FluentValidation;

namespace CandidateHub.Domain.Validaror
{
    public class CandidateValidator : AbstractValidator<Candidate>
    {
        public CandidateValidator()
        {
            RuleFor(x => x.Email)
                    .EmailAddress()
                ;

            RuleFor(x => x.FirstName)
                .NotEmpty();


            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Note)
                .NotEmpty();


        }
    }
}
