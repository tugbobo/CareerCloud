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
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService()
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        public override Task<CompanyJobSkillPayload> ReadCompanyJobSkill(CompanyJobSkillIdRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobSkillPayload>(() => new CompanyJobSkillPayload()
            {
                Id = poco.Id.ToString(),
                Importance = poco.Importance,
                Job = poco.Job.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel
            });
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            CompanyJobSkillPoco[] pocos = { new CompanyJobSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Importance = request.Importance,
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel
            } };
            _logic.Add(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            CompanyJobSkillPoco[] pocos = { new CompanyJobSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Importance = request.Importance,
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel
            } };
            _logic.Update(pocos);
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            _logic.Delete(new CompanyJobSkillPoco[] { poco });
            return new Task<Empty>(() => new Empty());
        }
    }
}