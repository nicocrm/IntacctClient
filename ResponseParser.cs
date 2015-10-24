using System.Linq;
using System.Xml.Linq;

namespace Intacct
{
	internal class ResponseParser
	{
		public static IntacctServiceResponse Parse(XDocument doc)
		{
			var control = doc.Descendants("control");
			var success = control.Elements("status").Single().Value == "success";

			return success
				? new IntacctServiceResponse()
				: ParseErrors(doc.Descendants("errormessage").First());
		}

		private static IntacctServiceResponse ParseErrors(XContainer errormessage)
		{
			var response = IntacctServiceResponse.Failure;
			response.AddErrors(errormessage.Elements("error").Select(IntacctError.FromXml));

			return response;
		}
	}
}
