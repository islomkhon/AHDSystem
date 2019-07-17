using Freezer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScreen
{
    class Program
    {
        static void Main(string[] args)
        {
            var screenshotJob = ScreenshotJobBuilder.Create("https://github.com")
              .SetBrowserSize(1366, 768)
              .SetCaptureZone(CaptureZone.FullPage) // Set what should be captured
              .SetTrigger(new WindowLoadTrigger()); // Set when the picture is taken

            System.IO.File.WriteAllBytes("this_is_not_even_my_final_screenshot.png", screenshotJob.Freeze());
        }
    }
}
