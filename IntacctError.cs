namespace IntacctClient
{
	public class IntacctError
	{
		public string Number		{ get; private set; }
		public string Description	{ get; private set; }
		public string Description2	{ get; private set; }
		public string Source		{ get; private set; }
		public string Correction	{ get; private set; }

		internal IntacctError() {}
	}
}
