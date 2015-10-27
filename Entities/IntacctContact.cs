using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctContact : IntacctObject
	{
		public string Name { get; set; }

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

		internal override XObject[] ToXmlElements()
		{
			return new[]
			{
				new XElement("contactname", Name),
				new XElement("printas", Name),
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
				new XElement("mailaddress", MailAddress?.ToXmlElements()),
			};
		}
	}
}