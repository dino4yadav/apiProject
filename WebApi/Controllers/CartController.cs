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
    public class CartController : ApiController
    {
        string ConnectionString = ConfigurationManager.
        ConnectionStrings["MyDBConnectionString"].ConnectionString;

        // GET: api/getCartData
        [HttpPost]
        [Route("api/getCartData")]
        public HttpResponseMessage GetProfileData([FromBody] Cart cartData)
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
                        CommandTimeout = 10,
                        CommandText = "getCartMedecine"
                    };
                    cmd.Parameters.AddWithValue("@serchText", "'%"+cartData.SearchText + "%'");
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
        [Route("api/saveCartData")]
        public HttpResponseMessage saveCartData([FromBody] Cart cartData)
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
                        CommandTimeout = 10,
                        CommandText = "SaveUserCartData"
                    };
                    cmd.Parameters.AddWithValue("@MedicineID", cartData.MedicineID);
                    cmd.Parameters.AddWithValue("@UserName", cartData.UserName);
                    cmd.Parameters.AddWithValue("@Quantity", cartData.Quantity);
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
