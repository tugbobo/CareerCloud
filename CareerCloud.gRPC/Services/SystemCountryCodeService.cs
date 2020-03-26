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
using static CareerCloud.gRPC.Protos.SystemCountryCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeService()
        {
            _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
        }

        public override Task<SystemCountryCodePayload> ReadSystemCountryCode(SystemCountryCodeRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(request.Code);
            return new Task<SystemCountryCodePayload>(() => new SystemCountryCodePayload()
            {
                Code = poco.Code,
                Name = poco.Name
            });
        }

        public override Task<Empty> CreateSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            SystemCountryCodePoco[] pocos = { new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            SystemCountryCodePoco[] pocos = { new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = _logic.Get(request.Code);
            _logic.Delete(new SystemCountryCodePoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}