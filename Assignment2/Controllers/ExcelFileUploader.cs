using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage;

namespace Assignment2.Controllers
{
    public class ExcelFileUploaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ExcelFileUploader")]
        public async Task<IActionResult> Index(IFormFile files)
        {
            string? message = null;
            if (files == null || files.Length == 0)
            {
                message = "Please upload a valid Excel file";
            }

            else
            {
                string connectionstring = "DefaultEndpointsProtocol=https;AccountName=excelblobupload;AccountKey=tqpwKw+Nv+/fq/p+yHb+xUC6I5Qrhdxgz6cX6Z6ZnPpW7CgW9esvYriXmbWLBDXZ4VcsTuFnMq1m+AStLn7k4A==;EndpointSuffix=core.windows.net";
                string blobContainerName = "excelblob";
                BlobClient blobClient = new BlobClient(connectionString: connectionstring, blobContainerName: blobContainerName, blobName: files.FileName);
                try
                {
                    var result = await blobClient.UploadAsync(files.OpenReadStream());
                    message = files.FileName + " uploaded successfully to blob";
                }
                catch (Exception)
                {
                    message = "An Error occored, please try again later!";
                }
            }
            return Ok(message);

        }
    }
}