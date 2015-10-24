using System;
using System.Xml.Linq;

namespace Intacct
{
	public abstract class IntacctOperationBase
	{
		private readonly IntacctUserCredential _userCredential;
		private readonly IntacctSession _session;

		protected IntacctOperationBase(IntacctUserCredential userCredential)
		{
			_userCredential = userCredential;
		}

		protected IntacctOperationBase(IntacctSession session)
		{
			_session = session;
		}

		public XElement GetOperationElement()
		{
			return new XElement("operation",
			                    CreateAuthElement(),
			                    new XElement("content", CreateOperationContents()));
		}

		protected abstract XElement CreateOperationContents();

		protected abstract IntacctOperationResult<IntacctSession> ProcessResponseData(XElement responseData);

		private XElement CreateAuthElement()
		{
			var inner = (_session != null)
				? CreateAuthSessionElement(_session)
				: CreateAuthLoginElement(_userCredential);

			return new XElement("authentication", inner);
		}

		private static XElement CreateAuthSessionElement(IntacctSession session)
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
