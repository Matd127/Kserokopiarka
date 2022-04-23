using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice
    {
        public int ReceiveCounter { get; set; }
        public int SendCounter { get; set; }
        public Printer printer = new();
        public Scanner scanner = new();

        public void Print(in IDocument document)
        {
            printer.PowerOn();
            printer.Print(document);
            printer.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            scanner.PowerOn();
            scanner.Scan(out document, formatType);
            scanner.PowerOff();
        }

        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.PDF);
            Print(in document);
        }

        public void Receive(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;

            ReceiveCounter++;
            Console.WriteLine("Receive: " + document.GetFileName());

        }

        public void Send()
        {
            if (state == IDevice.State.off)
                return;
            else
            {
                SendCounter++;
                IDocument document;
                Scan(out document, IDocument.FormatType.PDF);
                Console.WriteLine("Send: " + document.GetFileName());
            }

        }
    }
}
