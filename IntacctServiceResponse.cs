using System.Collections.Generic;

namespace Intacct
{
	public class IntacctServiceResponse
	{
		private readonly List<IntacctError> _errors = new List<IntacctError>(); 

		public bool Success { get; private set; }

		public IReadOnlyCollection<IntacctError> Errors => _errors.AsReadOnly();

		public IReadOnlyCollection<IntacctOperationResult> OperationResults { get; private set; }

		public static IntacctServiceResponse Failure => new IntacctServiceResponse { Success = false };

		internal void AddError(IntacctError error)
		{
			_errors.Add(error);
		}

		public void AddErrors(IEnumerable<IntacctError> errors)
		{
			_errors.AddRange(errors);
		}
	}
}
