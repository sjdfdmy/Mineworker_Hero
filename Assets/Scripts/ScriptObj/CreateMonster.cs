using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Monster",menuName ="CreateDate/Monster",order = 1)]
public class CreateMonster : ScriptableObject
{
    public enum Level
    {
        firstlevel,
        secondlevel,
        thirdlevel,
    } 

    [Header("ID")]
    public int ID;
    [Header("怪物名称")]
    public string MonsterName;
    [Header("怪物图标")]
    public Sprite MonsterIcon;
    [Header("怪物等级/层级")]
    public Level monsterLevel;
    [Header("怪物生命值")]
    public int MonsterHP;
    [Header("怪物攻击力")]
    public int MonsterATK;
    [Header("怪物介绍")]
    [TextArea]
    public string MonsterInfo;
}
