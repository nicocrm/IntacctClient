namespace Intacct.Operations
{
	public abstract class IntacctAuthenticatedOperationBase<T> : IntacctOperationBase<T>
	{
		protected IntacctAuthenticatedOperationBase(IIntacctSession session, string functionName, string responseElementName) : base(session, functionName, responseElementName) {}
	}
}
