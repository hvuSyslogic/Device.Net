﻿using System.Threading.Tasks;
using Device.Net.LibUsb;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using usbnet = Usb.Net;

namespace Device.Net.IntegrationTests
{
    [TestClass]
    public class STM32BootloaderTests
    {
        [TestMethod]
        public async Task Test()
        {
            var usbDeviceFinder = new UsbDeviceFinder(0x0483, 0xDF11);
            var usbDevice = UsbDevice.OpenUsbDevice(usbDeviceFinder);

            Assert.IsNotNull(usbDevice);

            var logger = new Mock<ILogger>();
            var tracer = new Mock<ITracer>();

            var libUsbInterfaceManager = new LibUsbInterfaceManager(usbDevice, 3000, logger.Object, tracer.Object, null, null);

            var usbNetUsbDevice = new usbnet.UsbDevice("", libUsbInterfaceManager, logger.Object, tracer.Object);

            await usbNetUsbDevice.InitializeAsync();

            var bytes = new byte[64];
            var asdasd = await usbNetUsbDevice.WriteAndReadAsync(bytes);
        }
    }
}
