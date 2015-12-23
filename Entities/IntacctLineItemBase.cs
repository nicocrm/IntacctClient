using System.Xml.Linq;

namespace Intacct.Entities
{
	public abstract class IntacctLineItemBase : IntacctObject
	{
		public decimal Amount { get; }

		protected IntacctLineItemBase(decimal amount)
		{
			Amount = amount;
		}

		protected abstract XElement GetAccountXmlElement();

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
			{
				GetAccountXmlElement(),
				new XElement("amount", Amount), 
			};
		}
	}
}
