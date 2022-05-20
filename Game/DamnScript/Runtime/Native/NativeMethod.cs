﻿#if ENABLE_DAMN_SCRIPT
using System;
using System.Threading.Tasks;
using DamnLibrary.DamnScript.Executing;

namespace DamnLibrary.DamnScript
{
    public class NativeMethod
    {
        private readonly Func<ScriptCode, string[], Task<bool>> asyncFunction;
        
        public NativeMethod(Func<ScriptCode, string[], Task<bool>> function) => asyncFunction = function;

        public bool Invoke(ScriptCode code, string[] arguments) => asyncFunction.Invoke(code, arguments).Result;
        
        public async Task<bool> InvokeAsync(ScriptCode code, string[] arguments) =>
            await asyncFunction.Invoke(code, arguments);

        public static implicit operator NativeMethod(Func<ScriptCode, string[], Task<bool>> function) => 
            new NativeMethod(function);
    } 
}
#endif