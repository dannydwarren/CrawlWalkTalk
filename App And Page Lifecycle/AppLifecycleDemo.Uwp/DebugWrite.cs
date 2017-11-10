using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AppLifecycleDemo.Uwp
{
    public static class DebugWrite
    {
        public static void MethodName(string className, [CallerMemberName]string memberName = null)
        {
            Debug.WriteLine($"Class.Method: {className}.{memberName}");
        }
    }

}
