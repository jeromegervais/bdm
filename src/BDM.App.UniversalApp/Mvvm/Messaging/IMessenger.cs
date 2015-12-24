using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.App.UniversalApp.Mvvm.Messaging
{
    public interface IMessenger
    {
        /// <summary>
        /// Permet d'écouter à un certain type de message
        /// </summary>
        /// <typeparam name="TMessage">le type</typeparam>
        /// <param name="handler">l'action a effectuer à la réception du message</param>
        /// <returns>un token qui represente la souscription</returns>
        SubscriptionToken Subscribe<TMessage>(Action<TMessage> handler);

        /// <summary>
        /// Envoi d'un message
        /// </summary>
        /// <typeparam name="TMessage">le type du message envoyé</typeparam>
        /// <param name="message">le message envoyé</param>
        void Send<TMessage>(TMessage message);

        /// <summary>
        /// Permet d'arrêter une souscription préétablie
        /// </summary>
        /// <param name="token">le token renvoyé par la méthode Suscribe</param>
        void Unsubscribe(SubscriptionToken token);
    }
}
