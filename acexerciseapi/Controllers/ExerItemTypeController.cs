using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using acexerciseapi.Models;

namespace acexerciseapi.Controllers
{
    [Produces("application/json")]
    [Route("api/ExerItemType")]
    public class ExerItemTypeController : Controller
    {
        // GET: api/ExerItemType
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ExerItemTypeViewModel> listRst = new List<ExerItemTypeViewModel>();
            Boolean bError = false;
            String strErrMsg = "";

            // Get user name
            //var usr = User.FindFirst(c => c.Type == "sub");
            //String usrName = String.Empty;
            //if (usr != null)
            //    usrName = usr.Value;
            //else
            //    bError = true;

            try
            {
                SqlConnection conn = new SqlConnection(Startup.DBConnectionString);
                try
                {
                    await conn.OpenAsync();
                    String queryString = @"SELECT [ID],[NAME],[DETAILS] FROM [dbo].[t_extype] ";
                    SqlCommand cmd = new SqlCommand(queryString, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ExerItemTypeViewModel vm = new ExerItemTypeViewModel()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            if (!reader.IsDBNull(2))
                                vm.Details = reader.GetString(2);
                            listRst.Add(vm);
                        }
                    }
                }
                catch (Exception exp)
                {
                    System.Diagnostics.Debug.WriteLine(exp.Message);
                    bError = true;
                    strErrMsg = exp.Message;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch(Exception exp2)
            {
                System.Diagnostics.Debug.WriteLine(exp2.Message);
                bError = true;
                strErrMsg = exp2.Message;
            }

            if (bError)
            {
                return StatusCode(500, strErrMsg);
            }

            return new JsonResult(listRst);
        }

        // GET: api/ExerItemType/5
        [HttpGet("{id}", Name = "GetExItemType")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ExerItemType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ExerItemTypeViewModel vm)
        {
            if (vm == null)
            {
                return BadRequest();
            }

            // Update the database
            SqlConnection conn = new SqlConnection(Startup.DBConnectionString);
            String queryString = "";
            Boolean bError = false;
            String strErrMsg = "";

            try
            {
                await conn.OpenAsync();

                queryString = @"INSERT INTO[dbo].[t_extype] ([NAME],[DETAILS]) VALUES ( @NAME, @DETAILS );
                    SELECT @Identity = SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@NAME", vm.Name);
                if (String.IsNullOrEmpty(vm.Details))
                    cmd.Parameters.AddWithValue("@DETAILS", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@DETAILS", vm.Details);
                SqlParameter idparam = cmd.Parameters.AddWithValue("@Identity", SqlDbType.Int);
                idparam.Direction = ParameterDirection.Output;

                Int32 nRst = await cmd.ExecuteNonQueryAsync();
                vm.ID = (Int32)idparam.Value;
                cmd.Dispose();
                cmd = null;
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.Message);
                bError = true;
                strErrMsg = exp.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            if (bError)
            {
                return StatusCode(500, strErrMsg);
            }

            return new JsonResult(vm);
        }
        
        // PUT: api/ExerItemType/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
