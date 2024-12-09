using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject scrollContent;

    [SerializeField]
    private GameObject rankCard;

    private void Start()
    {
        // TODO: 서버로 요청 후 데이터를 받아온 뒤, RankCard 컴포넌트를 가져와 Initialize() 메서드로 정보 기입(이름, 아바타, 순위 , 점수)
        for (int i = 0; i < 50; i++)
        {
            Instantiate(rankCard, scrollContent.transform);
        }
    }
}