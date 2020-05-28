using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.Web.Responses
{
    public class ErrorModel 
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
      
    }
}
