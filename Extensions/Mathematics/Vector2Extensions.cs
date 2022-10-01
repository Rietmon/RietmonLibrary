﻿#if UNITY_5_3_OR_NEWER 
using UnityEngine;

namespace DamnLibrary.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Abs(this Vector2 vector) => new(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        public static float Sum(this Vector2 vector) => vector.x + vector.y;
        public static Vector2 Add(this Vector2 vector, float x = 0, float y = 0) => vector + new Vector2(x, y);
        
        public static Vector2Int Abs(this Vector2Int vector) => new(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        public static int Sum(this Vector2Int vector) => vector.x + vector.y;
        public static Vector2Int Add(this Vector2Int vector, int x = 0, int y = 0) => vector + new Vector2Int(x, y);
    }
}
#endif