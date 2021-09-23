using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    public class RegistrationController : ApiController
    {
        string ConnectionString = ConfigurationManager.
            ConnectionStrings["MyDBConnectionString"].ConnectionString;

        // GET: api/Registration
        [HttpGet]
        [Route("api/GetRegistrationData")]
        public HttpResponseMessage getAllUserData()
        {
            DataSet dtResult = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 0,
                        CommandText = "SELECT * FROM registration"
                    };
                    SqlDataAdapter sda = new SqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    sda.Fill(dtResult); 
                    return Request.CreateResponse(HttpStatusCode.OK, dtResult);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }


        [HttpPost]
        [Route("api/SaveRegistrationData")]
        public HttpResponseMessage SaveRegistrationData()
        {
            DataSet dtResult = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 0,
                        CommandText = "insert into registraion select "
                    };
                    SqlDataAdapter sda = new SqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    sda.Fill(dtResult);
                    return Request.CreateResponse(dtResult);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(dtResult);
                }
            }
        }


        // POST: api/Registration
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Registration/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Registration/5
        public void Delete(int id)
        {
        }
    }
}
