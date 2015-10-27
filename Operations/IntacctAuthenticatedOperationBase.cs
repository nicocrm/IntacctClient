namespace Intacct.Operations
{
	public abstract class IntacctAuthenticatedOperationBase<T> : IntacctOperationBase<T>
	{
		protected IntacctAuthenticatedOperationBase(IntacctSession session, string functionName, string responseElementName) : base(session, functionName, responseElementName)
		{
		}
	}
}
