using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class MultifunctionalDevice : IPrinter, IScanner, IFax
    {
        public int Counter { get; set; }
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }
        public int SendCounter { get; set; }
        public int ReceiveCounter { get; set; }

        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            if (state != IDevice.State.off) 
                state = IDevice.State.off;
        }

        public void PowerOn()
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
        public void Recieve(in IDocument document){
            if (state == IDevice.State.off)
                return;

            ReceiveCounter++;
            Console.WriteLine("Receive: " + document.GetFileName());
            
        }

        public void Send()
        {
            if (state == IDevice.State.off)
                return;
            else {
                SendCounter++;
                IDocument document;
                Scan(out document);
                Console.WriteLine("Send: " + document.GetFileName());
            }
               
        }
    }
}
