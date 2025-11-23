using UnityEngine;

public class SimplePlayer : MonoBehaviour
{
    public int health = 4;
    public int attack = 1;
    public int maxHealth = 4;
    private SimpleOre targetOre;
    private Vector2 currentDirection = Vector2.down;

    void Start()
    {
        // 确保有刚体
        if (GetComponent<Rigidbody2D>() == null)
            gameObject.AddComponent<Rigidbody2D>();

        GetComponent<Rigidbody2D>().gravityScale = 3f;
        UpdateStats();

        Debug.Log("玩家初始化完成");
    }

    void Update()
    {
        // 移动
        float h = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(h * 5, GetComponent<Rigidbody2D>().velocity.y);

        // 转向
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentDirection = Vector2.left;
            Debug.Log("转向左边");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentDirection = Vector2.right;
            Debug.Log("转向右边");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentDirection = Vector2.down;
            Debug.Log("转向下边");
        }

        // 找矿石
        FindOre();

        // 挖矿控制
        HandleDigging();

        // 可视化射线
        Debug.DrawRay(transform.position, currentDirection, GetDirectionColor());
    }

    void FindOre()
    {
        // 使用OverlapCircle检测，更可靠
        Vector2 checkPos = (Vector2)transform.position + currentDirection * 0.8f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(checkPos, 0.4f);

        SimpleOre newTarget = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Ore"))
            {
                SimpleOre ore = hit.GetComponent<SimpleOre>();
                if (ore != null)
                {
                    newTarget = ore;
                    break;
                }
            }
        }

        // 如果目标变化
        if (newTarget != targetOre)
        {
            if (targetOre != null)
                targetOre.StopDigging();

            targetOre = newTarget;

            if (targetOre != null)
                Debug.Log($"找到矿石: {targetOre.oreType}");
        }
    }

    void HandleDigging()
    {
        if (targetOre == null) return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("按下J键，开始挖掘");
            targetOre.StartDigging();
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            Debug.Log("松开J键，停止挖掘");
            targetOre.StopDigging();
        }
    }

    Color GetDirectionColor()
    {
        if (currentDirection == Vector2.down) return Color.red;
        if (currentDirection == Vector2.left) return Color.blue;
        if (currentDirection == Vector2.right) return Color.green;
        return Color.white;
    }

    public void UpdateStats()
    {
        health = Mathf.Min(health, maxHealth);
        Debug.Log($"状态: 生命{health}/{maxHealth} 攻击{attack}");
    }
}