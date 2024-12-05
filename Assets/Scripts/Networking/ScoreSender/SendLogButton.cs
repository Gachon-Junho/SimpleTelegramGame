using UnityEngine;
using UnityEngine.UI; // Button을 사용하기 위해 추가
using UnityEngine.Networking;
using System.Collections; // IEnumerator를 사용하기 위해 추가

public class SendLogButton : MonoBehaviour
{
    [SerializeField] private Button logButton; // 버튼 연결
    private string serverURI;

    private void Start()
    {
        // EnvLoader.Load() 호출하여 환경 변수를 로드
        EnvLoader.Load("D:/GCUnity/T/simple-telegram-game-backend/.env");

        // .env에서 서버 주소 읽기
        serverURI = EnvLoader.Get("SERVER_URI");

        if (string.IsNullOrEmpty(serverURI))
        {
            Debug.LogError("SERVER_URI is not defined in .env file");
            return;
        }

        if (logButton != null)
        {
            logButton.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("Log Button is not assigned!");
        }
    }

    private void OnButtonClicked()
    {
        StartCoroutine(SendLogToServer("Player clicked the log button!"));
    }

    private IEnumerator SendLogToServer(string logMessage)
    {
        var jsonData = JsonUtility.ToJson(new LogData { message = logMessage });

        using (UnityWebRequest webRequest = new UnityWebRequest(serverURI, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Log successfully sent to server!");
            }
            else
            {
                Debug.LogError($"Failed to send log: {webRequest.error}");
            }
        }
    }

    [System.Serializable]
    private class LogData
    {
        public string message;
    }
}
