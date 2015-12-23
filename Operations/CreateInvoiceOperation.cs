using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
	public class CreateInvoiceOperation : IntacctAuthenticatedOperationBase<string>
	{
		public CreateInvoiceOperation(IIntacctSession session, IntacctInvoice invoice) : base(session, "create_invoice", "key")
		{
		}

		protected override XObject[] CreateFunctionContents()
		{
			throw new NotImplementedException();
		}

		protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
		{
			throw new NotImplementedException();
		}
	}
}
