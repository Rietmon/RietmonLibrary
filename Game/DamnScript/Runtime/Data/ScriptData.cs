﻿#if ENABLE_DAMN_SCRIPT
using Rietmon.Extensions;

namespace Rietmon.DamnScript.Data
{
    public class ScriptData
    {
        public readonly string name;

        internal readonly ScriptRegionData[] regionsData;

        public ScriptData(string name, ScriptRegionData[] regionsData)
        {
            this.name = name;
            this.regionsData = regionsData;
        }

        public ScriptRegionData GetRegionData(string regionName) => 
            regionsData.Find((data) => data.name == regionName);
    }
}
#endif