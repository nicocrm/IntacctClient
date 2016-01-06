using System;

namespace Intacct.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public class IntacctNameAttribute : Attribute
	{
		public string Name { get; }

		public IntacctNameAttribute(string name)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Argument is null or whitespace", nameof(name));

			Name = name;
		}
	}
}
