using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        { }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantSkillPoco poco in pocos)
            {
                if (poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, $"Error! Start month - {poco.StartMonth} cannot be greater than 12."));
                }

                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, $"Error! End month - {poco.EndMonth} cannot be greater than 12."));
                }

                if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, $"Error! Start year - {poco.StartYear} cannot come before the 1900s."));
                }

                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, $"Error! End year - {poco.EndYear} cannot come before start year - {poco.StartYear}."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}