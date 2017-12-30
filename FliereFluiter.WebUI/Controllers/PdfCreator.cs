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
        Document oDoc = new Document();

       /* protected void GeneratePDF()
        {
            iTextSharp.text.Document oDoc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(oDoc, new FileStream("HelloWorld.pdf", FileMode.Create));
            oDoc.Open();
            oDoc.Add(new Paragraph("Hello World!"));
            oDoc.Close();
        }*/

        protected void OpenConnection(int Id)
        {
            string fileName = "S:/download/CreatedPDF/" + Id + ".pdf";
            PdfWriter.GetInstance(oDoc, new FileStream(fileName, FileMode.OpenOrCreate));
            oDoc.Open();
        }

        private void CloseConnection()
        {
            oDoc.Close();
        }

        public void AddLineToPDF(string text, int Id)
        {
            OpenConnection(Id);

            oDoc.Add(new Paragraph(text));

            CloseConnection();
        }
    }

}