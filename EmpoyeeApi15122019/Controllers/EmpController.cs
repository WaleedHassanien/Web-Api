using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using employeeDataAccess;

namespace EmpoyeeApi15122019.Controllers
{
    public class EmpController : ApiController
    {
        public HttpResponseMessage Get(string gender="All")
        {
            using ( EmployDbEntities emp = new EmployDbEntities())
            {
               switch(gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, emp.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            emp.Employees.Where(x => x.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            emp.Employees.Where(x => x.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "false choose " + gender + " enter true input "
                            );
                }
            }
        }
        [HttpGet]
        public HttpResponseMessage get(int id)
        {
            using (EmployDbEntities emp = new EmployDbEntities())
            {
                var message = emp.Employees.FirstOrDefault(x => x.Id == id);
                if (message != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Emloyee with id" + id.ToString() + "not found");
                }
            }

        }
        [HttpPost]
        public HttpResponseMessage post([FromBody] Employee emp)
        {
            try
            {
                using (EmployDbEntities employe = new EmployDbEntities())
                {
                    employe.Employees.Add(emp);
                    employe.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employe);
                    message.Headers.Location = new Uri(Request.RequestUri + emp.Id.ToString());
                    return message;
                }

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployDbEntities emp = new EmployDbEntities())
                {
                    var objemp = emp.Employees.FirstOrDefault(x => x.Id == id);
                    if (objemp == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with id " + id.ToString() + " not found");
                    }
                    else
                    {
                        emp.Employees.Remove(objemp);
                        emp.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage put(int id, [FromBody] Employee emp)
        {
            using (EmployDbEntities empEntity = new EmployDbEntities())
            {
                try
                {
                    var empObj = empEntity.Employees.FirstOrDefault(x => x.Id == id);
                    if (empObj == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with id " + id.ToString() + " not found ");

                    }
                    else
                    {
                        empObj.FirstName = emp.FirstName;
                        empObj.LastName = emp.LastName;
                        empEntity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, empObj);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
                }
            }
        }
    }
}
