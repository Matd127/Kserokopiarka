using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie3;

namespace Zadanie3UnitTest
{
    [TestClass]
    public class ScannerTest
    {
        [TestMethod]
        public void Scanner_GetState_StateOff()
        {
            var scanner = new Scanner();
            scanner.PowerOff();

            Assert.AreEqual(IDevice.State.off, scanner.GetState());
        }

        [TestMethod]
        public void Scanner_GetState_StateOn()
        {
            var scanner = new Scanner();
            scanner.PowerOn();

            Assert.AreEqual(IDevice.State.on, scanner.GetState());
        }

        [TestMethod]
        public void Scanner_Scan_DeviceOff()
        {
            var scanner = new Scanner();
            scanner.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                scanner.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Scanner_Scan_DeviceOn()
        {
            var scanner = new Scanner();
            scanner.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                scanner.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Scanner_Scan_FormatTypeDocument()
        {
            var scanner = new Scanner();
            scanner.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                scanner.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                scanner.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                scanner.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Scanner_ScanCounter()
        {
            var scanner = new Scanner();
            scanner.PowerOn();

            IDocument doc1;
            scanner.Scan(out doc1);
            IDocument doc2;
            scanner.Scan(out doc2);

            scanner.PowerOff();
            scanner.Scan(out doc1);
            scanner.PowerOn();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(2, scanner.Counter);
        }

    }
}
