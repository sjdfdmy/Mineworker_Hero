using UnityEngine;
using static TreeEditor.TreeEditorHelper;
using static SimpleOre;

public class SimpleOre : MonoBehaviour
{
    public enum OreType { Normal, Ruby, Blue, Purple, Hard, Lava }
   

    public OreType oreType;
    private float digProgress = 0f;
    private bool isBeingDug = false;

    void Start()
    {
        gameObject.tag = "Ore";
    }

    void Update()
    {
        if (isBeingDug)
        {
            digProgress += Time.deltaTime;

            if (digProgress >= 1f)
            {
                CompleteDigging();
            }
        }
    }

    public void StartDigging()
    {
        if (oreType == OreType.Hard)
        {
            Debug.Log("坚硬矿石挖不动！");
            return;
        }

        if (!isBeingDug)
        {
            isBeingDug = true;
            Debug.Log($"开始挖掘，当前进度: {digProgress:F2}");
        }
    }

    public void StopDigging()
    {
        if (isBeingDug)
        {
            isBeingDug = false;
            Debug.Log($"停止挖掘，保存进度: {digProgress:F2}");
        }
    }

    void CompleteDigging()
    {
        Debug.Log($"挖掘完成！总用时: {digProgress:F2}秒");
        ApplyEffect();
        Destroy(gameObject);
    }

    void ApplyEffect()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<SimplePlayer>();
        if (player == null) return;

        switch (oreType)
        {
            case OreType.Ruby:
                player.health += 2;
                player.maxHealth += 2;
                Debug.Log("💖 红宝石！生命+2");
                break;

            case OreType.Blue:
                player.attack += 1;
                Debug.Log("💙 蓝宝石！攻击+1");
                break;

            case OreType.Purple:
                player.health += 3;
                player.attack += 2;
                player.maxHealth += 3;
                Debug.Log("💜 紫水晶！生命+3 攻击+2");
                break;

            case OreType.Lava:
                if (player.health > 1)
                {
                    player.health -= 1;
                    Debug.Log("🔥 熔岩！生命-1");
                }
                else
                {
                    Debug.Log("生命值过低，挖熔岩不会扣血");
                }
                break;

            case OreType.Normal:
                Debug.Log("普通矿石被挖掉");
                break;
        }

        player.UpdateStats();
    }
}