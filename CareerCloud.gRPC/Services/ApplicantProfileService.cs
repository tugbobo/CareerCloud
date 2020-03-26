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
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<ApplicantProfilePayload> ReadApplicantProfile(ApplicantProfileIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantProfilePayload>(() => new ApplicantProfilePayload()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Street = poco.Street,
                City = poco.City,
                Province = poco.Province,
                PostalCode = poco.PostalCode,
                Country = poco.Country,
                Currency = poco.Currency,
                CurrentRate = (double?)poco.CurrentRate,
                CurrentSalary = (double?)poco.CurrentSalary
            });
        }

        public override Task<Empty> CreateApplicantProfile(ApplicantProfilePayload request, ServerCallContext context)
        {
            ApplicantProfilePoco[] pocos = { new ApplicantProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                CurrentSalary = (decimal?)request.CurrentSalary,
                CurrentRate = (decimal?)request.CurrentRate,
                Currency = request.Currency,
                Street =request.Street,
                City = request.City,
                Province = request.Province,
                Country = request.Country,
                PostalCode=request.PostalCode,
                Login = Guid.Parse(request.Login)
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfilePayload request, ServerCallContext context)
        {
            ApplicantProfilePoco[] pocos = { new ApplicantProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                CurrentSalary = (decimal?)request.CurrentSalary,
                CurrentRate = (decimal?)request.CurrentRate,
                Currency = request.Currency,
                Street =request.Street,
                City = request.City,
                Province = request.Province,
                Country = request.Country,
                PostalCode=request.PostalCode,
                Login = Guid.Parse(request.Login)
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfilePayload request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new ApplicantProfilePoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}
