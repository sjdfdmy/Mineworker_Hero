using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static SimpleOre;

public class FlyingOreIcon : MonoBehaviour
{
    public float arcHeight = 80f; 

    Image img;
    RectTransform rt;

    void Awake()
    {
        img = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
    }


    public void StartFlying(Vector2 startScreenPos, RectTransform endAnchor, float duration,OreType theoreType,bool ismouse)
    {
        rt.anchoredPosition = ScreenPointToAnchored(startScreenPos);
        Vector2 endPos = endAnchor.anchoredPosition;
        Vector2 midPos = (rt.anchoredPosition + endPos) * 0.5f + Vector2.up * arcHeight;

        StartCoroutine(FlyRoutine(rt.anchoredPosition, midPos, endPos, duration,theoreType,ismouse));
    }

    IEnumerator FlyRoutine(Vector2 start, Vector2 mid, Vector2 end, float t, OreType theoreType, bool ismouse)
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<DropBounce>()?.PlayBounce();
        float elapsed = 0;
        while (elapsed < t)
        {
            elapsed += Time.deltaTime;
            float p = elapsed / t;
            Vector2 pos = Vector2.Lerp(
                            Vector2.Lerp(start, mid, p),
                            Vector2.Lerp(mid, end, p),
                            p);
            rt.anchoredPosition = pos;
            rt.localScale = Vector3.one * Mathf.Lerp(1f, 0.3f, p);
            yield return null;
        }


        ApplyOreEffect(theoreType,ismouse);
        Destroy(gameObject);
    }

    void ApplyOreEffect(OreType oreType,bool isminebymouse)
    {
        if (GameDateController.Instance == null) return;

        switch (oreType)
        {
            case OreType.Normal:
                Debug.Log("普通矿石被挖掉");
                break;

            case OreType.Ruby:
                GameDateController.Instance.blood += 2;
                Debug.Log($"红宝石！生命值+2");
                break;

            case OreType.Blue:
                GameDateController.Instance.attack += 1;
                Debug.Log($"蓝宝石！攻击力+1");
                break;

            case OreType.Purple:
                GameDateController.Instance.blood += 3;
                GameDateController.Instance.attack += 2;
                Debug.Log($"紫水晶！生命值+3，攻击力+2");
                break;

            case OreType.Hard:
                Debug.Log($"坚硬矿石被挖掉，无效果");
                break;

            case OreType.Lava:
                if (isminebymouse)
                {
                    Debug.Log($"鼠标挖掉熔岩块！不扣血");
                }
                else
                {
                    if (GameDateController.Instance.blood > 1)
                    {
                        GameDateController.Instance.blood -= 1;
                        Debug.Log($"键盘挖掉熔岩块！生命值-1");
                    }
                    else
                    {
                        Debug.Log($"键盘挖掉熔岩块！生命值为1，不扣血");
                    }
                }
                break;
        }

        UpdateUI(oreType);
    }

    void UpdateUI(OreType oreType)
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateBloodUI();
            UIManager.Instance.UpdateAttackUI();
            if (oreType == OreType.Ruby || oreType == OreType.Blue || oreType == OreType.Purple)
            {
                UIManager.Instance.AddGem(oreType);
            }
        }
    }

    Vector2 ScreenPointToAnchored(Vector2 screenPoint)
    {
        Vector2 localPoint;
        Canvas canvas = GetComponentInParent<Canvas>();
        Camera cam = canvas.worldCamera;   
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            cam = null;                      

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt.parent as RectTransform,
            screenPoint,
            cam,  
            out localPoint);
        return localPoint;
    }
}