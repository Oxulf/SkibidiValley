using UnityEngine;

[CreateAssetMenu(fileName = "NewSeed", menuName = "Farming/Seed")]
public class SeedData : MonoBehaviour
{
    public string seedName;
    public Sprite seedSprite;
    public Sprite harvestableSprite;
    public GameObject harvestablePrefab;
}