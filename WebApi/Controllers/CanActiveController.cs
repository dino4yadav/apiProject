using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CanActiveController : ApiController
    {
        string ConnectionString = ConfigurationManager.
            ConnectionStrings["MyDBConnectionString"].ConnectionString;

        // GET: api/Registration
        [HttpGet]
        [Route("api/CanActive")]
        public HttpResponseMessage CanActive()
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
    }
}
