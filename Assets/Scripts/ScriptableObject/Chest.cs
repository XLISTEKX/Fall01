using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Chest", menuName = "Chest")]
public class Chest : ScriptableObject
{

    [Header("Items")]
    public List<GameObject> Unrare;

    public List<GameObject> Rare;

    public List<GameObject> Mythic;

    public List<GameObject> Legendery;
    [Header("Settings")]
    public List<float> chestChanses;
    public int chestCost;
    public string chestName;

}
