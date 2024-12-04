using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoughnutLevel", menuName = "Doughnut/Doughnut Level", order = 0)]
public class DoughnutLevel : ScriptableObject
{
    public List<DoughnutInfo> SpritePerLevel;
}