using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace BitGo
{
    public static partial class Extensions
    {
        internal static string ConvertValueToString(this object value)
        {
            if (value is bool || value is bool?)
            {
                return ((bool)value) ? "true" : "false";
            }
            if (value is string)
            {
                return (string)value;
            }
            return value?.ToString();
        }

        internal static string SecureStringToString(this SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        internal static SecureString StringToSecureString(this string str)
        {
            var secureStr = new SecureString();
            if (str.Length > 0)
            {
                foreach (var c in str.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

    }

}
