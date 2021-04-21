﻿using UnityEngine;

namespace Rietmon.Extensions
{
    public static class ObjectExtensions
    {
        public static Transform GetTransform(this Object obj)
        {
            switch (obj)
            {
                case Component component:
                    return component.transform;
                case GameObject gameObject:
                    return gameObject.transform;
                default:
                    return null;
            }
        }
    }
}