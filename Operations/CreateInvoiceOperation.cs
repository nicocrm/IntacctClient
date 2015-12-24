using System;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
	public class CreateInvoiceOperation : IntacctAuthenticatedOperationBase<string>
	{
		private readonly IntacctInvoice _invoice;

		public CreateInvoiceOperation(IIntacctSession session, IntacctInvoice invoice) : base(session, "create_invoice", "key")
		{
			if (invoice == null) throw new ArgumentNullException(nameof(invoice));

			_invoice = invoice;
		}

		protected override XObject[] CreateFunctionContents()
		{
			return _invoice.ToXmlElements();
		}

		protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
		{
			return new IntacctOperationResult<string>(responseData.Value);
		}
	}
}
