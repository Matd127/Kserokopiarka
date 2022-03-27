using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }
        public new int Counter { get; set; }

        public new void PowerOff()
        {
            if (state != IDevice.State.off) state = IDevice.State.off;
        }

        public new void PowerOn()
        {
            if (state != IDevice.State.on)
            {
                state = IDevice.State.on;
                Counter++;
            }
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off) return;
            PrintCounter++;
            Console.WriteLine(DateTime.Now + " Print: " + document.GetFileName());
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            document = new PDFDocument(filename: null);
            if (state == IDevice.State.off) return;

            ScanCounter++;
            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(filename: " PDFScan" + ScanCounter + ".pdf");

            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(filename: " ImageScan " + ScanCounter + ".jpg");

            if (formatType == IDocument.FormatType.TXT)
                document = new TextDocument(filename: " TextScan " + ScanCounter + ".txt");

            Console.WriteLine(DateTime.Now + " Scan:" + document.GetFileName());
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.off) return;

            IDocument document;
            Scan(out document);
            Print(in document);
        }
    }
}
