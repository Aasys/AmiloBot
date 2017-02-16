using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temboo.Core;

namespace AmiloBot.temboo
{
    class TembooUtil
    {
        private static readonly string TEMBOO_USER_NAME = "as3828";
        private static readonly string TEMBOO_APP_NAME = "myFirstApp";
        private static readonly string TEMBOO_API_KEY = "kUUKLkhLe66eOAxLJqq8f3YhGSSxXfuU";

        private static TembooSession session = null;

        public static TembooSession ACTIVE_SESSION
        {
            get
            {
                if (session == null)
                    session = new TembooSession(TEMBOO_USER_NAME, TEMBOO_APP_NAME, TEMBOO_API_KEY);
                return session;
            }
            private set { }
        }       
    }
}
