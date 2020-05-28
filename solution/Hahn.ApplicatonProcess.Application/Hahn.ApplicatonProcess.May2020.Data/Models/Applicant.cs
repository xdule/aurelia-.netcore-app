using System;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.May2020.Data.Models
{
    public partial class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FamiliyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
    }
}
