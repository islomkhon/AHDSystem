using System.Web;

namespace AHD.Controllers
{
    public class NueRequestAttchment
    {
        public string requestId { get; set; }
        public string userId { get; set; }
        public HttpPostedFileBase requestAtchmentFile { get; set; }
    }
}