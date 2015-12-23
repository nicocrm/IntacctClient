using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctCustomer : IntacctObject
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ExternalId { get; set; }

		/// <summary>
		///		Create a new customer record.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		public IntacctCustomer(string id, string name)
		{
			Id = id;
			Name = name;
		}

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
				elements.Add(new XElement("primary", new XElement("contact", PrimaryContact.ToXmlElements().Cast<object>())));
			}

			return elements.ToArray();
		}
	}
}
