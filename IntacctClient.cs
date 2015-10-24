using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Intacct
{
	public class IntacctClient
	{
		private readonly Uri _apiUri;
		private readonly NetworkCredential _serviceCredential;

		public IntacctClient(Uri apiUri, NetworkCredential serviceCredential)
		{
			_apiUri = apiUri;
			_serviceCredential = serviceCredential;
		}

		public async Task<IntacctSession> InitiateApiSession(IntacctUserCredential cred)
		{
			return await InitiateApiSession(cred, CancellationToken.None);
		}

		public async Task<IntacctSession> InitiateApiSession(IntacctUserCredential cred, CancellationToken token)
		{
			var operation = new GetApiSessionOperation(cred);

			var requestId = Guid.NewGuid().ToString();
			var body = ConstructRequestBody(requestId, new[] { operation });

			var response = await ExecuteRequest(body, token, _apiUri);
			if (!response.Success)
			{
				throw new IntacctServiceException(response);
			}

			// expecting a single operation result
			var result = response.OperationResults.OfType<IntacctOperationResult<IntacctSession>>().Single();

			return result.Value;
		}

		private string ConstructRequestBody(string requestId, IEnumerable<IntacctOperationBase> operations)
		{
			var doc = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", null) };

			var request = new XElement("request",
			                           GetControlElement(_serviceCredential, requestId));
			request.Add(operations.Select(op => op.GetOperationElement()));

			doc.Add(request);

			var builder = new StringBuilder();
			using (var writer = new StringWriter(builder))
			{
				doc.Save(writer);
			}
			return builder.ToString();
		}

		private static XElement GetControlElement(NetworkCredential serviceCredential, string requestId)
		{
			var control = new XElement("control",
			                       new XElement("senderid", serviceCredential.UserName),
			                       new XElement("password", serviceCredential.Password),
			                       new XElement("controlid", requestId),
								   new XElement("dtdversion", "3.0"));

			return control;
		}

		private async Task<IntacctServiceResponse> ExecuteRequest(string body, CancellationToken token, Uri uri = null)
		{
			uri = uri ?? _apiUri;
			
			using (var client = new HttpClient())
			{
				// set up the content for the request
				var content = new StringContent(body);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/x-intacct-xml-request");

				// execute request and throw if response is not a success code
				var response = await client.PostAsync(uri, content, token);
				response.EnsureSuccessStatusCode();

				// process response
				using (var stream = await response.Content.ReadAsStreamAsync())
				{
					var doc = XDocument.Load(stream);

					return ResponseParser.Parse(doc);
				}
			}
		}
	}
}
