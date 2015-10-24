using System;
using System.Net;

namespace Intacct
{
	public class IntacctUserCredential : NetworkCredential
	{
		public string CompanyId { get; }
		public string ChildCompanyId { get; }
		public ChildCompanyType? ChildCompanyType { get; }

		public IntacctUserCredential(string companyId, string userName, string password) : this(companyId, userName, password, null, null)
		{
		}

		public IntacctUserCredential(string companyId, string userName, string password, string childCompanyId, ChildCompanyType childCompanyType) : this(companyId, userName, password, childCompanyId, (ChildCompanyType?) childCompanyType)
		{
		}

		private IntacctUserCredential(string companyId, string userName, string password, string childCompanyId = null, ChildCompanyType? childCompanyType = null) : base(userName, password)
		{
			if (companyId == null) throw new ArgumentNullException(nameof(companyId));
			if (childCompanyId != null && childCompanyType == null) throw new ArgumentNullException(nameof(childCompanyType));
		
			CompanyId = companyId;
			ChildCompanyId = childCompanyId;
			ChildCompanyType = childCompanyType;
		}
	}
}
