using AmiloBot.WebCapture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiloBot
{
    class Program
    {
        static void Main(string[] args)
        {
            AmiloBot amilo = new AmiloBot();

            //HtmlCapture capture = new HtmlCapture(@"myimg.png");
            //capture.HtmlImageCapture += new HtmlCapture.HtmlCaptureEvent(capture_HtmlImageCapture);
            //capture.Create("http://www.highcharts.com/demo/combo-dual-axes");
            
        }

        private static void capture_HtmlImageCapture(object sender, Uri url)
        {
            //throw new NotImplementedException();
        }
    }
}
