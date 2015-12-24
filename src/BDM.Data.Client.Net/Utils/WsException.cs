using System;

namespace BDM.Data.Client.Net.Utils
{
	public class WsException : Exception
	{
		public Guid Id { get; private set; }
		public HttpReturnCode Code { get; private set; }
		public WsErrors Error { get; private set; }

		public WsException(Guid id, HttpReturnCode code, string message)
			: base(message)
		{
			Id = id;
			Code = code;
		}

		public WsException(Guid id, HttpReturnCode code, string message, WsErrors error)
			: base(message)
		{
			Id = id;
			Code = code;
			Error = error;
		}
	}

	public enum WsErrors
	{
		TechnicalError, ////   Erreur technique
		AuthenticationError, ////   Erreur d’authentification
		UpdatingClosedError, ////Erreur, service d’actualisation fermé
		UpdatingAlreadyDoneError, ////Erreur, actualisation déjà effectuée
		ParamsError, //// Erreur de paramètres (RechercheOffre)
	}

	public enum HttpReturnCode
	{
		Code200 = 200, //// Le serveur a pu traiter la requête avec succès. Attention, il est toujours possible d’avoir une erreur fonctionnelle dans les données de la réponse.
		Code400 = 400, //// Les données de la requête ne sont pas valides (paramètre obligatoire absent, format d’un paramètre non valide…)
		Code401 = 401, //// Unauthorized - Mauvais environnement(utilisation d’une appli preprod sur prod ou inversement)
		Code403 = 403, //// AuthToken non reconnu côté WebService (cas d’une application native périmée)
		Code404 = 404, //// Mauvaise URI
		Code405 = 405, //// Mauvaise méthode HTTPS (ex : GET au lieu de POST)
		Code406 = 406, //// Header « Application » et/ou « Environment » absent(s)
		Code500 = 500, //// Internal Server Error - Une erreur technique s’est produite au sein du serveur et ne permet pas de traiter la requête.
		Code503 = 503, //// Service Unavailable - Saturation du service
	}
}
