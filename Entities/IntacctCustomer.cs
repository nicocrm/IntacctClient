using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
	[IntacctName("customer")]
	public class IntacctCustomer : IntacctObject
	{
		[IntacctName("customerid")]
		public string Id { get; set; }
		public string Name { get; set; }
		public string ExternalId { get; set; }

		public IntacctContact PrimaryContact { get; set; }

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

		public IntacctCustomer(XElement data)
		{
			this.SetPropertyValue(x => Id, data);
			this.SetPropertyValue(x => Name, data);
			this.SetPropertyValue(x => ExternalId, data, isOptional: true);

			var primaryContactElement = data.Element("primary");
			if (primaryContactElement != null && primaryContactElement.HasElements)
			{
				PrimaryContact = new IntacctContact(primaryContactElement);
			}
		}

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
