using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.Web.Controllers
{
    
    [ApiController]
    [Route("api/applicant")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ILogger<ApplicantController> _logger;
        public ApplicantController(IApplicantService applicantService, ILogger<ApplicantController> logger)
        {
            _applicantService = applicantService;
            _logger = logger;

        }
        [EnableCors("AllowOrigin")]
        [HttpPost]
        //[EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateApplicant([FromBody] Applicant app)
        {
            try
            {
                bool valid = _applicantService.CreateApplicant(app);
                var result = _applicantService.GetLast();
                return Created("/api/applicant ", result);
            }         
            catch(Exception ex)
            {
                _logger.LogInformation("CreateApplicant error: "+ex.ToString());
                return BadRequest();
            }   
                
        }
        /*
        [EnableCors("AllowOrigin")]
        [HttpOptions]
        //[EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateApplicantOp([FromBody] Applicant app)
        {
            try
            {
                bool valid = _applicantService.CreateApplicant(app);
                var result = _applicantService.GetLast();
                return Created("/api/applicant ", result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CreateApplicant error: " + ex.ToString());
                return BadRequest();
            }

        }*/


        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetApplicant(int id)
        {
            try
            {
                var result = _applicantService.GetApplicant(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("GetApplicant error: " + ex.ToString());
                return NotFound();
            }
        }


        [EnableCors("AllowOrigin")]
        [HttpPut]
        //[EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateApplicant([FromBody] Applicant app)
        {
            try
            {
                bool valid = _applicantService.UpdateApplicant(app);   
                
                return Ok();                
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UpdateApplicant error: " + ex.ToString());
                return BadRequest();
            }

        }

        [EnableCors("AllowOrigin")]
        [HttpDelete]
        //[EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteApplicant(int id)
        {
            try
            {
                bool valid = _applicantService.DeleteApplicant(id);
                if (valid)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UpdateApplicant error: " + ex.ToString());
                return BadRequest();
            }

        }
        /*
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetApplicant(int id)
        {
            var result = _applicantService.GetApplicant(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        */
    }
}
