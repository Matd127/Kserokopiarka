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
    public class MultidimensionalDeviceTest
    {
        [TestMethod]
        public void MultidimensionalDevice_Send_DeviceOn()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultidimensionalDevice.Send();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Send_Deviceoff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultidimensionalDevice.Send();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_SendCounter()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();

            MultidimensionalDevice.Send();
            MultidimensionalDevice.Send();
            MultidimensionalDevice.Send();

            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.Send();
            MultidimensionalDevice.Send();
            MultidimensionalDevice.Send();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.Send();

            Assert.AreEqual(4, MultidimensionalDevice.SendCounter);
        }

        //RECEIVE
        [TestMethod]
        public void MultidimensionalDevice_Receive_DeviceOff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.png");
                MultidimensionalDevice.Receive(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Receive"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_Receive_DeviceOn()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.png");
                MultidimensionalDevice.Receive(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Receive"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_ReceiveCounter()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();

            IDocument doc1 = new ImageDocument("aaa.pdf");
            MultidimensionalDevice.Receive(doc1);
            IDocument doc2 = new ImageDocument("bbb.pdf");
            MultidimensionalDevice.Receive(doc2);
            IDocument doc3 = new ImageDocument("ccc.pdf");
            MultidimensionalDevice.Receive(doc3);

            MultidimensionalDevice.PowerOff();
            IDocument doc4 = new ImageDocument("ddd.pdf");
            MultidimensionalDevice.Receive(doc4);
            MultidimensionalDevice.PowerOn();

            Assert.AreEqual(3, MultidimensionalDevice.ReceiveCounter);
        }
    }
}
