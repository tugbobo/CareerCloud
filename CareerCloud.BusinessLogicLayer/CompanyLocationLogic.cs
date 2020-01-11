using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        { }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, "Error! Country code cannot be omitted."));
                }

                if (string.IsNullOrEmpty(poco.Province))
                {
                    exceptions.Add(new ValidationException(501, "Error! Province cannot be omitted."));
                }

                if (string.IsNullOrEmpty(poco.Street))
                {
                    exceptions.Add(new ValidationException(502, "Error! Street name cannot be omitted."));
                }

                if (string.IsNullOrEmpty(poco.City))
                {
                    exceptions.Add(new ValidationException(503, "Error! City name cannot be omitted."));
                }

                if (string.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, "Error! Postal code cannot be omitted."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}