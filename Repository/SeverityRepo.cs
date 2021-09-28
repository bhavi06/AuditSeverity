using AuditSeverityModule.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverityModule.Repository
{
    public class SeverityRepo : ISeverityRepo
    {
        public readonly log4net.ILog logs = log4net.LogManager.GetLogger(typeof(SeverityRepo));

        Uri baseAddress = new Uri("https://localhost:44368/api");   
        HttpClient client;

        public SeverityRepo()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            

        }
        public List<AuditBenchmark> GetResponse(string token)
        {
            logs.Info(" GetResponse Method of SeverityRepo Called ");
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                List<AuditBenchmark> list = new List<AuditBenchmark>();
                HttpResponseMessage httpResponseMessage = client.GetAsync(client.BaseAddress + "/AuditBenchmark").Result;

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<AuditBenchmark>>(data);

                }

                return list;

            }
            catch (Exception e)
            {
                logs.Error(e.Message);
                return null;
            }

        }
    }
}
