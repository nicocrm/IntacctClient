using System.Collections.Generic;

namespace Intacct
{
	public class IntacctServiceResponse
	{
		private readonly List<IntacctError> _errors = new List<IntacctError>();
		private readonly List<IntacctOperationResult> _operationResults = new List<IntacctOperationResult>();

		public bool Success { get; private set; }

		public IReadOnlyCollection<IntacctError> Errors => _errors.AsReadOnly();

		public IReadOnlyCollection<IntacctOperationResult> OperationResults => _operationResults.AsReadOnly();

		public static IntacctServiceResponse Failed => new IntacctServiceResponse { Success = false };
		public static IntacctServiceResponse Successful => new IntacctServiceResponse { Success = true };

		private IntacctServiceResponse() { }

		internal void AddError(IntacctError error)
		{
			_errors.Add(error);
			Success = false;
		}

		public void AddErrors(IEnumerable<IntacctError> errors)
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
