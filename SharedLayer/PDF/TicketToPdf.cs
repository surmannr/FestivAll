using iTextSharp.text;
using iTextSharp.text.pdf;
using SharedLayer.DTOs;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using iTextSharp.text.html.simpleparser;
using Microsoft.JSInterop;
using System;

namespace SharedLayer.PDF
{
    public class TicketToPdf
    {
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public string Filename { get; set; }
        public TicketToPdf(List<OrderItemDto> orderItems)
        {
            OrderItems = orderItems;
        }
        
        public void CreatePdf(IJSRuntime js)
        {
            var order = OrderItems.FirstOrDefault();
            string OutputPath = $"Jegyek.pdf";
            if (order != null)
            {
                OutputPath = $"Jegyek_{order.OrderId}.pdf";
            }
            else OutputPath = $"Jegyek.pdf";

            Filename = OutputPath;
            using (MemoryStream ms = new MemoryStream())
            {
            
                Document pdfDoc = new Document();
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                float minheight = 40f;

                int counter = 0;
                foreach(var o in OrderItems)
                {
                    for(int i = 0; i<o.Amount; i++)
                    {
                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100f;
                        table.SpacingAfter = 12.5f;

                        var MyFont = FontFactory.GetFont("Times New Roman", 11, BaseColor.WHITE);
                        var celldate = new PdfPCell(new Phrase($"{o.EventStartDate.Day}\n{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(o.EventStartDate.Month)}", MyFont));
                        celldate.Rowspan = 3;
                        celldate.BackgroundColor = BaseColor.BLACK;
                        celldate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        celldate.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        table.AddCell(celldate);

                        var MyFont4 = FontFactory.GetFont("Times New Roman", 16, BaseColor.BLACK);
                        var cellnama = new PdfPCell(new Phrase(o.EventName,MyFont4));
                        cellnama.Colspan = 2;
                        cellnama.Border = PdfPCell.TOP_BORDER;
                        table.AddCell(cellnama);

                        var MyFont2 = FontFactory.GetFont("Times New Roman", 16, BaseColor.WHITE);
                        var cellcategory = new PdfPCell(new Phrase(o.TicketCategory, MyFont2));
                        cellcategory.Rotation = 90;
                        cellcategory.BackgroundColor = BaseColor.BLACK;
                        cellcategory.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cellcategory.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        cellcategory.Rowspan = 3;
                        table.AddCell(cellcategory);

                        var celllocation = new PdfPCell(new Phrase(o.EventLocation));
                        celllocation.Colspan = 2;
                        celllocation.Border = PdfPCell.NO_BORDER;
                        celllocation.BorderColor = BaseColor.WHITE;
                        celllocation.MinimumHeight = minheight;
                        table.AddCell(celllocation);

                        var MyFont3 = FontFactory.GetFont("Times New Roman", 8, BaseColor.BLACK);
                        var cellpriceandid = new PdfPCell(new Phrase($"{o.Price.ToString("N0")} Ft\n{o.OrderId}",MyFont3));
                        cellpriceandid.Colspan = 2;
                        cellpriceandid.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        cellpriceandid.Border = PdfPCell.BOTTOM_BORDER;
                        cellpriceandid.MinimumHeight = minheight;
                        table.AddCell(cellpriceandid);
                       
                        pdfDoc.Add(table);
                      
                        counter++;
                    }
                }
            
                
                pdfWriter.CloseStream = false;
                pdfDoc.Close();

                Generate(js,Filename,ms.GetBuffer());
            }
        }
        
        public void Generate(IJSRuntime js, string filename, byte[] bytes)
        {
            js.InvokeVoidAsync("jsSaveAsFile",
                                filename,
                                Convert.ToBase64String(bytes)
                                );
        }

        public void DeletePdf(string filename)
        {
            System.IO.File.Delete(filename);
        }
    }
}
