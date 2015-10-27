using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Intacct
{
	internal class ResponseParser
	{
		public static IntacctServiceResponse Parse(XDocument doc, IEnumerable<IIntacctOperation> operations)
		{
			var control = doc.Descendants("control");
			var success = control.Elements("status").Single().Value == "success";

			if (! success) return ParseErrorsIntoResponse(doc.Descendants("errormessage").First());

			var response = IntacctServiceResponse.Successful;

			ParseOperations(response, operations, doc.Descendants("operation"));

			return response;
		}

		public static IEnumerable<IntacctError> ParseErrors(XContainer errorMessage)
		{
			return errorMessage.Elements("error").Select(IntacctError.FromXml);
        }

		private static void ParseOperations(IntacctServiceResponse response, IEnumerable<IIntacctOperation> operations, IEnumerable<XElement> operationElements)
		{
			foreach (var operationElement in operationElements)
			{
				var authSuccess = operationElement.Element("authentication").Element("status").Value == "success";
				if (! authSuccess)
				{
					var errorMessageElement = operationElement.Element("errormessage");
					response.AddOperationResult(IntacctOperationAuthFailedResult.Create(ParseErrors(errorMessageElement)));
					continue;
				}

				var opResponses = from resultElement in operationElement.Elements("result")
								  let functionName = resultElement.Element("function").Value
								  let controlId = resultElement.Element("controlid").Value
								  join operation in operations on new { F = functionName, C = controlId } equals new { F = operation.FunctionName, C = operation.Id }
								  select new
								  {
									  Operation = operation,
									  Result = resultElement
								  };
				foreach (var operationAndResponse in opResponses)
				{
					var result = operationAndResponse.Operation.ProcessResult(operationAndResponse.Result);
					response.AddOperationResult(result);
				}

			}
		}

		private static IntacctServiceResponse ParseErrorsIntoResponse(XContainer errorMessage)
		{
			var response = IntacctServiceResponse.Failed;
			response.AddErrors(ParseErrors(errorMessage));

			return response;
		}
	}
}
