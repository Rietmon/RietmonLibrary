using System;
using DamnLibrary.Utilities;

#pragma warning disable 660,661

namespace DamnLibrary.Managements.Signals
{
    internal readonly struct SignalSystemId
    {
        private readonly IntPtr originalMethodPointer;

        private SignalSystemId(IntPtr originalMethodPointer)
        {
            this.originalMethodPointer = originalMethodPointer;
        }

        public static implicit operator SignalSystemId(IntPtr originalMethodPointer) =>
            new(originalMethodPointer);
        
        public static implicit operator SignalSystemId(RuntimeMethodHandle handler) =>
            new(handler.Value);

        public static bool operator ==(SignalSystemId left, SignalSystemId right)
        {
            var preCompare = CompareUtilities.PreCompare(left, right);
            if (preCompare != null)
                return preCompare.Value;

            return left.originalMethodPointer == right.originalMethodPointer;
        }

        public static bool operator !=(SignalSystemId left, SignalSystemId right)
        {
            return !(left == right);
        }
    }
}