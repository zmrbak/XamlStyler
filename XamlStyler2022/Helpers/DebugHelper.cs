using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamlStyler2022.Helpers
{
    public class DebugHelper
    {
        public static void WriteDebug(string message = "",
                                      [CallerMemberName] string memberName = null,
                                      [CallerFilePath] string sourceFilePath = null,
                                      [CallerLineNumber] int sourceLineNumber = default(int))
        {
            Trace.WriteLine("DEBUG:\t" + DateTime.Now.ToString() + "," + memberName + "," + sourceFilePath + "(" + sourceLineNumber + "):" + message);
        }
    }
}
