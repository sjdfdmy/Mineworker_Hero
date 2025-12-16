using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CreateSimpleAnimations : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Create Simple Animations")]
    static void CreateAnimations()
    {
        // 创建Animator Controller
        var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath("Assets/PlayerAnimator.controller");

        // 添加参数
        controller.AddParameter("IsMoving", AnimatorControllerParameterType.Bool);
        controller.AddParameter("IsMining", AnimatorControllerParameterType.Bool);

        // 获取状态机
        var stateMachine = controller.layers[0].stateMachine;

        // 创建三个状态
        var idleState = stateMachine.AddState("Idle");
        var moveState = stateMachine.AddState("Move");
        var mineState = stateMachine.AddState("Mine");

        // 创建简单的动画剪辑
        var idleClip = CreateBasicIdleAnimation();
        var moveClip = CreateBasicMoveAnimation();
        var mineClip = CreateBasicMineAnimation();

        // 分配动画
        idleState.motion = idleClip;
        moveState.motion = moveClip;
        mineState.motion = mineClip;

        // 设置默认状态
        stateMachine.defaultState = idleState;

        // 创建过渡
        var idleToMove = idleState.AddTransition(moveState);
        idleToMove.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "IsMoving");
        idleToMove.hasExitTime = false;

        var moveToIdle = moveState.AddTransition(idleState);
        moveToIdle.AddCondition(UnityEditor.Animations.AnimatorConditionMode.IfNot, 0, "IsMoving");
        moveToIdle.hasExitTime = false;

        var idleToMine = idleState.AddTransition(mineState);
        idleToMine.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "IsMining");
        idleToMine.hasExitTime = false;

        var mineToIdle = mineState.AddTransition(idleState);
        mineToIdle.AddCondition(UnityEditor.Animations.AnimatorConditionMode.IfNot, 0, "IsMining");
        mineToIdle.hasExitTime = false;

        // 从挖矿到移动的过渡
        var mineToMove = mineState.AddTransition(moveState);
        mineToMove.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "IsMoving");
        mineToMove.hasExitTime = false;

        // 从移动到挖矿的过渡
        var moveToMine = moveState.AddTransition(mineState);
        moveToMine.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "IsMining");
        moveToMine.hasExitTime = false;

        AssetDatabase.SaveAssets();
    }

    static AnimationClip CreateBasicIdleAnimation()
    {
        AnimationClip clip = new AnimationClip();
        clip.name = "Idle";

        // 简单的上下浮动
        AnimationCurve curve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.5f, 0.1f),
            new Keyframe(1f, 0f)
        );
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);
        clip.wrapMode = WrapMode.Loop;

        return clip;
    }

    static AnimationClip CreateBasicMoveAnimation()
    {
        AnimationClip clip = new AnimationClip();
        clip.name = "Move";

        // 简单的左右摇摆
        AnimationCurve curve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.25f, 10f),
            new Keyframe(0.5f, 0f),
            new Keyframe(0.75f, -10f),
            new Keyframe(1f, 0f)
        );
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curve);
        clip.wrapMode = WrapMode.Loop;

        return clip;
    }

    static AnimationClip CreateBasicMineAnimation()
    {
        AnimationClip clip = new AnimationClip();
        clip.name = "Mine";

        // 简单的挖矿旋转
        AnimationCurve curve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.1f, 30f),
            new Keyframe(0.3f, -20f),
            new Keyframe(0.5f, 0f),
            new Keyframe(1f, 0f)
        );
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curve);
        clip.wrapMode = WrapMode.Loop;

        return clip;
    }
#endif
}