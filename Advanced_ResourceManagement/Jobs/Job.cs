using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Jobs
{
    static class NativeJob
    {
        [DllImport("kernel32")]
        public static extern IntPtr CreateJobObject(IntPtr sa, string name);

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AssignProcessToJobObject(IntPtr hjob, IntPtr hprocess);

        [DllImport("kernel32")]
        public static extern bool CloseHandle(IntPtr h);

        [DllImport("kernel32")]
        public static extern bool TerminateJobObject(IntPtr hjob, uint code);
    }

    public class Job : IDisposable
    {
        private readonly IntPtr _hJob;
        private List<Process> _processes;
        private long _sizeInBytes = 1024L *1024 * 100;
        private bool _disposed = false;

        public Job(string name)
        {
            _hJob = NativeJob.CreateJobObject(IntPtr.Zero, name);

            if (_hJob == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            _processes = new List<Process>();
            GC.AddMemoryPressure(_sizeInBytes);
            Console.WriteLine("Job Was Created");
        }

        public Job()
            : this(null)
        {
        }

        protected void AddProcessToJob(IntPtr hProcess)
        {
            CheckIfDisposed();

            if (!NativeJob.AssignProcessToJobObject(_hJob, hProcess))
                throw new InvalidOperationException("Failed to add process to job");
        }

        private void CheckIfDisposed()
        {
            if(_disposed) throw new ObjectDisposedException("Job");
        }

        public void AddProcessToJob(int pid)
        {
            CheckIfDisposed();
            AddProcessToJob(Process.GetProcessById(pid));
        }

        public void AddProcessToJob(Process proc)
        {
            CheckIfDisposed();
            Debug.Assert(proc != null);
            AddProcessToJob(proc.Handle);
            _processes.Add(proc);
        }

        public void Kill()
        {
            NativeJob.TerminateJobObject(_hJob, 0);
        }

        ~Job()
        {
            Dispose(false);
            GC.RemoveMemoryPressure(_sizeInBytes);
            Console.WriteLine("Job Was Released!");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _processes.Clear();
            }

            NativeJob.CloseHandle(_hJob);
            _disposed = true;
        }
    }
}
