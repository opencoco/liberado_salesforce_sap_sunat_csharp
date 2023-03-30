using EvoPdf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

namespace ACME.WorkerService.Helpers
{
    public class PDFService : IPDFService
    {
        private readonly IConfiguration _configuration;
        private readonly string _licenseKey;
        private readonly ILogger _logger;

        public PDFService(IConfiguration config,ILogger<PDFService> logger)
        {
            _configuration = config;
            _licenseKey = @_configuration["LoremIpsum"];
            _logger = logger;
        }

        public byte[] GenerarMergePdfStream(IList<Stream> files,byte[]? digitalsign,string clave)
        {
            EvoPdf.Document document = new EvoPdf.Document
            {
                LicenseKey = _licenseKey
            };
            document.AutoCloseAppendedDocs = true;

            foreach (var pdfStream in files)
            {
                try
                {
                    document.AppendDocument(new EvoPdf.Document(pdfStream));
                }
                catch (Exception ex1) { }
            }

            
            //signature
            if (digitalsign != null && !string.IsNullOrEmpty(clave))
            {

                PdfPage pdfPage = document.AddPage();
                //using streamreader for reading my htmltemplate
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                
                PhysicalFileProvider _fileProvider = new PhysicalFileProvider(directoryPath);
                IFileInfo fileInfo = _fileProvider.GetFileInfo("Templates/signature.html");
              
                string conte = null;

                if (System.IO.File.Exists(fileInfo.PhysicalPath))
                {
                    conte = System.IO.File.ReadAllText(fileInfo.PhysicalPath);//, Encoding.GetEncoding("iso-8859-1"));
                }

                HtmlToPdfElement htmlToPdfElement = new HtmlToPdfElement(conte, "");
                pdfPage.AddElement(htmlToPdfElement);

                HtmlElementMapping digitalSignatureMapping = htmlToPdfElement.HtmlElementsMappingOptions.HtmlElementsMappingResult.GetElementByMappingId("digital_signature_element");
                if (digitalSignatureMapping != null)
                {
                    PdfPage digitalSignaturePage = digitalSignatureMapping.PdfRectangles[0].PdfPage;
                    RectangleF digitalSignatureRectangle = digitalSignatureMapping.PdfRectangles[0].Rectangle;

                    DigitalCertificatesCollection certificates = DigitalCertificatesStore.GetCertificates(digitalsign, clave);
                    DigitalCertificate certificate = certificates[certificates.Count-1];

                    // Create the digital signature
                    DigitalSignatureElement signature = new DigitalSignatureElement(digitalSignatureRectangle, certificate);
                    signature.Reason = "Protegemos el documento de cambios no deseados";
                    signature.ContactInfo = "El correo de contacto es info@ACME.com.pe";
                    signature.Location = "Plataforma de Salud Ocupacional";
                    digitalSignaturePage.AddElement(signature);
                    
                }

            }

            byte[] outStream = document.Save();
            document.Close();

            return outStream;
        }

        //public byte[] MergeFiles(List<Stream> sourceFiles)
        // {
        //     iTextSharp.text.Document document = new iTextSharp.text.Document();
        //     using (MemoryStream ms = new MemoryStream())
        //     {
        //         PdfCopy copy = new PdfCopy(document, ms);
        //         document.Open();
        //         int documentPageCounter = 0;

        //         // Iterate through all pdf documents
        //         for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
        //         {
        //             // Create pdf reader
        //             var x = sourceFiles[fileCounter];
        //             x.Position = 0;
        //             PdfReader reader = new PdfReader(x);
        //             int numberOfPages = reader.NumberOfPages;

        //             // Iterate through all pages
        //             for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
        //             {
        //                 documentPageCounter++;
        //                 PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
        //                 PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

        //                 // Write header
        //                 ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
        //                     new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
        //                     importedPage.Width < importedPage.Height ? 0 : 1);

        //                 // Write footer
        //                 ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
        //                     new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
        //                     importedPage.Width < importedPage.Height ? 0 : 1);

        //                 pageStamp.AlterContents();

        //                 copy.AddPage(importedPage);
        //             }

        //             copy.FreeReader(reader);
        //             reader.Close();
        //         }

        //         document.Close();
        //         return ms.GetBuffer();
        //     }
        // }

        public byte[] GenerarPdfStream(string htmlContent)
        {
            string licenseKey = @_configuration["LoremIpsum"];
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
            htmlToPdfConverter.LicenseKey = licenseKey;
            htmlToPdfConverter.PdfDocumentOptions.LeftMargin = -20;
            htmlToPdfConverter.PdfDocumentOptions.RightMargin = -20;
            htmlToPdfConverter.PdfDocumentOptions.TopMargin = 10;
            htmlToPdfConverter.PdfDocumentOptions.BottomMargin = 10;
            //htmlToPdfConverter.PdfDocumentOptions.Width = 800;
            //htmlToPdfConverter.PdfDocumentOptions.FitWidth = true;
            //htmlToPdfConverter.HtmlViewerWidth = 2480;

            //HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter
            //{
            //    LicenseKey = licenseKey,
            //    //ConversionDelay = 2,
            //    //HtmlViewerWidth = 1024,
            //    PdfDocumentOptions.LeftMargin = 10,
            //    PdfDocumentOptions.RightMargin = 10
            //};

            byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(htmlContent, "");

            //string repositorioPdf = @_configuration["AppSettings:Repository"];
            //string fileName = string.Format("{0}.pdf", Guid.NewGuid());
            //string pdfFilePath = string.Format(@"{0}\{1}", repositorioPdf, fileName);
            //File.WriteAllBytes(pdfFilePath, outPdfBuffer);

            return outPdfBuffer;
        }

        public string Base64QR(string texto)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string result = qrCode.GetGraphic(20);

            return result;
        }


        

        public void GenerarMergePdfFile(string destinationFile, IList<string> files)
        {
            EvoPdf.Document document = new EvoPdf.Document
            {
                LicenseKey = _licenseKey
            };

            foreach (var file in files)
            {
                document.AppendDocument(new EvoPdf.Document(@file));
            }

            document.AutoCloseAppendedDocs = true;

            document.Save(@destinationFile);
            document.Close();
        }
    }
}