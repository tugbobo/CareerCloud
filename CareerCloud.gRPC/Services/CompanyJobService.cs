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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public override Task<CompanyJobPayload> ReadCompanyJob(CompanyJobIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobPayload>(() => new CompanyJobPayload()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                IsCompanyHidden = poco.IsCompanyHidden,
                IsInactive = poco.IsInactive,
                ProfileCreated = Timestamp.FromDateTime(poco.ProfileCreated)
            });
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            CompanyJobPoco[] pocos = { new CompanyJobPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                IsCompanyHidden = request.IsCompanyHidden,
                IsInactive = request.IsInactive,
                ProfileCreated = request.ProfileCreated.ToDateTime()
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            CompanyJobPoco[] pocos = { new CompanyJobPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                IsCompanyHidden = request.IsCompanyHidden,
                IsInactive = request.IsInactive,
                ProfileCreated = request.ProfileCreated.ToDateTime()
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new CompanyJobPoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}