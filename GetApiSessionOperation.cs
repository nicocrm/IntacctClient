using System;
using System.Xml.Linq;
using System.Linq;

namespace Intacct
{
	public class GetApiSessionOperation : IntacctOperationBase
	{
		public GetApiSessionOperation(IntacctUserCredential cred) : base(cred)
		{
		}

		protected override XElement CreateOperationContents()
		{
			return new XElement("function",
			                    new XAttribute("controlid", "api"),
			                    new XElement("getAPISession"));
		}

		protected override IntacctOperationResult<IntacctSession> ProcessResponseData(XElement responseData)
		{
			var api = responseData.Descendants("api").Single();
			var sessionId = api.Elements("sessionid").Single().Value;
			var endpoint = api.Elements("endpoint").Single().Value;

			var session = new IntacctSession(sessionId, new Uri(endpoint));
			return new IntacctOperationResult<IntacctSession>(session);
		}
	}
}
