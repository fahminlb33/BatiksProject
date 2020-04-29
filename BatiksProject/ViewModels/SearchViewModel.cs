using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BatiksProject.ViewModels
{
    public class SearchViewModel
    {
        public FormFile UploadedFile { get; set; }
        public string SearchText { get; set; }
    }
}
