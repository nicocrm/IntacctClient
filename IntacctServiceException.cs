using System;

namespace Intacct
{
	public class IntacctServiceException : Exception
	{
		public IntacctServiceResponse Response { get; }

		public IntacctServiceException(IntacctServiceResponse response)
		{
			if (response == null) throw new ArgumentNullException(nameof(response));

			Response = response;
		}
	}
}