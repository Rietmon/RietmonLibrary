﻿#if ENABLE_DAMN_SCRIPT
using System.Collections.Generic;

namespace Rietmon.DamnScript
{
    internal static class NativeMethodsManager
    {
        private static readonly Dictionary<string, NativeMethod> methods =
            new Dictionary<string, NativeMethod>();

        public static void Add(string name, NativeMethod method) => methods.Add(name, method);

        public static NativeMethod Get(string name) => methods.TryGetValue(name, out var method) ? method : null;

        public static bool TryGet(string name, out NativeMethod method) => methods.TryGetValue(name, out method);
    }
}
#endif