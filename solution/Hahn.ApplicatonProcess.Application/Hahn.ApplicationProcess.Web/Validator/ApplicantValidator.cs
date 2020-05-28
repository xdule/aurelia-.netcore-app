using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.Web.Validator
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(5, 100).WithMessage("Please insert a name with more than 5 characters");

            RuleFor(x => x.FamiliyName).NotEmpty().Length(5, 100).WithMessage("Please insert a last name with more than 5 characters");

            RuleFor(x => x.Address).NotEmpty().Length(10, 100).WithMessage("Please insert an address with more than 10 characters");

            RuleFor(x => x.Age).InclusiveBetween(20,60).WithMessage("Please insert an age between 20 - 60 y");

            RuleFor(x => x.EmailAddress).Matches(@"^[\w.-]+@(?=[a-z\d][^.]*\.)[a-z\d.-]*[^.]$").WithMessage("Not a valid mail !")
                .NotEmpty() // <-- and cant be empty
                .WithMessage("Mail missing, not valid");

            RuleFor(x => x.CountryOfOrigin).NotEmpty().WithMessage("Please state the country").Custom(async (obj, context) => {
                
                var result =await ValidateCountry(obj);
                if (!result)
                {
                    context.AddFailure("CountryOfOrigin","Please insert a valid country!");                    
                }
            });


        }
        static async Task<bool> ValidateCountry(string CountryOfOrigin)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync("https://restcountries.eu/rest/v2/name/" + CountryOfOrigin + "?fullText=true");
            if (result.IsSuccessStatusCode)
                return true;
            else return false;
        }
    }
}
