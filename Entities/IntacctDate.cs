using System;
using System.Xml.Linq;

namespace Intacct.Entities
{
	public class IntacctDate : IntacctObject
	{
		public int Day { get; }
		public int Month { get; }
		public int Year { get; }

		public IntacctDate(DateTime date) : this(date.Day, date.Month, date.Year) {}

		public IntacctDate(int day, int month, int year)
		{
			if (month <= 0 || month > 12) throw new ArgumentOutOfRangeException(nameof(month));
			if (year <= 0) throw new ArgumentOutOfRangeException(nameof(year));
			if (day <= 0 || day > 31 || day > DateTime.DaysInMonth(year, month)) throw new ArgumentOutOfRangeException(nameof(day));

			Day = day;
			Month = month;
			Year = year;
		}

		internal override XObject[] ToXmlElements()
		{
			return new XObject[]
			{
				new XElement("year", Year),
				new XElement("month", Month), 
				new XElement("day", Day), 
			};
		}
	}
}
