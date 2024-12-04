using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DoughtInfo", menuName = "Doughnut/Dought Info", order = 0)]
public class DoughnutInfo : ScriptableObject
{
    public int Level;
    public float Scale;
    public int Score;
    public Sprite Sprite;
    public GameObject Doughnut;

    public Doughnut Instantiate()
    {
        var obj = Instantiate(Doughnut).GetComponent<Doughnut>();

        obj.Level = Level;
        obj.Sprite = Sprite;
        obj.Score = Score;

        return obj;
    }
}