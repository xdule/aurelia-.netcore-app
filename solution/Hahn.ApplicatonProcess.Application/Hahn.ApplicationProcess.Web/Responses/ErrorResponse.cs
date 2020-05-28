using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.Web.Responses
{
    public class ErrorResponse 
    {
        public List<ErrorModel> Errors = new List<ErrorModel>();
    }
}
