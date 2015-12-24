using Newtonsoft.Json;
using System;

namespace BDM.App.UniversalApp.Utils
{
    /// <summary>
    /// Utilitaire permettant la (de)serialisation
    /// </summary>
    public static class SerializationHelper
    {
        public static object GenericDeserialize(this string content, Type type)
        {
            return JsonConvert.DeserializeObject(content, type);
        }

        public static string GenericSerialize(this object obj, Type type)
        {
            return JsonConvert.SerializeObject(obj, type, null);
        }
    }
}
