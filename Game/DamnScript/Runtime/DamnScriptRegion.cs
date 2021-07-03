﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Rietmon.Serialization;

namespace Rietmon.DS
{
    public class DamnScriptRegion : ISerializable
    {
        public DamnScript Parent { get; }

        public bool IsOver => damnScriptCodes.Count <= CurrentMethod;

        public DamnScriptCode CurrentCode => IsOver ? null : damnScriptCodes[CurrentMethod];
    
        public int CurrentMethod { get; private set; }

        private readonly List<DamnScriptCode> damnScriptCodes = new List<DamnScriptCode>();

        private readonly Action onRegionEndCallback;
    
        private bool stopExecuting;

        public DamnScriptRegion(string code, Action onRegionEndCallback, DamnScript parent)
        {
            Parent = parent;
            this.onRegionEndCallback = onRegionEndCallback;

            var codes = DamnScriptParser.ParseCode(code);
            foreach (var codesArray in codes)
                damnScriptCodes.Add(new DamnScriptCode(codesArray, this));
        }

        public async void BeginAsync()
        {
            while (true)
            {
                if (IsOver || stopExecuting)
                {
                    await UniTask.Yield();
                    
                    stopExecuting = false;
                    onRegionEndCallback?.Invoke();
                    return;
                }

                if (await CurrentCode.ExecuteAsync()) 
                    CurrentMethod++;

                await UniTask.Yield();
            }
        }

        public void Stop()
        {
            stopExecuting = true;
        }

        public void Serialize(SerializationStream stream)
        {
            stream.Write(CurrentMethod);
        }

        public void Deserialize(SerializationStream stream)
        {
            CurrentMethod = stream.Read<int>();
        }
    }   
}