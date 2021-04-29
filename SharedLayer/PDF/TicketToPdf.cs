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
                // stuff 
            
                Document pdfDoc = new Document();
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                string ordertemplate = "";

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
                        /*
                        table.AddCell("");
                        table.AddCell(o.EventLocation);
                        table.AddCell("");
                        var cell = new PdfPCell(new Phrase(o.TicketCategory));
                        cell.Rotation = -90;
                        table.AddCell(cell);

                        table.AddCell("");
                        table.AddCell($"{o.Price.ToString("N0")} Ft");
                        table.AddCell(o.OrderId);
                        table.AddCell("");
                        */
                        pdfDoc.Add(table);
                        /*
                        if (counter != 0 && counter % 6 == 0)
                        {
                            ordertemplate += @"<div style = 'page-break-after: always;' > </div>";
                        }
                        ordertemplate += $@"<table class='table m-4 table-borderless myborder' style='page-break-inside: avoid'>
                            <colgroup>
                                <col class='blck' style='width: 20%;'/>
                                <col class='wht' span='2' />
                                <col class='blck' style='width: 20%;' />
                            </colgroup>
                            <tbody>

                                <tr>
                                    <td class='blck'>Jegy</td>
                                    <td><h1 class='padleft'>{o.EventName}</h1></td>
                                    <td class='wht'>Jegy</td>
                                    <td class='blck'></td>
                                </tr>
                                <tr>
                                    <td class='myflex' style='color:white;text-align:center'>
                                        <h1>{o.EventStartDate.Day}</h1>
                                        <h6>{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(o.EventStartDate.Month)}</h6>
                                    </td>
                                    <td><h6 class='padleft'>{o.EventLocation}</h6></td>
                                    <td class='wht'>Jegy</td>
                                    <td class='myflex rotate' style='color:white;text-align:center'><h4>{o.TicketCategory}</h4></td>
                                </tr>
                                <tr>
                                    <td class='blck'>Jegy</td>
                                    <td><p class='padleft'>{o.Price.ToString("N0")} Ft</p></td>
                                    <td>{o.OrderId}</td>
                                    <td class='blck'></td>
                                </tr>

                            </tbody>
                        </table>";*/
                        counter++;
                    }
                }
            
                //var htmlbuilder = new StringBuilder();
                /*htmlbuilder.Append(@" <html lang='en' dir='ltr' style='height: 100% '>
                          <head>
                            <meta charset = 'utf-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1'>
                            <title> Ticket </title>
                            <link href = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css' rel = 'stylesheet' integrity = 'sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl' crossorigin = 'anonymous' >
                            <style>
                                .blck{
                                    background-color: black;
                                    color: black;
                                    }
                                .wht{
                                    background-color: white;
                                    color: white;
                                    }
                                .myborder{
                                    border: 1px solid black;
                                    }
                                .myborder td{
                                    border: none;
                                }
                                .myborder tr{
                                    border: none;
                                }
                                .myflex{
                                    display: flex;
                                        flex-direction: column;
                                        justify-content: center;
                                        align-items: center;
                                    }
                                .rotate{
                                        -webkit-transform: rotate(-90deg);
                                    }
                                .padleft{
                                        padding-left: 15px;
                                    }
                            </style>
                          </head >
                          <body>");
                *///htmlbuilder.Append(ordertemplate);
                //htmlbuilder.Append(@"</body> </html>");
                
                //HTMLWorker htmlWorker = new HTMLWorker(pdfDoc);
                //htmlWorker.Parse(new StringReader(htmlbuilder.ToString()));
                
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
