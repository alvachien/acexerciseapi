using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using acexerciseapi.Models;
using System.Data;
using System.Data.SqlClient;

namespace acexerciseapi.Controllers
{
    [Produces("application/json")]
    [Route("api/ExerciseItem")]
    public class ExerciseItemController : Controller
    {
        // GET: api/ExerciseItem
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ExerItemViewModel> listRst = new List<ExerItemViewModel>();
            Boolean bError = false;
            String strErrMsg = "";

            try
            {
                SqlConnection conn = new SqlConnection(Startup.DBConnectionString);
                try
                {
                    await conn.OpenAsync();
                    String queryString = @"SELECT [ID],[Question],[Answer],[CreatedBy],[CreatedAt],[Types] FROM [dbo].[t_exitems] ";
                    SqlCommand cmd = new SqlCommand(queryString, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ExerItemViewModel vm = new ExerItemViewModel()
                            {
                                ID = reader.GetInt32(0),
                                Question = reader.GetString(1),
                                Answer = reader.GetString(2),
                                CreatedBy = reader.GetString(3),
                                CreatedAt = reader.GetDateTime(4)
                            };
                            if (!reader.IsDBNull(5))
                                vm.Types = reader.GetString(5);
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
            catch (Exception exp2)
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

        // GET: api/ExerciseItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return BadRequest();
        }
        
        // POST: api/ExerciseItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ExerItemViewModel vm)
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

                queryString = @"INSERT INTO [dbo].[t_exitems]
                       ([Question]
                       ,[Answer]
                       ,[CreatedBy]
                       ,[CreatedAt]
                       ,[Types])
                 VALUES
                       (@Question
                       ,@Answer
                       ,@CreatedBy
                       ,@CreatedAt
                       ,@Types );
                SELECT @Identity = SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@Question", vm.Question);
                cmd.Parameters.AddWithValue("@Answer", vm.Answer);
                cmd.Parameters.AddWithValue("@CreatedBy", vm.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);                
                if (String.IsNullOrEmpty(vm.Types))
                    cmd.Parameters.AddWithValue("@Types", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Types", vm.Types);

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
        
        // PUT: api/ExerciseItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string value)
        {
            return BadRequest();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return BadRequest();
        }
    }
}
