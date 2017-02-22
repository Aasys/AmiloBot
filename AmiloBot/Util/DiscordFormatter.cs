using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiloBot.Util
{
    class DiscordFormatter
    {
        public static string Bold(String str) {
            return "**" + str + "**";
        }

        public static string Url(String url) {
            return "<" + url + ">";
        }
    }
}
