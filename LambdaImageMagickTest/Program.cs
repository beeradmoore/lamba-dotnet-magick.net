using System;
using Amazon.Lambda.Core;
using ImageMagick;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

//LambdaImageMagickTest::LambdaImageMagickTest.Program::MyHandler
namespace LambdaImageMagickTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private string LDDFile(string file)
        {
            var process = new  System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "ldd",
                    Arguments = file,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return file +": \n" + result + "\n\n\n";
        }

        public string MyHandler(ILambdaContext context)
        {
            var logger = context?.Logger;
            logger?.Log("MyHandler started\n");


            string output = String.Empty;

            output += LDDFile("lib/libjpeg.so.8");
            output += LDDFile("lib/libMagickCore-7.Q8.so.3");
            output += LDDFile("lib/libMagickWand-7.Q8.so.3");
            output += LDDFile("runtimes/linux-x64/native/Magick.NET-Q8-x64.Native.dll.so");

            //var magickImage = new MagickImage(MagickColors.Red, 100, 100);
            //logger?.Log($"Image size: {magickImage.Width}x{magickImage.Height}");

            logger?.Log("MyHandler ended\n");

            return output;
        }
    }

}
