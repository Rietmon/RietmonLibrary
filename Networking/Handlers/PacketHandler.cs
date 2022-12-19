﻿#if ENABLE_SERIALIZATION && ENABLE_NETWORKING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DamnLibrary.Debugging;
using DamnLibrary.Extensions;
using DamnLibrary.Networking.Attributes;
using DamnLibrary.Serialization;

namespace DamnLibrary.Networking.Handlers
{
    public static class PacketHandler
    {
        private static Dictionary<IConvertible, MethodInfo> Handlers { get; } = new();

        static PacketHandler()
        {
            var handlerTypes =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                let attributes = type.GetCustomAttributes(typeof(PacketHandlerAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = type, Attributes = attributes.OfType<PacketHandlerAttribute>() };

            foreach (var handlerType in handlerTypes)
            {
                var packetType = handlerType.Attributes.First().Type;
                var methodInfo = handlerType.Type.GetMethodByName("HandlePacket");
            
                Handlers.Add(packetType, methodInfo);
            }
        }
    
#pragma warning disable CS8603
        public static ISerializable Handle(IConvertible type, SerializationStream deserializationStream)
        {
            if (!Handlers.TryGetValue(type, out var handler))
            {
                UniversalDebugger.LogError($"{nameof(PacketHandler)} ({nameof(Handle)}) Unable to find handler for type {type}!");
                return null;
            }

#pragma warning disable CS8600
            return (ISerializable)handler.Invoke(null, new object[] { deserializationStream });
#pragma warning restore CS8600
#pragma warning restore CS8603
        }
    }
}
#endif