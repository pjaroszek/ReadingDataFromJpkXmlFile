namespace Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile.Services
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile.Extensions;
    internal sealed class ReadingDataFromJpkFile
    {
        public void ReadFile(string fileName)
        {
            bool fileCompatibleWithTemplate = true;
            var xsdString = File.ReadAllText("Schemat_JPK_VAT(3)_v1-1.xsd");
            var xmlSchemaSet = new XmlSchemaSet();

            using (var xmlReader = XmlReader.Create(new StringReader(xsdString)))
            {
                xmlSchemaSet.Add(null, xmlReader);
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.Schemas = xmlSchemaSet;
            xmlDocument.Load(fileName);
            xmlDocument.Validate((sender, e) =>
            {
                if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
                {
                    fileCompatibleWithTemplate = false;

                    var xml = sender as XmlDocument;
                    Console.WriteLine(e.Message);
                }
            });

            if (fileCompatibleWithTemplate)
            {
                var nameTable = xmlDocument.NameTable;
                var namespaceManager = new XmlNamespaceManager(nameTable);
                namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema");
                namespaceManager.AddNamespace("tns", "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/");
                namespaceManager.AddNamespace("etd",
                    "http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/");

                var nip = xmlDocument.SelectSingleNode("//tns:JPK//tns:Podmiot1//tns:NIP", namespaceManager).ToText();
                var email = xmlDocument.SelectSingleNode("//tns:JPK//tns:Podmiot1//tns:Email", namespaceManager).ToText();

                Console.WriteLine(nip);
                Console.WriteLine(email);

                XmlNodeList nodes = xmlDocument.SelectNodes("//tns:JPK//tns:SprzedazWiersz", namespaceManager);

                if (nodes != null)
                {
                    Console.WriteLine("Sales");
                    foreach (XmlNode node in nodes)
                    {
                        var nrKontrahenta = node.SelectSingleNode("tns:NrKontrahenta", namespaceManager).ToText();
                        var nazwaKontrahenta = node.SelectSingleNode("tns:NazwaKontrahenta", namespaceManager).ToText();

                        if (!string.IsNullOrEmpty(nrKontrahenta))
                        {
                            Console.WriteLine(nrKontrahenta + " " + nazwaKontrahenta);
                        }
                    }
                }

                nodes = xmlDocument.SelectNodes("//tns:JPK//tns:ZakupWiersz", namespaceManager);

                if (nodes != null)
                {
                    Console.WriteLine("Purchase");
                    foreach (XmlNode node in nodes)
                    {
                        var nrKontrahenta = node.SelectSingleNode("tns:NrDostawcy", namespaceManager).ToText();
                        var nazwaKontrahenta = node.SelectSingleNode("tns:NazwaDostawcy", namespaceManager).ToText();

                        if (!string.IsNullOrEmpty(nrKontrahenta))
                        {
                            Console.WriteLine(nrKontrahenta + " " + nazwaKontrahenta);
                        }
                    }
                }

            }
        }

    }
}