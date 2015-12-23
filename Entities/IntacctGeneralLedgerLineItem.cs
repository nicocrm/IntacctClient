using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctGeneralLedgerLineItem : IntacctLineItemBase
	{
		public string GeneralLedgerAccountNumber { get; }

		public IntacctGeneralLedgerLineItem(string generalLedgerAccountNumber, decimal amount) : base(amount)
		{
			GeneralLedgerAccountNumber = generalLedgerAccountNumber;
		}

		protected override XElement GetAccountXmlElement()
		{
			return new XElement("glaccountno", GeneralLedgerAccountNumber);
		}
	}
}
