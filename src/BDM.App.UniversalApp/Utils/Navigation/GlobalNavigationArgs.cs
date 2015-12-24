using System;

namespace BDM.App.UniversalApp.Utils.Navigation
{
    /// <summary>
    /// Classe qui encapsule le parametre de navigation afin de résoudre le probleme de la serialisation des données lors de la suspension de l'application.
    /// </summary>
    public class GlobalNavigationArgs
    {
        /// <summary>
        /// Le type du parametre de navigation
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// Le vrai parametre de navigation
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// La version serialisee du parametre de navigation
        /// </summary>
        public string SerializedData { get; set; }

        public override string ToString()
        {
            var returnObject = new GlobalNavigationArgs()
            {
                DataType = this.DataType,
                SerializedData = this.Data != null ? this.Data.GenericSerialize(DataType) : null
            };

            return returnObject.GenericSerialize(typeof(GlobalNavigationArgs));
        }
    }
}
