using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Mvvm.Messaging
{
    public struct SubscriptionToken : IEquatable<SubscriptionToken>
    {
        private readonly Type _type;
        private readonly Guid _id;

        /// <summary>
        /// Le type auquel on est abonné.
        /// </summary>
        public Type Type { get { return _type; } }

        public SubscriptionToken(Type type)
        {
            _type = type;
            _id = Guid.NewGuid();
        }

        public bool Equals(SubscriptionToken other)
        {
            return _id == other._id && _type == other._type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SubscriptionToken && Equals((SubscriptionToken)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_type.GetHashCode() * 397) ^ _id.GetHashCode();
            }
        }
    }
}
