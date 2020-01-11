using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        { }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, "Error! Company website cannot be omitted."));
                }
                else
                {
                    Uri uri = new UriBuilder(poco.CompanyWebsite).Uri;

                    if (uri.IsWellFormedOriginalString())
                    {
                        var splitUri = uri.Host.Split('.');
                        string tld = "." + splitUri[splitUri.Length - 1];

                        if (!((tld == ".ca") || (tld == ".com") || (tld == ".biz")))
                        {
                            exceptions.Add(new ValidationException(600, "Error! Company website top-level domain name must be one of the following – .ca, .com, or .biz"));
                        }
                    }
                    else
                    {
                        exceptions.Add(new ValidationException(600, "Error! Company website must be a valid url."));
                    }
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "Error! Contact phone cannot be omitted."));
                }
                else
                {
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, "Error! Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, "Error! Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, "Error! Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException(601, "Error! Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                        }
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}