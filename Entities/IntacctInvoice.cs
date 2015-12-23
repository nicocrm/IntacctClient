using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctInvoice : IntacctObject
	{
		public string CustomerId { get; set; }

		public IntacctDate DateCreated { get; set; }

		public ICollection<IntacctLineItemBase> Items { get; }

		public IntacctInvoice(string customerId, IntacctDate dateCreated)
		{
			CustomerId = customerId;
			DateCreated = dateCreated;
			Items = new List<IntacctLineItemBase>();
		}

		public void AddItem(IntacctLineItemBase item)
		{
			Items.Add(item);
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
			{
				new XElement("customerid", CustomerId),
				new XElement("datecreated", DateCreated.ToXmlElements().Cast<object>()),
				new XElement("invoiceitems", Items.Select(item => new XElement("invoiceitem", item.ToXmlElements().Cast<object>()))), 
			};
		}
	}
}
