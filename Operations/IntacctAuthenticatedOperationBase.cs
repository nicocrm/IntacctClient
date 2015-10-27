namespace Intacct.Operations
{
	public abstract class IntacctAuthenticatedOperationBase<T> : IntacctOperationBase<T>
	{
		protected IntacctAuthenticatedOperationBase(IntacctSession session) : base(session)
		{
		}
	}
}
