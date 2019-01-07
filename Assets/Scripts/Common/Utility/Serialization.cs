using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Common.Utility
{
    /// <summary>
    /// <brief> List<T></brief>
    ///
    /// <remarks> Kazuyuki, 2019/01/06.</remarks>
    ///
    /// <typeparam name="T"> Generic type parameter.</typeparam>
    /// </summary>

    [Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }

    /// <summary>
    /// <brief> A serialization.</brief>
    ///
    /// <remarks> Kazuyuki, 2019/01/06.</remarks>
    ///
    /// <typeparam name="TKey">   Type of the key.</typeparam>
    /// <typeparam name="TValue"> Type of the value.</typeparam>
    /// </summary>

    [System.Serializable]
    class Serialization<TKey, TValue> :ISerializationCallbackReceiver
    {
        /// <brief> The keys.</brief>
        [SerializeField]
        List<TKey> keys;
        /// <brief> The values.</brief>
        [SerializeField]
        List<TValue> values;

        Dictionary<TKey, TValue> target;

        /// <summary>
        /// <brief> Gets a dictionary of toes.</brief>
        ///
        /// <value> A dictionary of toes.</value>
        /// </summary>

        public Dictionary<TKey, TValue> ToDictionary()
        {
            return target;
        }

        /// <summary>
        /// <brief> Constructor.</brief>
        ///
        /// <remarks> Kazuyuki, 2019/01/06.</remarks>
        ///
        /// <param name="target"> Target for the.</param>
        /// </summary>

        public Serialization(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }
        

        /// <summary>
        /// <brief> <para>Implement this method to receive a callback before Unity serializes your
        ///     object.</para></brief>
        ///
        /// <remarks> Kazuyuki, 2019/01/06.</remarks>
        /// </summary>

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        /// <summary>
        /// <brief> <para>Implement this method to receive a callback after Unity deserializes your
        ///     object.</para></brief>
        ///
        /// <remarks> Kazuyuki, 2019/01/06.</remarks>
        /// </summary>

        public void OnAfterDeserialize()
        {
            var count = Math.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (var i = 0; i < count; ++i)
            {
                target.Add(keys[i], values[i]);
            }
        }

    }
}
