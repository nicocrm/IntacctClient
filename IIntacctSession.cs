using System;

namespace Intacct
{
	public interface IIntacctSession
	{
		string SessionId { get; }
		Uri EndpointUri { get; }
	}
}