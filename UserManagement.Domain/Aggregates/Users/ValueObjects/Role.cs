using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.Aggregates.Users.ValueObjects
{
    public class Role : Enumeration
    {
		#region Constant(s)
		public const int MaxLength = 50;
		#endregion /Constant(s)

		#region Static Member(s)
		public static readonly Role User = new(value: 0, name: Resources.DataDictionary.User);
		public static readonly Role Blogger = new(value: 1, name: Resources.DataDictionary.Blogger);
		public static readonly Role Admin = new(value: 2, name: Resources.DataDictionary.Administrator);

		public static FluentResults.Result<Role> GetByValue(int? value)
		{
			var result =
				new FluentResults.Result<Role>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.UserRole);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var role =
				FromValue<Role>(value: value.Value);

			if (role is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.UserRole);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			result.WithValue(role);

			return result;
		}
		#endregion /Static Member(s)

		private Role() : base()
		{
		}

		private Role(int value, string name) : base(value: value, name: name)
		{
		}
	}
}
