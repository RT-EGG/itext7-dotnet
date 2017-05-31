/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Kernel.Pdf {
    public class ParentTreeTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/kernel/pdf/ParentTreeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/kernel/pdf/ParentTreeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1253")]
        public virtual void Test01() {
            String outFile = destinationFolder + "parentTreeTest01.pdf";
            String cmpFile = sourceFolder + "cmp_parentTreeTest01.pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(outFile));
            document.SetTagged();
            PdfStructElem doc = document.GetStructTreeRoot().AddKid(new PdfStructElem(document, PdfName.Document));
            PdfPage firstPage = document.AddNewPage();
            PdfCanvas canvas = new PdfCanvas(firstPage);
            canvas.BeginText();
            canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.COURIER), 24);
            canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
            PdfStructElem paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
            PdfStructElem span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, firstPage));
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(firstPage, span1))));
            canvas.ShowText("Hello ");
            canvas.CloseTag();
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrDictionary(firstPage, span1))));
            canvas.ShowText("World");
            canvas.CloseTag();
            canvas.EndText();
            canvas.Release();
            PdfPage secondPage = document.AddNewPage();
            document.Close();
            NUnit.Framework.Assert.IsTrue(CheckParentTree(outFile, cmpFile));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1253")]
        public virtual void Test02() {
            String outFile = destinationFolder + "parentTreeTest02.pdf";
            String cmpFile = sourceFolder + "cmp_parentTreeTest02.pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(outFile));
            document.SetTagged();
            PdfStructElem doc = document.GetStructTreeRoot().AddKid(new PdfStructElem(document, PdfName.Document));
            PdfPage firstPage = document.AddNewPage();
            PdfPage secondPage = document.AddNewPage();
            PdfCanvas canvas = new PdfCanvas(secondPage);
            canvas.BeginText();
            canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.COURIER), 24);
            canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
            PdfStructElem paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
            PdfStructElem span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, secondPage));
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(secondPage, span1))));
            canvas.ShowText("Hello ");
            canvas.CloseTag();
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrDictionary(secondPage, span1))));
            canvas.ShowText("World");
            canvas.CloseTag();
            canvas.EndText();
            canvas.Release();
            document.Close();
            NUnit.Framework.Assert.IsTrue(CheckParentTree(outFile, cmpFile));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1253")]
        public virtual void Test03() {
            String outFile = destinationFolder + "parentTreeTest03.pdf";
            String cmpFile = sourceFolder + "cmp_parentTreeTest03.pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(outFile));
            document.SetTagged();
            PdfStructElem doc = document.GetStructTreeRoot().AddKid(new PdfStructElem(document, PdfName.Document));
            PdfPage firstPage = document.AddNewPage();
            for (int i = 0; i < 51; i++) {
                PdfPage anotherPage = document.AddNewPage();
                PdfCanvas canvas = new PdfCanvas(anotherPage);
                canvas.BeginText();
                canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.COURIER), 24);
                canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
                PdfStructElem paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
                PdfStructElem span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, anotherPage));
                canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(anotherPage, span1))));
                canvas.ShowText("Hello ");
                canvas.CloseTag();
                canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrDictionary(anotherPage, span1))));
                canvas.ShowText("World");
                canvas.CloseTag();
                canvas.EndText();
                canvas.Release();
            }
            document.Close();
            NUnit.Framework.Assert.IsTrue(CheckParentTree(outFile, cmpFile));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1253")]
        public virtual void Test04() {
            String outFile = destinationFolder + "parentTreeTest04.pdf";
            String cmpFile = sourceFolder + "cmp_parentTreeTest04.pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(outFile));
            document.SetTagged();
            PdfStructElem doc = document.GetStructTreeRoot().AddKid(new PdfStructElem(document, PdfName.Document));
            for (int i = 0; i < 51; i++) {
                PdfPage anotherPage = document.AddNewPage();
                PdfCanvas canvas = new PdfCanvas(anotherPage);
                canvas.BeginText();
                canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.COURIER), 24);
                canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
                PdfStructElem paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
                PdfStructElem span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, anotherPage));
                canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(anotherPage, span1))));
                canvas.ShowText("Hello ");
                canvas.CloseTag();
                canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrDictionary(anotherPage, span1))));
                canvas.ShowText("World");
                canvas.CloseTag();
                canvas.EndText();
                canvas.Release();
            }
            document.Close();
            NUnit.Framework.Assert.IsTrue(CheckParentTree(outFile, cmpFile));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1253")]
        public virtual void Test05() {
            String outFile = destinationFolder + "parentTreeTest05.pdf";
            String cmpFile = sourceFolder + "cmp_parentTreeTest05.pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(outFile));
            document.SetTagged();
            PdfStructElem doc = document.GetStructTreeRoot().AddKid(new PdfStructElem(document, PdfName.Document));
            PdfPage page1 = document.AddNewPage();
            PdfCanvas canvas = new PdfCanvas(page1);
            canvas.BeginText();
            canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.COURIER), 24);
            canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
            PdfStructElem paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
            PdfStructElem span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, page1));
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(page1, span1))));
            canvas.ShowText("Hello ");
            canvas.CloseTag();
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrDictionary(page1, span1))));
            canvas.ShowText("World");
            canvas.CloseTag();
            canvas.EndText();
            canvas.Release();
            PdfPage page2 = document.AddNewPage();
            canvas = new PdfCanvas(page2);
            canvas.BeginText();
            canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.HELVETICA), 24);
            canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
            paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
            span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, page2));
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(page2, span1))));
            canvas.ShowText("Hello ");
            canvas.CloseTag();
            PdfStructElem span2 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, page2));
            canvas.OpenTag(new CanvasTag(span2.AddKid(new PdfMcrNumber(page2, span2))));
            canvas.ShowText("World");
            canvas.CloseTag();
            canvas.EndText();
            canvas.Release();
            document.Close();
            NUnit.Framework.Assert.IsTrue(CheckParentTree(outFile, cmpFile));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1253")]
        public virtual void Test06() {
            String outFile = destinationFolder + "parentTreeTest06.pdf";
            String cmpFile = sourceFolder + "cmp_parentTreeTest06.pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(outFile));
            document.SetTagged();
            PdfStructElem doc = document.GetStructTreeRoot().AddKid(new PdfStructElem(document, PdfName.Document));
            PdfPage firstPage = document.AddNewPage();
            PdfCanvas canvas = new PdfCanvas(firstPage);
            canvas.BeginText();
            canvas.SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.COURIER), 24);
            canvas.SetTextMatrix(1, 0, 0, 1, 32, 512);
            PdfStructElem paragraph = doc.AddKid(new PdfStructElem(document, PdfName.P));
            PdfStructElem span1 = paragraph.AddKid(new PdfStructElem(document, PdfName.Span, firstPage));
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrNumber(firstPage, span1))));
            canvas.ShowText("Hello ");
            canvas.CloseTag();
            canvas.OpenTag(new CanvasTag(span1.AddKid(new PdfMcrDictionary(firstPage, span1))));
            canvas.ShowText("World");
            canvas.CloseTag();
            canvas.EndText();
            canvas.Release();
            PdfPage secondPage = document.AddNewPage();
            PdfLinkAnnotation linkExplicitDest = new PdfLinkAnnotation(new Rectangle(35, 785, 160, 15));
            secondPage.AddAnnotation(linkExplicitDest);
            document.Close();
            NUnit.Framework.Assert.IsTrue(CheckParentTree(outFile, cmpFile));
        }

        /// <exception cref="System.IO.IOException"/>
        private bool CheckParentTree(String outFileName, String cmpFileName) {
            PdfReader outReader = new PdfReader(outFileName);
            PdfDocument outDocument = new PdfDocument(outReader);
            PdfReader cmpReader = new PdfReader(cmpFileName);
            PdfDocument cmpDocument = new PdfDocument(cmpReader);
            CompareTool.CompareResult result = new CompareTool().CompareByCatalog(outDocument, cmpDocument);
            if (!result.IsOk()) {
                System.Console.Out.WriteLine(result.GetReport());
            }
            return result.IsOk();
        }
    }
}