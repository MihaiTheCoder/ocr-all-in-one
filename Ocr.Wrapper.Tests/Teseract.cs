﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ocr.Wrapper.TesseractOcr;
using System;
using System.Threading.Tasks;

namespace Ocr.Wrapper.Tests
{
    [TestClass]
    public class TeseractTest
    {
        [TestMethod]
        public void reads_abc2()
        {
            var abc = System.IO.Path.GetFullPath(@"data\abc.JPG");

            TesseractService ts = new TesseractService(new NoOpOcrCache());
            var task = ts.GetOcrResultWithoutCacheAsync(abc, "eng");
            var res = task.GetAwaiter().GetResult();
            Assert.IsNotNull(res);
        }
    }
}
