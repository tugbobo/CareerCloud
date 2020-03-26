using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantWorkHistory;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantWorkHistoryService : ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryService()
        {
            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }

        public override Task<ApplicantWorkHistoryPayload> ReadApplicantWorkHistory(ApplicantWorkHistoryIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantWorkHistoryPayload>(() => new ApplicantWorkHistoryPayload()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CompanyName = poco.CompanyName,
                CountryCode = poco.CountryCode,
                JobDescription = poco.JobDescription,
                JobTitle = poco.JobTitle,
                Location = poco.Location,
                StartMonth = poco.StartMonth,
                EndMonth = poco.EndMonth,
                StartYear = poco.StartYear,
                EndYear = poco.EndYear
            });
        }

        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco[] pocos = { new ApplicantWorkHistoryPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                Location = request.Location,
                StartMonth = (byte)request.StartMonth,
                EndMonth = (byte)request.EndMonth,
                StartYear = request.StartYear,
                EndYear = request.EndYear
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco[] pocos = { new ApplicantWorkHistoryPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                Location = request.Location,
                StartMonth = (byte)request.StartMonth,
                EndMonth = (byte)request.EndMonth,
                StartYear = request.StartYear,
                EndYear = request.EndYear
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new ApplicantWorkHistoryPoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}