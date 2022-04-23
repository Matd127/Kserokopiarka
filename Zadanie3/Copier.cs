using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {
        public new int Counter { get; set; }
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }

        public Printer printer = new();
        public Scanner scanner = new();

        //public new void PowerOn(){
        //    if (state == IDevice.State.off)
        //        base.PowerOn();
        //}

        //public new void PowerOff() {
        //    if (state == IDevice.State.on)
        //        base.PowerOff();
        //}

        public void Print(in IDocument document) {
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


    }
}
