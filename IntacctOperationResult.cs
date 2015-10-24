using System.Collections.Generic;

namespace Intacct
{
	public abstract class IntacctOperationResult
	{
		public bool AuthSuccess { get; private set; }
		public bool OperationSuccess { get; private set; }
		public string OperationId { get; private set; }

		public IEnumerable<IntacctError> Errors { get; private set; }
	}

	public class IntacctOperationResult<T> : IntacctOperationResult
	{
		public T Value { get; }

		public IntacctOperationResult(T value)
		{
			Value = value;
		}
	}
}
