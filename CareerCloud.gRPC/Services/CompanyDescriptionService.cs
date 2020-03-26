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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionService()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        public override Task<CompanyDescriptionPayload> ReadCompanyDescription(CompanyDescriptionIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyDescriptionPayload>(() => new CompanyDescriptionPayload()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,
                LanguageId = poco.LanguageId
            });
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            CompanyDescriptionPoco[] pocos = { new CompanyDescriptionPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CompanyDescription = request.CompanyDescription,
                CompanyName = request.CompanyName,
                LanguageId = request.LanguageId
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            CompanyDescriptionPoco[] pocos = { new CompanyDescriptionPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CompanyDescription = request.CompanyDescription,
                CompanyName = request.CompanyName,
                LanguageId = request.LanguageId
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new CompanyDescriptionPoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}