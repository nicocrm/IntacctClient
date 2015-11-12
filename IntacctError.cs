using System.Diagnostics;
using System.Xml.Linq;

namespace Intacct
{
	[DebuggerDisplay("{Number}: {Description2}")]
	public class IntacctError
	{
		public string Number		{ get; private set; }
		public string Description	{ get; private set; }
		public string Description2	{ get; private set; }
		public string Source		{ get; private set; }
		public string Correction	{ get; private set; }

		internal IntacctError() {}

		internal static IntacctError FromXml(XElement errorElement)
		{
			return new IntacctError
			       {
				       Number		= errorElement.Element("errorno")?.Value,
				       Description	= errorElement.Element("description")?.Value,
				       Description2	= errorElement.Element("description2")?.Value,
				       Source		= errorElement.Element("source")?.Value,
				       Correction	= errorElement.Element("correction")?.Value
			       };
		}

		public override string ToString()
		{
			return $"[ Number: {Number ?? "--"}, Description: {Description ?? "--"}, Description2: {Description2 ?? "--"}, Source: {Source ?? "--"}, Correction: {Correction ?? "--"} ]";
		}
	}
}
