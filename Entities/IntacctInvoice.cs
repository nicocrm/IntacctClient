using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctInvoice : IntacctObject
	{
		public string CustomerId { get; }

		public IntacctDate DateCreated { get; }
		public IntacctDate DateDue { get; }

		public ICollection<IntacctLineItem> Items { get; }

		public IntacctInvoice(string customerId, IntacctDate dateCreated, IntacctDate dateDue)
		{
			CustomerId = customerId;
			DateCreated = dateCreated;
			DateDue = dateDue;
			Items = new List<IntacctLineItem>();
		}

		public void AddItem(IntacctLineItem item)
		{
			Items.Add(item);
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
				       {
					       new XElement("customerid", CustomerId),
					       new XElement("datecreated", DateCreated.ToXmlElements().Cast<object>()),
					       new XElement("datedue", DateDue.ToXmlElements().Cast<object>()),
					       new XElement("invoiceitems", Items.Select(item => new XElement("lineitem", item.ToXmlElements().Cast<object>()))),
				       };
		}
	}
}
