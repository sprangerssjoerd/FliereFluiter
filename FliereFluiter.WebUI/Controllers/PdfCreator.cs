using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FliereFluiter.WebUI.Controllers
{
    public class PdfCreator
    {
        protected void GeneratePDF()
        {
            iTextSharp.text.Document oDoc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(oDoc, new FileStream("HelloWorld.pdf", FileMode.Create));
            oDoc.Open();
            oDoc.Add(new Paragraph("Hello World!"));
            oDoc.Close();
        }

    }

}