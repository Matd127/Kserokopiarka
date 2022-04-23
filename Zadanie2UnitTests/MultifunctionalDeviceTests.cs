using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie2;

namespace Zadanie2UnitTests
{
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class MultifunctionalDeviceTests
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, MultifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, MultifunctionalDevice.GetState());
        }

        //PRINT
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        //SCAN
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var MultifunctionalDevice = new Copier();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        //SEND
        [TestMethod]
        public void MultifunctionalDevice_Send_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultifunctionalDevice.Send();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Send_Deviceoff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultifunctionalDevice.Send();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_SendCounter()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            MultifunctionalDevice.Send();
            MultifunctionalDevice.Send();
            MultifunctionalDevice.Send();

            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.Send();
            MultifunctionalDevice.Send();
            MultifunctionalDevice.Send();
            MultifunctionalDevice.PowerOn();
            MultifunctionalDevice.Send();

            Assert.AreEqual(4, MultifunctionalDevice.SendCounter);
        }

        //RECEIVE
        [TestMethod]
        public void MultifunctionalDevice_Receive_DeviceOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.png");
                MultifunctionalDevice.Recieve(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Receive"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Receive_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.png");
                MultifunctionalDevice.Recieve(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Receive"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ReceiveCounter()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            IDocument doc1 = new ImageDocument("aaa.pdf");
            MultifunctionalDevice.Recieve(doc1);
            IDocument doc2 = new ImageDocument("bbb.pdf");
            MultifunctionalDevice.Recieve(doc2);
            IDocument doc3 = new ImageDocument("ccc.pdf");
            MultifunctionalDevice.Recieve(doc3);

            MultifunctionalDevice.PowerOff();
            IDocument doc4 = new ImageDocument("ddd.pdf");
            MultifunctionalDevice.Recieve(doc4);
            MultifunctionalDevice.PowerOn();

            Assert.AreEqual(3, MultifunctionalDevice.ReceiveCounter);
        }

    }
}
