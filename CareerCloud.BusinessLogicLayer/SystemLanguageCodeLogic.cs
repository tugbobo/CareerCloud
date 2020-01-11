using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemLanguageCodeLogic
	{
		protected IDataRepository<SystemLanguageCodePoco> _repository;
		public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
		{
			_repository = repository;
		}

		protected void Verify(SystemLanguageCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();
			foreach (SystemLanguageCodePoco poco in pocos)
			{
				if (string.IsNullOrEmpty(poco.LanguageID))
				{
					exceptions.Add(new ValidationException(1000, "Error! Language id cannot be omitted."));
				}

				if (string.IsNullOrEmpty(poco.Name))
				{
					exceptions.Add(new ValidationException(1001, "Error! Name cannot be omitted."));
				}

				if (string.IsNullOrEmpty(poco.NativeName))
				{
					exceptions.Add(new ValidationException(1002, "Error! Native name cannot be omitted."));
				}
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}

		public SystemLanguageCodePoco Get(string languageid)
		{
			return _repository.GetSingle(l => l.LanguageID == languageid);
		}

		public List<SystemLanguageCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

		public void Add(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Add(pocos);
		}

		public void Update(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}

		public void Delete(SystemLanguageCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}