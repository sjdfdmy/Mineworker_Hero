using UnityEngine;

public class FMineModeManager : MonoBehaviour
{
    public static FMineModeManager Instance;

    public bool isFMouseMineActive = false;  // F键模式是否激活
    public bool hasUsedFMouseMine = false;   // 本次是否已使用

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFMouseMineMode();
        }
    }

    void ToggleFMouseMineMode()
    {
        isFMouseMineActive = !isFMouseMineActive;
        hasUsedFMouseMine = false; // 重置使用状态

        if (isFMouseMineActive)
        {
            Debug.Log("F键挖矿模式激活！可以鼠标点击挖掉一块矿石");
        }
        else
        {
            Debug.Log("F键挖矿模式关闭");
        }
    }

    public bool TryUseFMouseMine()
    {
        if (isFMouseMineActive && !hasUsedFMouseMine)
        {
            hasUsedFMouseMine = true;
            isFMouseMineActive = false; // 使用后自动关闭
            return true;
        }
        return false;
    }

    public bool CanUseFMouseMine()
    {
        return isFMouseMineActive && !hasUsedFMouseMine;
    }
}