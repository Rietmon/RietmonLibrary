﻿#if ENABLE_DAMN_SCRIPT
namespace Rietmon.DamnScript.Data
{
    public class ScriptRegionData
    {
        public int CodesCount => codesData.Length;
        
        public readonly string name;

        internal readonly ScriptCodeData[] codesData;

        public ScriptRegionData(string name, ScriptCodeData[] codesData)
        {
            this.name = name;
            this.codesData = codesData;
        }

        public ScriptCodeData GetCodeData(int index) => 
            index >= codesData.Length ? null : codesData[index];
    }
}
#endif