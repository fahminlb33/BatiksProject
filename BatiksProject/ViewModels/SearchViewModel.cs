using Microsoft.AspNetCore.Http;

namespace BatiksProject.ViewModels
{
    public class SearchViewModel
    {
        public IFormFile UploadedFile { get; set; }
        public string SearchText { get; set; }
    }
}
