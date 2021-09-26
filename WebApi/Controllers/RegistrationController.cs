using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

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

        [AllowAnonymous]
        [HttpPost]
        [Route("api/CheckUserNameAndPassword")]
        public HttpResponseMessage CheckUserNameAndPassword([FromBody] UserProfileData userProfileData)
        {
            DataSet dt = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 0,
                        CommandText = "checkUserAndPassword"
                    };
                    cmd.Parameters.AddWithValue("@userName", userProfileData.UserName);
                    cmd.Parameters.AddWithValue("@password", userProfileData.Password);
                    SqlDataAdapter sda = new SqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                    sda.Fill(dt);
                    return Request.CreateResponse(dt);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(ex.Message);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/SaveRegistrationData")]
        public HttpResponseMessage SaveRegistrationData([FromBody]UserProfileData userProfileData)
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
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 0,
                        CommandText = "saveUserData"
                    };
                    cmd.Parameters.AddWithValue("@Username", userProfileData.UserName);
                    cmd.Parameters.AddWithValue("@Password", userProfileData.Password);
                    cmd.Parameters.AddWithValue("@EmailAddress", userProfileData.EmailID);
                    cmd.Parameters.AddWithValue("@PhoneNumber", userProfileData.PhoneNumber);
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
