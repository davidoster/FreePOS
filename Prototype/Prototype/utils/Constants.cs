using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public static class Constants
    {
        public static string RestURL = "http://mill.com.co/ws/freepos/process";

        #region Operations
        public static int SIGNUP = 100;
        public static int LOGIN = 101;
        public static int ADD_COMPANY = 200;
        public static int FETCH_COMPANY = 201;
        public static int ADD_STORE = 202;
        public static int FETCH_STORE = 203;
        public static int ASSOC_USER = 204;
        public static int ADD_INVENTORY = 300;
        public static int ADD_PRODUCT = 301;
        public static int REM_PRODUCT = 302;
        public static int SHOW_PRODUCTS = 303;
        public static int FETCH_INVENTORIES = 304;
        #endregion
    }
}
