using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLayer.PDF;

namespace BlazorPL.Server.Areas.Identity.Pages
{
    public class DownloadModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Orderid { get; set; }
        public string FileName { get; set; }

        public TicketToPdf ticketToPdf { get; set; }


        public IActionResult OnGet()
        {
            ticketToPdf = new TicketToPdf(null);

            FileName = "Jegyek_" + Orderid + ".pdf";

            //Build the File Path.
            string path = FileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            ticketToPdf.DeletePdf(FileName);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", FileName);
        }
    }
}
