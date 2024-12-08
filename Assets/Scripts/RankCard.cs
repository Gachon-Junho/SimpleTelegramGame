using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankCard : MonoBehaviour
{
    [SerializeField] 
    private Image rankBadge;
    
    [SerializeField] 
    private Sprite[] badge;
    
    [SerializeField] 
    private TMP_Text rankNumber;
    
    [SerializeField] 
    private Image avatar;
    
    [SerializeField] 
    private TMP_Text name;
    
    [SerializeField] 
    private TMP_Text score;
    
    public void Initialize(int rank, Sprite avatar, string name, int score)
    {
        // TODO: 랭크에 따라 텍스트 또는 스크라이트 사용여부 결정, 데이터 채우기
    }
}