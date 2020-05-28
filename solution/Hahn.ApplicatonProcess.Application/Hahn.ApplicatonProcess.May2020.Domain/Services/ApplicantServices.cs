using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hahn.ApplicatonProcess.May2020.Domain.Services
{
    public class ApplicantServices: IApplicantService
    {

        public Applicant GetApplicant(int id)
        {
            using (var context = new HahnContext())
            {
                var result = context.Applicant.Where(x => x.Id == id).FirstOrDefault();
                return result;
            }            
        }

        public bool CreateApplicant(Applicant app)
        {
            try
            {
                using (var context = new HahnContext())
                {
                    context.Applicant.Add(app);
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }            
        }

        public Applicant GetLast()
        {
            try
            {
                using (var context = new HahnContext())
                {
                    var result = context.Applicant.Reverse().First();
                    return result;
                }
            }
            catch(Exception ex)
            {
                return new Applicant();
            }
        }

        public bool DeleteApplicant(int id)
        {
            try
            {
                using (var context = new HahnContext())
                {
                    var result = context.Applicant.Where(x=>x.Id==id).First();
                    context.Applicant.Remove(result);
                    context.SaveChanges();
                    result = context.Applicant.Where(x => x.Id == id).First();
                   
                    if (result == null)
                        return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateApplicant(Applicant app)
        {
            try
            {
                using (var context = new HahnContext())
                {
                    var result = context.Applicant.Where(x => x.Id == app.Id).First();
                    result = app;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*public bool UpdateApplicant(int id)
        {
            using (var context = new HahnContext())
            {
                var applicant = context.Applicant.Where(x => x.Id == id).FirstOrDefault();
                applicant.Url = "http://example.com/blog";
                context.SaveChanges();
                return true;
            }
            return false;
        }*/
    }
}
