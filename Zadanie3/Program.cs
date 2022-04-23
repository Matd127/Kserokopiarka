using System;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2, IDocument.FormatType.PDF);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            var multifunctionalDevice = new MultidimensionalDevice();
            multifunctionalDevice.PowerOn();
            IDocument doc3 = new PDFDocument("test1.pdf");
            multifunctionalDevice.Print(in doc3);

            IDocument doc4 = new ImageDocument("test2.JPG");
            multifunctionalDevice.Scan(out doc4, IDocument.FormatType.JPG);

            multifunctionalDevice.Send();

            IDocument doc5 = new ImageDocument("sentFile.jpg");
            multifunctionalDevice.Receive(doc5);

            Console.WriteLine("Multifunctional Device");
            Console.WriteLine("Counter:" + multifunctionalDevice.Counter);
            Console.WriteLine("PrintCounter:" + multifunctionalDevice.printer.Counter);
            Console.WriteLine("ScanCounter:" + multifunctionalDevice.scanner.Counter);
            Console.WriteLine("SendCounter:" + multifunctionalDevice.SendCounter);
            Console.WriteLine("ReceiveCounter:" + multifunctionalDevice.ReceiveCounter);
        }
    }
}
