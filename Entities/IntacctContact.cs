using System;
using System.Linq;
using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
	public class IntacctContact : IntacctObject
	{
		[IntacctName("contactname")]
		public string Name { get; set; }
		public string PrintAs { get; set; }

		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Initial { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string CellPhone { get; set; }
		public string Pager { get; set; }
		public string Fax { get; set; }
		public string Email1 { get; set; }
		public string Email2 { get; set; }
		public string Url1 { get; set; }
		public string Url2 { get; set; }
		public string Status { get; set; }
		public IntacctAddress MailAddress { get; set; }

		public IntacctContact(string name, string printAs)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (printAs == null) throw new ArgumentNullException(nameof(printAs));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Argument is null or whitespace", nameof(name));
			if (string.IsNullOrWhiteSpace(printAs)) throw new ArgumentException("Argument is null or whitespace", nameof(printAs));

			Name = name;
			PrintAs = printAs;
		}

		public IntacctContact(XElement data)
		{
			this.SetPropertyValue(x => Name, data);
			this.SetPropertyValue(x => PrintAs, data, isOptional: true);
			this.SetPropertyValue(x => Prefix, data, isOptional: true);
			this.SetPropertyValue(x => FirstName, data, isOptional: true);
			this.SetPropertyValue(x => LastName, data, isOptional: true);
			this.SetPropertyValue(x => Initial, data, isOptional: true);
			this.SetPropertyValue(x => Phone1, data, isOptional: true);
			this.SetPropertyValue(x => Phone2, data, isOptional: true);
			this.SetPropertyValue(x => CellPhone, data, isOptional: true);
			this.SetPropertyValue(x => Pager, data, isOptional: true);
			this.SetPropertyValue(x => Fax, data, isOptional: true);
			this.SetPropertyValue(x => Email1, data, isOptional: true);
			this.SetPropertyValue(x => Email2, data, isOptional: true);
			this.SetPropertyValue(x => Url1, data, isOptional: true);
			this.SetPropertyValue(x => Url2, data, isOptional: true);
			this.SetPropertyValue(x => Status, data, isOptional: true);

			var mailAddressElement = data.Element("mailaddress");
			if (mailAddressElement != null)
			{
				MailAddress = new IntacctAddress(mailAddressElement);
			}
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
			{
				new XElement("contactname", Name),
				new XElement("printas", PrintAs),
				new XElement("prefix", Prefix),
				new XElement("firstname", FirstName),
				new XElement("lastname", LastName),
				new XElement("initial", Initial),
				new XElement("phone1", Phone1),
				new XElement("phone2", Phone2),
				new XElement("cellphone", CellPhone),
				new XElement("pager", Pager),
				new XElement("fax", Fax),
				new XElement("email1", Email1),
				new XElement("email2", Email2),
				new XElement("url1", Url1),
				new XElement("url2", Url2),
				new XElement("status", Status),
				new XElement("mailaddress", MailAddress?.ToXmlElements().Cast<object>()),
			};
		}
	}
}