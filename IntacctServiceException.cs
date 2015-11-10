using System;

namespace Intacct
{
	public class IntacctServiceException : Exception
	{
		public IIntacctServiceResponse Response { get; }

		public IntacctServiceException(IIntacctServiceResponse response)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}
	}
}