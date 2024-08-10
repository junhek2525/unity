using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMove playerMove;
    Animator animator;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponentInChildren<Animator>();
    }

    private string lastAnimation = null;

    public void PlayAnimation(string anim)
    {
        if (lastAnimation != anim)
        {
            animator.Play(anim);
            lastAnimation = anim;
        }
    }

    public void Update()
    {
        // ���� �ִϸ��̼� ���� ������ ������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Ư�� �ִϸ��̼��� ��� ������ Ȯ��
        if (stateInfo.IsName("Attack"))
        {
            // �ִϸ��̼��� �������� Ȯ�� (normalizedTime�� 1.0f �̻��̸� �ִϸ��̼��� ����� ��)
            if (stateInfo.normalizedTime >= 1.0f)
            {
                playerMove.isAttack = false;   
            }
        }
    }
}
