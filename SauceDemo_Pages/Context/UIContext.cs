using System;
using System.Collections.Generic;
using System.Text;

namespace SauceDemo_Core.Context
{
    public static class UIContext
    {
        public const string BaseUrl = "https://www.saucedemo.com/";
        public const string LockedOutErrorText = "Epic sadface: Sorry, this user has been locked out.";
        public static readonly KeyValuePair<string, string> LockedOut_User = new KeyValuePair<string, string>
        (
            "locked_out_user",
            "secret_sauce"
        );
        public static readonly KeyValuePair<string, string> User = new KeyValuePair<string, string>
        (
            "standard_user",
            "secret_sauce"
        );
    }
}
