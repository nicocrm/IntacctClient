using System;
using System.Xml.Linq;
using System.Linq;

namespace Intacct
{
	public class GetApiSessionOperation : IntacctOperationBase<IntacctSession>
	{
		public GetApiSessionOperation(IntacctUserCredential cred) : base(cred, "getAPISession")
		{
		}

		protected override IntacctOperationResult<IntacctSession> ProcessResponseData(XElement responseData)
		{
			var api = responseData.Descendants("api").Single();
			var sessionId = api.Elements("sessionid").Single().Value;
			var endpoint = api.Elements("endpoint").Single().Value;

			var session = new IntacctSession(sessionId, new Uri(endpoint));
			return new IntacctOperationResult<IntacctSession>(session);
		}

		protected override XElement CreateFunctionContents()
		{
			// do nothing no op required
			return null;
		}
	}
}
