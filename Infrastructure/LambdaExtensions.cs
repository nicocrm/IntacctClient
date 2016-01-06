using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace Intacct.Infrastructure
{
	internal static class LambdaExtensions
	{
		public static void SetPropertyValue<TObj, TProp>(this TObj target, Expression<Func<TObj, TProp>> memberLamda, XElement data, bool isOptional = false)
		{
			var memberSelectorExpression = memberLamda.Body as MemberExpression;
			var property = memberSelectorExpression?.Member as PropertyInfo;

			if (property == null) throw new ArgumentException("Lambda expression is not a valid property member express. Expected syntax is x => x.Prop.", nameof(memberLamda));

			// get Intacct field name
			var fieldName = property.GetCustomAttribute<IntacctNameAttribute>()?.Name ?? property.Name.ToLowerInvariant();

			// get corresponding element
			var element = data.Element(fieldName);
			if (element == null)
			{
				if (isOptional) return;
				throw new ArgumentException($"Intacct data XML element does not contain child element \"{fieldName}\".");
			}

			SetValue(property, target, element);
		}

		private static void SetValue(PropertyInfo property, object target, XElement data)
		{
			if (property.PropertyType == typeof(string))
			{
				property.SetValue(target, data.Value, null);
				return;
			}

			if (property.PropertyType == typeof(int))
			{
				property.SetValue(target, Convert.ToInt32(data.Value), null);
			}

			throw new ArgumentException($"Property type {property.PropertyType.Name} is not yet supported.");
		}
	}
}
