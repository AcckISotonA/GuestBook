using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GuestBookApi.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class UserNameValidation : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}

			if (value != null)
			{
				if (!Regex.IsMatch(value.ToString(), @"\d+([a-z])+|([a-z])+\d+|([a-z])+", RegexOptions.IgnoreCase) || Regex.IsMatch(value.ToString(), @"[^a-z\d]+", RegexOptions.IgnoreCase))
						return new ValidationResult("User Name должен содержать цифры и буквы латинского алфавита");
			}

			return ValidationResult.Success;
		}
	}
}
