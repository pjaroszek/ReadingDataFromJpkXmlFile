using System;
using Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile.Services;

namespace Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile
{
    class Program
    {
        private static readonly string file = @".xml";

        static void Main(string[] args)
        {
            var readingDataFromJpk = new ReadingDataFromJpkFile();
            readingDataFromJpk.ReadFile(file);

            Console.ReadLine();
        }
    }
}
