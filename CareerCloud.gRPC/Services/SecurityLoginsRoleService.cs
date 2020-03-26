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
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleService()
        {
            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());
        }

        public override Task<SecurityLoginsRolePayload> ReadSecurityLoginsRole(SecurityLoginsRoleIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsRolePayload>(() => new SecurityLoginsRolePayload()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Role = poco.Role.ToString()
            });
        }

        public override Task<Empty> CreateSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            SecurityLoginsRolePoco[] pocos = { new SecurityLoginsRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            SecurityLoginsRolePoco[] pocos = { new SecurityLoginsRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new SecurityLoginsRolePoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}