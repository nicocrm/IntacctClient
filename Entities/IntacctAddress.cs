using System.Xml.Linq;
using Intacct.Infrastructure;

namespace Intacct.Entities
{
	public class IntacctAddress : IntacctObject
	{
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
		public string IsoCountryCode { get; set; }

		public IntacctAddress(XElement data)
		{
			this.SetPropertyValue(x => Address1, data);
			this.SetPropertyValue(x => Address2, data);
			this.SetPropertyValue(x => City, data);
			this.SetPropertyValue(x => State, data);
			this.SetPropertyValue(x => Zip, data);
			this.SetPropertyValue(x => Country, data);
			this.SetPropertyValue(x => IsoCountryCode, data);
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
				       {
					       new XElement("address1", Address1),
					       new XElement("address2", Address2),
					       new XElement("city", City),
					       new XElement("state", State),
					       new XElement("zip", Zip),
					       new XElement("country", Country),
					       new XElement("isocountrycode", IsoCountryCode),
				       };
		}
	}
}
