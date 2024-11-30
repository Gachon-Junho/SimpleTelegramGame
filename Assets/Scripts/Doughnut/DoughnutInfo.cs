using UnityEngine;

[CreateAssetMenu(fileName = "DoughtInfo", menuName = "Doughnut/Dought Info", order = 0)]
public class DoughnutInfo : ScriptableObject
{
    public int Type;
    public Sprite Sprite;
    public GameObject Doughnut;

    public Doughnut Instantiate()
    {
        var obj = Instantiate(Doughnut).GetComponent<Doughnut>();

        obj.Type = Type;
        obj.Sprite = Sprite;

        return obj;
    }
}