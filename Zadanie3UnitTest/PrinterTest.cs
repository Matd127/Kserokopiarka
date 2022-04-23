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
    public class PrinterTest
    {
        [TestMethod]
        public void Printer_GetState_StateOff()
        {
            var printer = new Printer();
            printer.PowerOff();

            Assert.AreEqual(IDevice.State.off, printer.GetState());
        }

        [TestMethod]
        public void Printer_GetState_StateOn()
        {
            var printer = new Printer();
            printer.PowerOn();

            Assert.AreEqual(IDevice.State.on, printer.GetState());
        }

        [TestMethod]
        public void Printer_Print_DeviceOn()
        {
            var printer = new Printer();
            printer.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                printer.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Printer_Print_DeviceOff()
        {
            var printer = new Printer();
            printer.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                printer.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Printer_PrintCounter()
        {
            var printer = new Printer();
            printer.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            printer.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            printer.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            printer.Print(in doc3);

            printer.PowerOff();
            printer.Print(in doc3);

            printer.PowerOn();
            Assert.AreEqual(3, printer.Counter);
        }
    }
}
