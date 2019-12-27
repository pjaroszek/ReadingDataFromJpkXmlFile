namespace Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile.Extensions
{
    using System;
    using System.Globalization;
    using System.Xml;

    public static class XmlNodeExtensions
    {
        public static DateTime? ToDateTime(this XmlNode source)
        {
            if (source is null)
            {
                return null;
            }

            return DateTime.Parse(source.InnerText);
        }

        public static string ToText(this XmlNode source)
        {
            if (source is null)
            {
                return null;
            }

            return source.InnerText;
        }

        public static decimal? ToDecimal(this XmlNode source) // var cultureInfo = new CultureInfo("pl-PL");
        {
            if (source is null)
            {
                return null;
            }

            var text = source?.InnerText;

            if (decimal.TryParse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            throw new ArgumentOutOfRangeException();
        }

        public static dynamic toList(this XmlNodeList source)
        {

            foreach (XmlNode xn in source)
            {
                string firstName = xn["tns:NrKontrahenta"].InnerText;
            }

            return null;
        }

    }
}