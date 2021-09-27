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
    public class ProfileController : ApiController
    {
        string ConnectionString = ConfigurationManager.
           ConnectionStrings["MyDBConnectionString"].ConnectionString;

        // GET: api/Registration
        [HttpPost]
        [Route("api/GetProfileData")]
        public HttpResponseMessage GetProfileData([FromBody] UserProfileData userProfileData)
        {
            DataTable dtResult = new DataTable();
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
                        CommandText = "getProfileData"
                    };
                    cmd.Parameters.AddWithValue("@UserName", userProfileData.UserName);
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

        // GET: api/Registration
        [HttpPost]
        [Route("api/UpdateProfileData")]
        public HttpResponseMessage UpdateProfileData([FromBody] UserProfileData userProfileData)
        {
            DataTable dtResult = new DataTable();
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
                        CommandText = "UpdateProfileData"
                    };
                    cmd.Parameters.AddWithValue("@UserID", userProfileData.UserID);
                    cmd.Parameters.AddWithValue("@UserName", userProfileData.UserName);
                    cmd.Parameters.AddWithValue("@Password", userProfileData.Password);
                    cmd.Parameters.AddWithValue("@Email", userProfileData.EmailID);
                    cmd.Parameters.AddWithValue("@Phone", userProfileData.PhoneNumber);
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

    }
}
