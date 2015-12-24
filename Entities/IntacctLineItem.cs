using System;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctLineItem : IntacctObject
	{
		public string AccountNumber { get; private set; }
		public string AccountLabel { get; private set; }
		public decimal Amount { get; }

		public string Memo { get; set; }

		public static IntacctLineItem CreateWithAccountNumber(string accountNumber, decimal amount)
		{
			return new IntacctLineItem(amount) { AccountNumber = accountNumber };
		}

		public static IntacctLineItem CreateWithAccountLabel(string accountLabel, decimal amount)
		{
			return new IntacctLineItem(amount) { AccountLabel = accountLabel };
		}

		private IntacctLineItem(decimal amount)
		{
			Amount = amount;
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
				       {
					       GetAccountXmlElement(),
					       new XElement("amount", Amount),
						   new XElement("memo", Memo ?? ""), 
				       };
		}

		private XElement GetAccountXmlElement()
		{
			if (!string.IsNullOrWhiteSpace(AccountNumber)) return new XElement("glaccountno", AccountNumber);
			if (!string.IsNullOrWhiteSpace(AccountLabel)) return new XElement("accountlabel", AccountLabel);

			throw new Exception("Unable to generate line item XML because neither account number or label are set.");
		}
	}
}
