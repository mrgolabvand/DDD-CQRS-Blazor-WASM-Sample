using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.SeedWork
{
    public abstract class Enumeration : object, System.IComparable
	{
		#region Static Member(s)
		public static IEnumerable<T> GetAll<T>() where T : Enumeration
		{
			var result1 =
				typeof(T);

			var result2 =
				result1.GetFields
				(System.Reflection.BindingFlags.Public |
				System.Reflection.BindingFlags.Static |
				System.Reflection.BindingFlags.DeclaredOnly)
				;

			var result3 =
				result2
				.Where(current => current.IsLiteral == false)
				.ToList()
				;

			var result4 =
				result3
				.Select(current => current.GetValue(null))
				;

			var result5 =
				result4.Cast<T>()
				;

			return result5;
		}

		private static bool TryParse<TEnumeration>
			(System.Func<TEnumeration, bool> predicate, out TEnumeration enumeration) where TEnumeration : Enumeration
		{
			enumeration =
				GetAll<TEnumeration>()
				.FirstOrDefault(predicate);

			return enumeration != null;
		}


		private static TEnumeration ParseByPredicate<TEnumeration>
			(System.Func<TEnumeration, bool> predicate)
			where TEnumeration : Enumeration
		{
			var matchingItem =
				GetAll<TEnumeration>()
				.FirstOrDefault(predicate);

			return matchingItem;
		}

		public static bool TryGetFromValueOrName<TEnumeration>
			(string valueOrName, out TEnumeration enumeration) where TEnumeration : Enumeration
		{
			return TryParse
				(item => item.Name == valueOrName, out enumeration) ||
				int.TryParse(valueOrName, out var value) &&
				TryParse(item => item.Value == value, out enumeration);
		}

		public static TEnumeration
			FromValue<TEnumeration>(int value) where TEnumeration : Enumeration
		{

			var matchingItem =
				ParseByPredicate<TEnumeration>(item => item.Value == value);

			return matchingItem;
		}

		#endregion /Static Member(s)

		protected Enumeration() : base()
		{
		}

		protected Enumeration(int value, string name) : base()
		{
			Value = value;
			Name = name;
		}

		public int Value { get; }

		public string Name { get; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object anotherObject)
		{
			if (anotherObject is null)
			{
				return false;
			}

			if (anotherObject is not Enumeration)
			{
				return false;
			}

			if (ReferenceEquals(this, anotherObject))
			{
				return true;
			}

			Enumeration anotherEnumeration = anotherObject as Enumeration;

			// For EF Core!
			if (GetRealType() != anotherEnumeration.GetRealType())
			{
				return false;
			}

			if (GetType() == anotherEnumeration.GetType())
			{
				return Value == anotherEnumeration.Value;
			}

			return false;
		}

		public int CompareTo(object otherObject)
		{
			int result =
				Value.CompareTo((otherObject as Enumeration).Value);

			return result;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static bool operator ==(Enumeration left, Enumeration right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(Enumeration left, Enumeration right)
		{
			return !(left == right);
		}

		public static bool operator <(Enumeration left, Enumeration right)
		{
			return left is null ? right is not null : left.CompareTo(right) < 0;
		}

		public static bool operator <=(Enumeration left, Enumeration right)
		{
			return left is null || left.CompareTo(right) <= 0;
		}

		public static bool operator >(Enumeration left, Enumeration right)
		{
			return left is not null && left.CompareTo(right) > 0;
		}

		public static bool operator >=(Enumeration left, Enumeration right)
		{
			return left is null ? right is null : left.CompareTo(right) >= 0;
		}

		private System.Type GetRealType()
		{
			System.Type type = GetType();

			if (type.ToString().Contains("Castle.Proxies."))
			{
				return type.BaseType;
			}

			return type;
		}
	}
}
