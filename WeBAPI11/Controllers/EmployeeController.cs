using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPICURD_Demo.Models;

namespace WebAPICURD_Demo.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Employee> GetEmployees()
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                return db.Employees.ToList();
            }
        }
        public HttpResponseMessage GetEmployeeById(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                var entity = db.Employees.FirstOrDefault(e => e.Employee_Id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + " not found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeEntities db = new EmployeeEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Employee_Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                var entity = db.Employees.FirstOrDefault(e => e.Employee_Id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not found to delete");
                }
                else
                {
                    db.Employees.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                try
                {
                    var entity = db.Employees.FirstOrDefault(e => e.Employee_Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.First_Name = employee.First_Name;
                        entity.Last_Name = employee.Last_Name;
                        entity.Salary = employee.Salary;
                        entity.Joing_Date = employee.Joing_Date;
                        entity.Department = employee.Department;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}