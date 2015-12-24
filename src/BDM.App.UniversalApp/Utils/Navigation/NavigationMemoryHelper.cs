using System;
using System.Collections.Generic;

namespace BDM.App.UniversalApp.Utils.Navigation
{
    /// <summary>
    /// Utilitaire qui stocke en RAM les objets de navigation.
    /// Le but est de pallier au problème de sérialisation lors du suspend de l'application.
    /// </summary>
    public static class NavigationMemoryHelper
    {
        private static Dictionary<Guid, GlobalNavigationArgs> _stack;

        static NavigationMemoryHelper()
        {
            _stack = new Dictionary<Guid, GlobalNavigationArgs>();
        }

        public static Guid AddInStack(GlobalNavigationArgs arg)
        {
            var guid = Guid.NewGuid();
            _stack.Add(guid, arg);
            return guid;
        }

        public static bool Clear(Guid guid)
        {
            return _stack.Remove(guid); ;
        }

        public static void ClearAll(Guid? guid = null)
        {
            GlobalNavigationArgs tempParam = null;
            if (guid.HasValue)
            {
                tempParam = Get(guid.Value);
            }
            _stack.Clear();
            if (tempParam != null)
                _stack.Add(guid.Value, tempParam);
        }

        public static GlobalNavigationArgs Get(Guid guid)
        {
            GlobalNavigationArgs res;
            return _stack.TryGetValue(guid, out res) ? res : null;
        }

        public static string SerializeStack()
        {
            var tempStack = new Dictionary<Guid, GlobalNavigationArgs>();
            foreach (var item in _stack)
            {
                var tempArg = new GlobalNavigationArgs()
                {
                    DataType = item.Value.DataType,
                    SerializedData = item.Value.Data != null ? item.Value.Data.GenericSerialize(item.Value.DataType) : null
                };
                tempStack.Add(item.Key, tempArg);
            }
            return tempStack.GenericSerialize(tempStack.GetType());
        }

        public static void LoadInStack(string content)
        {
            try
            {
                _stack = content.GenericDeserialize(_stack.GetType()) as Dictionary<Guid, GlobalNavigationArgs>;
                foreach (var item in _stack)
                {
                    if (item.Value.DataType != null && !string.IsNullOrEmpty(item.Value.SerializedData))
                    {
                        item.Value.Data = item.Value.SerializedData.GenericDeserialize(item.Value.DataType);
                        item.Value.SerializedData = null;
                    }
                }
            }
            catch { }
        }
    }
}
