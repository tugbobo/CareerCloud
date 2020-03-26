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
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationService()
        {
            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        public override Task<CompanyLocationPayload> ReadCompanyLocation(CompanyLocationIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyLocationPayload>(() => new CompanyLocationPayload()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                Street = poco.Street,
                City = poco.City,
                Province = poco.Province,
                CountryCode = poco.CountryCode,
                PostalCode = poco.PostalCode
            });
        }

        public override Task<Empty> CreateCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            CompanyLocationPoco[] pocos = { new CompanyLocationPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                Street = request.Street,
                City = request.City,
                Province = request.Province,
                CountryCode = request.CountryCode,
                PostalCode = request.PostalCode
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            CompanyLocationPoco[] pocos = { new CompanyLocationPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                Street = request.Street,
                City = request.City,
                Province = request.Province,
                CountryCode = request.CountryCode,
                PostalCode = request.PostalCode
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new CompanyLocationPoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}