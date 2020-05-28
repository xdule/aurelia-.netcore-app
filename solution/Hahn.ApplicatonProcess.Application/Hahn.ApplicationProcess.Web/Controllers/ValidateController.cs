using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.Web.Controllers
{
   /* [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly IValidator<Applicant> entitet;

        public ValidateController(IValidator<Applicant> validator)
        {
            entitet = validator;
        }

        public List<string> ApplicantValidation([FromBody]Applicant app )
        {
            List<string> response = new List<string>();
            var res = entitet.Validate(app);
            if(!res.IsValid)
            {
                foreach(var err in res.Errors)
                {
                    response.Add(err.ToString());
                }
            }

            return response;

        }
    }*/
}
