using SimpleTelegramGame.Networking.Encryption;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace SimpleTelegramGame.Networking
{
    public class ConnectionManager : MonoBehaviour
    {
        [Tooltip("The end point where the score will be read & handled on your app")]
        [SerializeField] 
        private string serverURI = "https://example.com/highscore/";

        private string playerId = "123123";
        private bool dontSend = false;
        
        void Start()
        {
#if UNITY_WEBGL
        // The telegram game is launched with the player id as parameter 
        playerId = URLParameters.GetSearchParameters()["id"];
        // Debug.Log("Got playerId: " + playerId);
#endif
        }
        
        public void SendScore(int score)
        {
            StartCoroutine(SendScoreCor(score));
        }
        
        IEnumerator SendScoreCor(int score)
        {
            if (dontSend || playerId == "") yield break;

            string uri = serverURI + score + "?id=" + playerId;
            
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();
                
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.LogError("Error sending score: " + webRequest.error);
                        break;
                    
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("Error sending score: " + webRequest.error);
                        break;
                    
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("Error sending score: " + webRequest.error);
                        break;
                    
                    case UnityWebRequest.Result.Success:
                        Debug.Log("Success sending score");
                        break;
                }
            }
        }
    }
}