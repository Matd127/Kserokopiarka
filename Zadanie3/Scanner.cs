using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Scanner : IScanner
    {
        public int Counter { get; set; }
        public IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            if (state == IDevice.State.on)
                state = IDevice.State.off;
        }
        public void PowerOn()
        {
            if (state == IDevice.State.off)
                state = IDevice.State.on;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = new PDFDocument(filename: null);
            if (state == IDevice.State.off) return;

            Counter++;
            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(filename: " PDFScan" + Counter + ".pdf");

            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(filename: " ImageScan " + Counter + ".jpg");

            if (formatType == IDocument.FormatType.TXT)
                document = new TextDocument(filename: " TextScan " + Counter + ".txt");

            Console.WriteLine(DateTime.Now + " Scan:" + document.GetFileName());
        }
    }
}
