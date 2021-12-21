using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class UploadFileViewModel
    {
        public IFormFile csvFile { get; set; }
    }
}
