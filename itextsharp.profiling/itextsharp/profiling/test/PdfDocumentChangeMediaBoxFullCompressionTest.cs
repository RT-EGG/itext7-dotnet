﻿
using System;
using NUnit.Framework;

namespace itextsharp.profiling.itextsharp.profiling.test
{
    class PdfDocumentChangeMediaBoxFullCompressionTest : PdfDocumenTest
    {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext
            .TestDirectory + "/../../resources/itextsharp/profiling/PdfDocumentTest/";

        [Test]
        [Timeout(300000)]
        public void Test() {
            ChangeMediaBox(sourceFolder + "100000PagesDocumentWithFullCompression.pdf", true, 1.35f);
        }
    }
}
