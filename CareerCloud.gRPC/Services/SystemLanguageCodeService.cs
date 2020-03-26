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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<SystemLanguageCodePayload> ReadSystemLanguageCode(SystemLanguageCodeIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(request.LanguageID);
            return new Task<SystemLanguageCodePayload>(() => new SystemLanguageCodePayload()
            {
                LanguageID = poco.LanguageID,
                Name = poco.Name,
                NativeName = poco.NativeName
            });
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            SystemLanguageCodePoco[] pocos = { new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName= request.NativeName
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            SystemLanguageCodePoco[] pocos = { new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName= request.NativeName
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageID);
            _logic.Delete(new SystemLanguageCodePoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}