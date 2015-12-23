using System.Collections.Generic;

namespace Intacct.Operations
{
	public abstract class IntacctOperationResult
	{
		public bool AuthSuccess { get; protected set; }
		public bool OperationSuccess { get; protected set; }

		public IEnumerable<IntacctError> Errors { get; protected set; }
	}

	public class IntacctOperationAuthFailedResult : IntacctOperationResult
	{
		public static IntacctOperationAuthFailedResult Create(IEnumerable<IntacctError> errors)
		{
			return new IntacctOperationAuthFailedResult { Errors = errors };
		}
	}

	public class IntacctOperationResult<T> : IntacctOperationResult
	{
		public T Value { get; }

		public IntacctOperationResult(T value)
		{
			AuthSuccess = true;
			OperationSuccess = true;

			Value = value;
		}

		public static IntacctOperationResult<T> CreateFailure(IEnumerable<IntacctError> errors)
		{
			return new IntacctOperationResult<T>(default(T)) { OperationSuccess = false, Errors = errors };
		}
	}
}
