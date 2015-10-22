using System.Collections.Generic;

namespace IntacctClient
{
	public class IntacctServiceResponse
	{
		public bool Success { get; private set; }

		public IReadOnlyCollection<IntacctOperationResult> OperationResults { get; private set; }
	}
}
