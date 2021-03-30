using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using System.Diagnostics;
using System.IO;
using System.Globalization;

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

        public byte[] CreatePdf()
        {
            var Renderer = new IronPdf.HtmlToPdf();
            //Set the width of the resposive virtual browser window in pixels
            Renderer.PrintOptions.ViewPortWidth = 1280;
            Renderer.PrintOptions.FitToPaperWidth = true;
            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            Renderer.PrintOptions.InputEncoding = Encoding.UTF8;
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.PrintHtmlBackgrounds = true;
            Renderer.PrintOptions.MarginLeft = 0;
            Renderer.PrintOptions.MarginRight = 0;
            Renderer.PrintOptions.MarginTop = 0;
            Renderer.PrintOptions.MarginBottom = 0;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            Renderer.PrintOptions.RenderDelay = 500; //milliseconds
            Renderer.PrintOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;

            string ordertemplate = "";

            int counter = 0;
            foreach(var o in OrderItems)
            {
                for(int i = 0; i<o.Amount; i++)
                {
                    if(counter != 0 && counter % 6 == 0)
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
                    </table>";
                    counter++;
                }
            }
            
            var htmlbuilder = new StringBuilder();
            htmlbuilder.Append(@" <html lang='en' dir='ltr' style='height: 100% '>
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
            htmlbuilder.Append(ordertemplate);
            htmlbuilder.Append(@"</body> </html>");

            var PDF = Renderer.RenderHtmlAsPdf(
                    htmlbuilder.ToString()
                );
            //var PDF = await Renderer.RenderHTMLFileAsPdfAsync("hello.html");
            var order = OrderItems.FirstOrDefault();
            string OutputPath = $"Jegyek.pdf";
            if (order != null)
            {
                OutputPath = $"Jegyek_{order.OrderId}.pdf";
            }
            else OutputPath = $"Jegyek.pdf";
            var savedpdf = PDF.SaveAs(OutputPath);
            Filename = OutputPath;
            return savedpdf.BinaryData;
            // This neat trick opens our PDF file so we can see the result in our default PDF viewer
            //System.Diagnostics.Process.Start(OutputPath);
        }

        public void DeletePdf(string filename)
        {
            System.IO.File.Delete(filename);
        }
    }
}
