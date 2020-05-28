using Hahn.ApplicatonProcess.May2020.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Interface
{
    public interface IApplicantService
    {
        Applicant GetApplicant(int id);
        bool CreateApplicant(Applicant app);
        Applicant GetLast();
        bool DeleteApplicant(int id);
        bool UpdateApplicant(Applicant app);
    }
}
