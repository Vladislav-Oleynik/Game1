using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomotionAnimationSpeedSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;
    protected AnimationClip[] currentAttackAnimSet;
    public AnimationClip[] defaultAttackAnimSet;
    public AnimationClip replaceableAttackAnimation;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSpeedSmoothTime, Time.deltaTime);

        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnimation.name] = currentAttackAnimSet[attackIndex];
    }
}
