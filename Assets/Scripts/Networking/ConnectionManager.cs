using UnityEngine;
using SimpleTelegramGame.Networking.Encryption;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.IO;

namespace SimpleTelegramGame.Networking
{
    public class ConnectionManager : MonoBehaviour
    {
        [Tooltip("The path to the .env file")]
        [SerializeField]
        private string envFilePath = "D:/GCUnity/T/simple-telegram-game-backend/.env"; // .env 파일 경로

        private string serverURI;
        private string token;

        private void Start()
        {
            try
            {
                // .env 파일에서 환경 변수 로드
                EnvLoader.Load(envFilePath);  // .env 파일을 로드만 하고 반환값을 받지 않음

                // 환경 변수 사용
                serverURI = EnvLoader.Get("SERVER_URI") ?? "https://defaultserver.com";
                token = EnvLoader.Get("TOKEN") ?? "default_token";

                Debug.Log($"Server URI: {serverURI}");
                Debug.Log($"Token: {token}");
            }
            catch (FileNotFoundException ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}
