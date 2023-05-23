using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared
{
    public static class ErrorCodeShared
    {
        public static string GetMostInnerExceptionMessage(this Exception ex)
        {
            return GetMostInnerException(ex).Message.ToUpper();
        }

        public static Exception GetMostInnerException(this Exception ex)
        {
            while (ex.InnerException != null) {  ex = ex.InnerException; }
            return ex;
        }

        public static bool ContainsString(this Exception ex, string value)
        {
            return ex.GetMostInnerException().Message.ToUpper().Contains(value.ToUpper());
        }

    }
}
