using System;
using Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile.Services;

namespace Jaroszek.Proof.Of.Concept.ReadingDataFromJpkXmlFile
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadingDataFromJpkFile readingDataFromJpk = new ReadingDataFromJpkFile();

            string file = @"C:\Users\Pawel\Desktop\JPK\JPK_VAT_20180101_20180131_f91060.xml";
            readingDataFromJpk.ReadFile(file);
            Console.ReadLine();

        }
    }
}
