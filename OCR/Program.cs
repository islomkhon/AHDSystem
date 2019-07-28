using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace OCR
{
    class Program
    {
        static void Main(string[] args)
        {
            string imagePath = @"C:\Users\Monin.Jose\Pictures\imgpsh_mobile_save.jpg";//AppDomain.CurrentDomain.BaseDirectory + @"\images\ocrtest.jpg";
            string tessDataFolder = @"C:\Program Files\Tesseract-OCR\tessdata";
            string result = "";
            using (var engine = new TesseractEngine(tessDataFolder, "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }
        }
    }
}
