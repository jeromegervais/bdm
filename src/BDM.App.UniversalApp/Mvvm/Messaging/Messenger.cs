using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Mvvm.Messaging
{
    public class Messenger : IMessenger
    {
        private static readonly Lazy<IMessenger> LazyInstance = new Lazy<IMessenger>(() => new Messenger(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static IMessenger Current { get { return LazyInstance.Value; } }

        private readonly Dictionary<Type, Dictionary<SubscriptionToken, object>> _actions;

        private Messenger()
        {
            _actions = new Dictionary<Type, Dictionary<SubscriptionToken, object>>();
        }

        public SubscriptionToken Subscribe<TMessage>(Action<TMessage> handler)
        {
            var type = typeof(TMessage);

            Dictionary<SubscriptionToken, object> actionsForType;
            if (!_actions.TryGetValue(type, out actionsForType))
                actionsForType = _actions[type] = new Dictionary<SubscriptionToken, object>();

            var token = new SubscriptionToken(type);
            actionsForType.Add(token, handler);

            return token;
        }

        public void Send<TMessage>(TMessage message)
        {
            Dictionary<SubscriptionToken, object> actionsForType;
            if (_actions.TryGetValue(typeof(TMessage), out actionsForType))
            {
                foreach (var pair in actionsForType.ToList())
                {
                    var handler = (Action<TMessage>)pair.Value;
                    handler(message);
                }
            }
        }

        public void Unsubscribe(SubscriptionToken token)
        {
            Dictionary<SubscriptionToken, object> actionsForType;
            if (_actions.TryGetValue(token.Type, out actionsForType))
            {
                actionsForType.Remove(token);
            }
        }
        
    }
}
