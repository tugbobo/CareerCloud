using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        public override Task<CompanyProfilePayload> ReadCompanyProfile(CompanyProfileIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyProfilePayload>(() => new CompanyProfilePayload()
            {
                Id = poco.Id.ToString(),
                RegistrationDate = Timestamp.FromDateTime(poco.RegistrationDate),
                CompanyWebsite = poco.CompanyWebsite,
                ContactPhone = poco.ContactPhone,
                ContactName = poco.ContactName,
                CompanyLogo = ByteString.CopyFrom(poco.CompanyLogo)
            });
        }

        public override Task<Empty> CreateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            CompanyProfilePoco[] pocos = { new CompanyProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = request.CompanyLogo.ToByteArray()
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            CompanyProfilePoco[] pocos = { new CompanyProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = request.CompanyLogo.ToByteArray()
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new CompanyProfilePoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}