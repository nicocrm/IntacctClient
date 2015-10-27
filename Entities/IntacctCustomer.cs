using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctCustomer : IntacctObject
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ExternalId { get; set; }

		public IntacctContact PrimaryContact { get; set; }

		internal override XObject[] ToXmlElements()
		{
			var elements = new List<XObject>
			{
				new XElement("customerid", Id),
				new XElement("name", Name),
				new XElement("externalid", ExternalId),
			};

			if (PrimaryContact != null)
			{
				elements.Add(new XElement("primary", new XElement("contact", PrimaryContact.ToXmlElements())));
			}

			return elements.ToArray();
		}
	}
}
