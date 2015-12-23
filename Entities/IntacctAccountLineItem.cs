using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctAccountLineItem : IntacctLineItemBase
	{
		public string AccountLabel { get; }

		public IntacctAccountLineItem(string accountLabel, decimal amount) : base(amount)
		{
			AccountLabel = accountLabel;
		}

		protected override XElement GetAccountXmlElement()
		{
			return new XElement("accountlabel", AccountLabel);
		}
	}
}
