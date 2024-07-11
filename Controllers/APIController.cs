using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Practice.Controllers
{
    [RoutePrefix("api/revenue")]
    public class APIController : ApiController
    {
        private readonly string _connectionString;

        public APIController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IHttpActionResult> InsertRevenue()
        {
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.Contents)
            {
                var buffer = await file.ReadAsByteArrayAsync();

                using (var stream = new MemoryStream(buffer))
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    bool isFirstLine = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }

                        var values = line.Split(',');

                        var revenueData = new RevenueData
                        {
                            ReportDate = DateTime.Parse(values[0]),
                            DataMonth = values[1],
                            CompanyCode = values[2],
                            CompanyName = values[3],
                            Industry = values[4],
                            MonthlyRevenue = decimal.Parse(values[5]),
                            LastMonthRevenue = decimal.Parse(values[6]),
                            LastYearMonthlyRevenue = decimal.Parse(values[7]),
                            MoMChange = decimal.Parse(values[8]),
                            YoYChange = decimal.Parse(values[9]),
                            CumulativeRevenue = decimal.Parse(values[10]),
                            LastYearCumulativeRevenue = decimal.Parse(values[11]),
                            CumulativeChange = decimal.Parse(values[12]),
                            Remarks = values[13]
                        };

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            var command = new SqlCommand("InsertRevenue", connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            command.Parameters.AddWithValue("@ReportDate", revenueData.ReportDate);
                            command.Parameters.AddWithValue("@DataMonth", revenueData.DataMonth);
                            command.Parameters.AddWithValue("@CompanyCode", revenueData.CompanyCode);
                            command.Parameters.AddWithValue("@CompanyName", revenueData.CompanyName);
                            command.Parameters.AddWithValue("@Industry", revenueData.Industry);
                            command.Parameters.AddWithValue("@MonthlyRevenue", revenueData.MonthlyRevenue);
                            command.Parameters.AddWithValue("@LastMonthRevenue", revenueData.LastMonthRevenue);
                            command.Parameters.AddWithValue("@LastYearMonthlyRevenue", revenueData.LastYearMonthlyRevenue);
                            command.Parameters.AddWithValue("@MoMChange", revenueData.MoMChange);
                            command.Parameters.AddWithValue("@YoYChange", revenueData.YoYChange);
                            command.Parameters.AddWithValue("@CumulativeRevenue", revenueData.CumulativeRevenue);
                            command.Parameters.AddWithValue("@LastYearCumulativeRevenue", revenueData.LastYearCumulativeRevenue);
                            command.Parameters.AddWithValue("@CumulativeChange", revenueData.CumulativeChange);
                            command.Parameters.AddWithValue("@Remarks", revenueData.Remarks);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            return Ok("資料匯入成功");
        }

        [HttpGet]
        [Route("select/{companyCode}")]
        public IHttpActionResult SelectRevenue(string companyCode)
        {
            List<RevenueData> revenues = new List<RevenueData>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SelectRevenue", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CompanyCode", companyCode);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        revenues.Add(new RevenueData
                        {
                            ReportDate = reader.GetDateTime(0),
                            DataMonth = reader.GetString(1),
                            CompanyCode = reader.GetString(2),
                            CompanyName = reader.GetString(3),
                            Industry = reader.GetString(4),
                            MonthlyRevenue = reader.GetDecimal(5),
                            LastMonthRevenue = reader.GetDecimal(6),
                            LastYearMonthlyRevenue = reader.GetDecimal(7),
                            MoMChange = reader.GetDecimal(8),
                            YoYChange = reader.GetDecimal(9),
                            CumulativeRevenue = reader.GetDecimal(10),
                            LastYearCumulativeRevenue = reader.GetDecimal(11),
                            CumulativeChange = reader.GetDecimal(12),
                            Remarks = reader.GetString(13)
                        });
                    }
                }
            }

            return Ok(revenues);
        }
    }

    public class RevenueData
    {
        public DateTime ReportDate { get; set; }
        public string DataMonth { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal LastMonthRevenue { get; set; }
        public decimal LastYearMonthlyRevenue { get; set; }
        public decimal MoMChange { get; set; }
        public decimal YoYChange { get; set; }
        public decimal CumulativeRevenue { get; set; }
        public decimal LastYearCumulativeRevenue { get; set; }
        public decimal CumulativeChange { get; set; }
        public string Remarks { get; set; }
    }
}
