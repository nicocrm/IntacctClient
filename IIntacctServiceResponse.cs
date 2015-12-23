using System.Collections.Generic;
using Intacct.Operations;

namespace Intacct
{
	public interface IIntacctServiceResponse
	{
		bool Success { get; }
		IReadOnlyList<IntacctError> Errors { get; }
		IReadOnlyList<IntacctOperationResult> OperationResults { get; }
	}
}
