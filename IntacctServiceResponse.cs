using System.Collections.Generic;
using System.Collections.ObjectModel;
using Intacct.Operations;

namespace Intacct
{
	public class IntacctServiceResponse : IIntacctServiceResponse
	{
		private readonly List<IntacctError> _errors = new List<IntacctError>();
		private readonly List<IntacctOperationResult> _operationResults = new List<IntacctOperationResult>();

		public bool Success { get; private set; }

		public IReadOnlyList<IntacctError> Errors => new ReadOnlyCollection<IntacctError>(_errors);

		public IReadOnlyList<IntacctOperationResult> OperationResults => new ReadOnlyCollection<IntacctOperationResult>(_operationResults);

		internal static IntacctServiceResponse Failed => new IntacctServiceResponse { Success = false };
		internal static IntacctServiceResponse Successful => new IntacctServiceResponse { Success = true };

		private IntacctServiceResponse() {}

		internal void AddError(IntacctError error)
		{
			_errors.Add(error);
			Success = false;
		}

		internal void AddErrors(IEnumerable<IntacctError> errors)
		{
			_errors.AddRange(errors);
			Success = false;
		}

		internal void AddOperationResult(IntacctOperationResult result)
		{
			_operationResults.Add(result);
		}
	}
}
