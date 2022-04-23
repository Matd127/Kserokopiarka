using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {
        public Printer printer = new();
        public Scanner scanner = new();

        public new void PowerOn(){
            if (state == IDevice.State.off)
                base.PowerOn();
        }

        public new void PowerOff() {
            if (state == IDevice.State.on){
                base.PowerOff();
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            if (state == IDevice.State.on){
                scanner.PowerOn();
                scanner.Scan(out document, formatType);
                scanner.PowerOff();
            }
            else
                document = null;
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                printer.PowerOn();
                printer.Print(document);
                printer.PowerOff();
            }
        }

        public void ScanAndPrint()
        {
            IDocument doc;
            Scan(out doc, IDocument.FormatType.JPG);
            Print(in doc);
        }


    }
}
