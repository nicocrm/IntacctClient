namespace IntacctClient
{
	public abstract class IntacctAuthenticatedOperationBase : IntacctOperationBase
	{
		protected IntacctAuthenticatedOperationBase(IntacctSession session) : base(session)
		{
		}
	}
}
