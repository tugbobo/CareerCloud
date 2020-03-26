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
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleService()
        {
            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }

        public override Task<SecurityRolePayload> ReadSecurityRole(SecurityRoleIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityRolePayload>(() => new SecurityRolePayload()
            {
                Id = poco.Id.ToString(),
                Role = poco.Role,
                IsInactive = poco.IsInactive
            });
        }

        public override Task<Empty> CreateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            SecurityRolePoco[] pocos = { new SecurityRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Role = request.Role,
                IsInactive= request.IsInactive
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            SecurityRolePoco[] pocos = { new SecurityRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Role = request.Role,
                IsInactive= request.IsInactive
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            SecurityRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new SecurityRolePoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}