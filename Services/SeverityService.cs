using AuditSeverityModule.Models;
using AuditSeverityModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityModule.Services
{
    public class SeverityService : ISeverityService
    {
        public readonly log4net.ILog logs = log4net.LogManager.GetLogger(typeof(SeverityRepo));
        private ISeverityRepo severityRepo;

        public SeverityService(ISeverityRepo severityRepo)
        {
            this.severityRepo = severityRepo;
        }
        public AuditResponse GetSeverityResponse(AuditRequest req, string token)
        {
            logs.Info(" GetSeverityResponse Method of SeverityService Called ");
           

            try
            {
                List<AuditBenchmark> listFromRepo = severityRepo.GetResponse(token);
                int count = 0;
                int acceptableNo = 0;

                if (req.Auditdetails.Questions.Question1 == false)
                    count++;
                if (req.Auditdetails.Questions.Question2 == false)
                    count++;
                if (req.Auditdetails.Questions.Question3 == false)
                    count++;
                if (req.Auditdetails.Questions.Question4 == false)
                    count++;
                if (req.Auditdetails.Questions.Question5 == false)
                    count++;

                if (req.Auditdetails.Type == listFromRepo[0].AuditType)
                    acceptableNo = listFromRepo[0].BenchmarkNoAnswers;
                else if (req.Auditdetails.Type == listFromRepo[1].AuditType)
                    acceptableNo = listFromRepo[1].BenchmarkNoAnswers;

                Random randomNumber = new Random();
                AuditResponse auditResponse = new AuditResponse();

                if (req.Auditdetails.Type=="Internal" && count<=acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = "Green";
                    auditResponse.RemedialActionDuration = "No Action Needed";
                }
                if (req.Auditdetails.Type == "Internal" && count > acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = "Red";
                    auditResponse.RemedialActionDuration = "Action to be taken in 2 weeks";
                }
                if (req.Auditdetails.Type == "SOX" && count <= acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = "Green";
                    auditResponse.RemedialActionDuration = "No Action Needed";
                }
                if (req.Auditdetails.Type == "SOX" && count > acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = "Red";
                    auditResponse.RemedialActionDuration = "Action to be taken in 1 weeks";
                }

                return auditResponse;


            }
            catch (Exception ex)
            {
                logs.Error(ex.Message);
                return null;
            }



        }
    }
}
