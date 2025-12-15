using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MonsterInfoUI : MonoBehaviour
{
    [Header("UI组件")]
    public Image monsterIcon;

    public TextMeshProUGUI monsterNameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI specialText;

    [Header("当前显示的怪物数据")]
    public CreateMonster currentMonsterData;
    public int currentFloor = 1;

    [Header("每层可选怪物列表")]
    public CreateMonster[] floor1Monsters; // 第1层可能出现的怪物
    public CreateMonster[] floor2Monsters; // 第2层可能出现的怪物
    public CreateMonster[] floor3Monsters; // 第3层可能出现的怪物

    void Start()
    {
        // 初始显示第1层随机怪物
        ShowRandomMonsterForFloor(1);
    }

    // 显示指定层的随机怪物
    public void ShowRandomMonsterForFloor(int floor)
    {
        currentFloor = floor;

        // 根据层数获取对应的怪物列表
        CreateMonster[] availableMonsters = GetMonstersForFloor(floor);

        if (availableMonsters == null || availableMonsters.Length == 0)
        {
            Debug.LogWarning($"第{floor}层没有配置怪物");
            ClearUI();
            return;
        }

        // 随机选择一个怪物
        int randomIndex = Random.Range(0, availableMonsters.Length);
        currentMonsterData = availableMonsters[randomIndex];

        // 更新UI
        UpdateUI();

        Debug.Log($"第{floor}层显示怪物: {currentMonsterData.MonsterName}");
    }

    // 获取指定层的怪物列表
    CreateMonster[] GetMonstersForFloor(int floor)
    {
        return floor switch
        {
            1 => floor1Monsters,
            2 => floor2Monsters,
            3 => floor3Monsters,
            _ => null
        };
    }

    // 更新UI显示
    void UpdateUI()
    {
        if (currentMonsterData == null)
        {
            ClearUI();
            return;
        }

        // 怪物图标
        if (monsterIcon != null)
        {
            monsterIcon.sprite = currentMonsterData.MonsterIcon;
            monsterIcon.enabled = currentMonsterData.MonsterIcon != null;
        }

        // 怪物名称
        if (monsterNameText != null)
        {
            monsterNameText.text = currentMonsterData.MonsterName;
        }

        // 所在层数

        // 生命值（使用CreateMonster配置的值）
        if (healthText != null)
        {
            // 注意：策划案要求怪物属性在一定范围内随机
            // 但你的CreateMonster已经有固定值，这里可以：
            // 1. 直接使用固定值
            healthText.text = $" {currentMonsterData.MonsterHP}";

            // 2. 或者在范围内随机（如果需要随机的话）
            // int randomHP = GetRandomMonsterHP(currentFloor);
            // healthText.text = $"生命值: {randomHP}";
        }

        // 攻击力
        if (attackText != null)
        {
            attackText.text = $" {currentMonsterData.MonsterATK}";
        }

        // 怪物介绍/特殊特征
        if (specialText != null)
        {
            specialText.text = GetMonsterSpecialAbility(currentMonsterData);
        }
    }

    // 获取怪物特殊能力描述
    string GetMonsterSpecialAbility(CreateMonster monster)
    {
        // 根据怪物类型返回特殊能力描述
        // 这里需要根据你的怪物类来判断
        if (monster.MonsterName.Contains("幽灵"))
            return "受到攻击有50%的概率不受伤害";
        if (monster.MonsterName.Contains("蝙蝠"))
            return "每次攻击恢复3点生命值";
        if (monster.MonsterName.Contains("火苗"))
            return "火焰攻击";
        if (monster.MonsterName.Contains("藤蔓"))
            return "缠绕攻击";

        return monster.MonsterInfo; // 使用CreateMonster中的介绍
    }

    // 清空UI
    void ClearUI()
    {
        if (monsterIcon != null) monsterIcon.enabled = false;
        
        
        if (healthText != null) healthText.text = "";
        if (attackText != null) attackText.text = "";
        if (specialText != null) specialText.text = "";
    }

    // 获取当前怪物信息（用于战斗系统）
    public CreateMonster GetCurrentMonsterData()
    {
        return currentMonsterData;
    }

    // 测试功能
    [ContextMenu("测试：显示随机怪物")]
    void TestRandomMonster()
    {
        ShowRandomMonsterForFloor(currentFloor);
    }

    [ContextMenu("测试：切换到下一层")]
    void TestNextFloor()
    {
        int nextFloor = currentFloor % 3 + 1;
        ShowRandomMonsterForFloor(nextFloor);
    }
}