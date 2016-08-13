using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomAwaiter
{
    public static class AwaiterExtensions
    {
        public static TaskAwaiter GetAwaiter(this int milliseconds)
        {
            if (milliseconds < 1)
            {
                return Task.Delay(0).GetAwaiter();
            }

            return Task.Delay(milliseconds).GetAwaiter();
        }

        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            if (process.HasExited)
            {
                tcs.TrySetResult(process.ExitCode);
            }
            else
            {
                process.EnableRaisingEvents = true;
                process.Exited += (sender, args) => { tcs.TrySetResult(process.ExitCode); };
            }

            return tcs.Task.GetAwaiter();
        }
    }
}
