using System;
using System.Collections;
using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> other)
    {
        foreach (var otherPair in other)
        {
            dictionary.Add(otherPair.Key, otherPair.Value);
        }
    }

    public static void AddOrChange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = value;
        else
            dictionary.Add(key, value);
    }
    
    public static bool AddIfNotExist<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key)) 
            return false;
        
        dictionary.Add(key, value);
        return true;
    }
    
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.TryGetValue(key, out var result))
            return result;
        
        dictionary.Add(key, value);
        return value;
    }

    public static bool ChangeIfExist<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
            return false;

        dictionary[key] = value;
        return true;
    }

    public static Dictionary<TKey, TNewValue> SmartCast<TNewValue, TKey, TCurrentValue>(this Dictionary<TKey, TCurrentValue> dictionary,
        Func<TCurrentValue, TNewValue> castFunction)
    {
        var result = new Dictionary<TKey, TNewValue>();
        foreach (var pair in dictionary)
        {
            result.Add(pair.Key, castFunction.Invoke(pair.Value));
        }

        return result;
    }
}
