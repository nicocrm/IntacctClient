using System.Xml.Linq;
using Intacct.Entities;

namespace Intacct.Operations
{
	public class CreateCustomerOperation : IntacctAuthenticatedOperationBase<string>
	{
		private readonly IntacctCustomer _customer;

		public CreateCustomerOperation(IntacctSession session, IntacctCustomer customer) : base(session, "create_customer", "key")
		{
			_customer = customer;
		}

		protected override XObject[] CreateFunctionContents()
		{
			var elements = _customer.ToXmlElements();

			return elements;
		}

		protected override IntacctOperationResult<string> ProcessResponseData(XElement responseData)
		{
			return new IntacctOperationResult<string>(responseData.Value);
		}
	}
}
