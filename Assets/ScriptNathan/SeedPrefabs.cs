using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "ScriptableObjects/Seed")]
public class SeedPrefab : ScriptableObject
{
    public string seedName;
    public Sprite seedSprite;
    public GameObject seedPrefab;
}