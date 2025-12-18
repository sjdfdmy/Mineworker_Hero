using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreDropSpawner : MonoBehaviour
{
    private static OreDropSpawner _instance;
    public static OreDropSpawner Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<OreDropSpawner>();
                if (_instance == null)
                {
                    Debug.Log("No OreDropSpawner in scene!");
                }
            }
            return _instance;
        }
    }
    public FlyingOreIcon flyingIconPrefab;  

    public RectTransform targetAnchor;      

    public float flyDuration = 0.6f;

    public List<FlyingOreIcon> flyingIconPool = new List<FlyingOreIcon>();
    public List<RectTransform> rectTransforms = new List<RectTransform>();
    public SpriteRenderer player;


    public void DropOreIcon(SimpleOre targetOre)
    {
        SimpleOre.OreType theoreType = targetOre.oreType;
        bool ismouse = targetOre.isMinedByMouse;
        switch (theoreType)
        {
            case SimpleOre.OreType.Ruby:
                flyingIconPrefab=flyingIconPool[0];
                targetAnchor = rectTransforms[0];
                break;
            case SimpleOre.OreType.Blue:
                flyingIconPrefab = flyingIconPool[1];
                targetAnchor = rectTransforms[1];
                break;
            case SimpleOre.OreType.Purple:
                flyingIconPrefab = flyingIconPool[2];
                targetAnchor = rectTransforms[2];
                break;
            case SimpleOre.OreType.Lava:
                if(ismouse)
                {
                    
                }
                else
                {
                    StartCoroutine(PlayerHurt());
                }
                return;
            default:
                return;
        }
        Vector3 worldPos = targetOre.transform.position;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        FlyingOreIcon icon = Instantiate(flyingIconPrefab, transform);
        icon.StartFlying(screenPos, targetAnchor, flyDuration,theoreType,ismouse);
    }

    IEnumerator PlayerHurt()
    {
        player.color=Color.red;
        yield return new WaitForSeconds(0.25f);
        player.color=Color.white;
        yield break;
    }
}