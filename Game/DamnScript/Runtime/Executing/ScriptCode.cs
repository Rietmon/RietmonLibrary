﻿#if ENABLE_DAMN_SCRIPT
using System.Threading.Tasks;
using Rietmon.DamnScript.Data;

namespace Rietmon.DamnScript.Executing
{
    public class ScriptCode
    {
        public Script Script => Parent.Parent;
        public ScriptRegion Parent { get; }

        private readonly ScriptCodeData codeData;

        public ScriptCode(ScriptRegion parent, ScriptCodeData codeData)
        {
            Parent = parent;
            this.codeData = codeData;
        }

        public async Task<bool> ExecuteAsync() => await ScriptEngine.InvokeAsync(this, codeData.codes);
    
        public bool Execute() => ScriptEngine.Invoke(this, codeData.codes);
    }
  
}
#endif