using System;
using System.Linq;
using System.Xml.Linq;

namespace Intacct.Operations
{
	public interface IIntacctOperation
	{
		string FunctionName { get; }
		string Id { get; }

		XElement GetOperationElement();

		IntacctOperationResult ProcessResult(XElement resultElement);
    }

	public abstract class IntacctOperationBase<T> : IIntacctOperation
	{
		private readonly IntacctUserCredential _userCredential;
		private readonly IIntacctSession _session;
		private readonly string _resultElementName;

		public string FunctionName { get; }
		public string Id { get; } = Guid.NewGuid().ToString();

		private IntacctOperationBase(string functionName, string resultElementName)
		{
			FunctionName = functionName;
			_resultElementName = resultElementName;
		}

		protected IntacctOperationBase(IntacctUserCredential userCredential, string functionName, string resultElementName) : this(functionName, resultElementName)
		{
			_userCredential = userCredential;
		}

		protected IntacctOperationBase(IIntacctSession session, string functionName, string resultElementName) : this(functionName, resultElementName)
		{
			_session = session;
		}

		public XElement GetOperationElement()
		{
			return new XElement("operation",
								CreateAuthElement(),
								new XElement("content",
											 new XElement("function",
														  new XAttribute("controlid", Id),
														  new XElement(FunctionName,
																	   CreateFunctionContents().Cast<object>()))));
		}

		public IntacctOperationResult ProcessResult(XElement resultElement)
		{
			// check operation status
			var success = resultElement.Element("status")?.Value == "success";
			if (!success)
			{
				var errorMessageElement = resultElement.Element("errormessage");
				return IntacctOperationResult<T>.CreateFailure(ResponseParser.ParseErrors(errorMessageElement));
			}

			// parse data
			var dataElement = resultElement.Element(_resultElementName);
			if (dataElement != null) return ProcessResponseData(dataElement);

			throw new Exception($"Element {_resultElementName} was not found in response.");
		}

		protected abstract XObject[] CreateFunctionContents();

		protected abstract IntacctOperationResult<T> ProcessResponseData(XElement responseData);

		private XElement CreateAuthElement()
		{
			var inner = (_session != null)
				? CreateAuthSessionElement(_session)
				: CreateAuthLoginElement(_userCredential);

			return new XElement("authentication", inner);
		}

		private static XElement CreateAuthSessionElement(IIntacctSession session)
		{
			return new XElement("sessionid", session.SessionId);
		}

		private static XElement CreateAuthLoginElement(IntacctUserCredential cred)
		{
			var loginElement = new XElement("login",
			                                new XElement("userid", cred.UserName),
			                                new XElement("companyid", cred.CompanyId),
			                                new XElement("password", cred.Password));

			if (cred.ChildCompanyType.HasValue)
			{
				switch (cred.ChildCompanyType.Value)
				{
					case ChildCompanyType.Shared:
						loginElement.Add(new XElement("locationid", cred.ChildCompanyId));
						break;
					case ChildCompanyType.Distributed:
						loginElement.Add(new XElement("clientid", cred.ChildCompanyId));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}				

			return loginElement;
		}
	}
}
