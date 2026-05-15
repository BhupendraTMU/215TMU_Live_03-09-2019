using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO.Compression;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Kernel.Pdf.Canvas.Parser;

public partial class SplitPDF : System.Web.UI.Page
{
    protected void btnSplit_Click(object sender, EventArgs e)
    {
        string sourcePath = Server.MapPath("~/Uploads/master.pdf");
        string outputFolder = Server.MapPath("~/SplitPDF/");

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Clear old files
        foreach (var file in Directory.GetFiles(outputFolder))
            File.Delete(file);

        SplitPdfPANWise(sourcePath, outputFolder);

        Response.Write("PDF Split & ZIP Created Successfully!");
    }

    public void SplitPdfPANWise(string sourcePdfPath, string outputFolder)
    {
        Regex panRegex = new Regex(@"\b[A-Z]{5}[0-9]{4}[A-Z]{1}\b");

        using (PdfReader reader = new PdfReader(sourcePdfPath))
        using (PdfDocument pdfDoc = new PdfDocument(reader))
        {
            int totalPages = pdfDoc.GetNumberOfPages();

            string currentPAN = "";
            List<int> currentPages = new List<int>();

            for (int i = 1; i <= totalPages; i++)
            {
                string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));

                // Extract all PANs from page
                MatchCollection matches = panRegex.Matches(pageText);

                string employeePAN = "";

                // In Form 16, second PAN is employee PAN
                if (matches.Count >= 2)
                {
                    try
                    {
                        employeePAN = matches[2].Value;
                    }
                    catch
                    {
                        employeePAN = matches[1].Value;
                    }
                    
                }

                // If first employee
                if (string.IsNullOrEmpty(currentPAN) && !string.IsNullOrEmpty(employeePAN))
                {
                    currentPAN = employeePAN;
                }
                // If new employee detected
                else if (!string.IsNullOrEmpty(employeePAN) && employeePAN != currentPAN)
                {
                    SaveEmployeePdf(pdfDoc, outputFolder, currentPAN, currentPages);

                    currentPages = new List<int>();
                    currentPAN = employeePAN;
                }

                currentPages.Add(i);
            }

            // Save last employee
            if (currentPages.Count > 0 && !string.IsNullOrEmpty(currentPAN))
            {
                SaveEmployeePdf(pdfDoc, outputFolder, currentPAN, currentPages);
            }
        }

        //CreateZip(outputFolder);
    }

    private void SaveEmployeePdf(PdfDocument sourcePdf,
                                 string outputFolder,
                                 string pan,
                                 List<int> pages)
    {
        string filePath = Path.Combine(outputFolder, pan + ".pdf");

        using (PdfWriter writer = new PdfWriter(filePath))
        using (PdfDocument newPdf = new PdfDocument(writer))
        {
            PdfMerger merger = new PdfMerger(newPdf);

            foreach (int pageNumber in pages)
            {
                merger.Merge(sourcePdf, pageNumber, pageNumber);
            }
        }
    }

    //public void CreateZip(string folderPath)
    //{
    //    string zipPath = Server.MapPath("~/App_Data/PAN_PDFs.zip");

    //    if (File.Exists(zipPath))
    //        File.Delete(zipPath);

    //    ZipFile.CreateFromDirectory(folderPath, zipPath);
    //}
}
