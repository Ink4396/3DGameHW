using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public bool moveable;           //�ͷſ��ƶ�
    public bool stop;               //�ͷ���ͣ�ƶ�
    public bool change;             //��Ҫ�ı䷽��
    Animator animator;
    FollowManager followManager;
    float hitCounter;               //������ȴ
    float collisionCounter;         //ײǽ��ȴ
    private void Start()
    {
        moveable = false;
        animator = gameObject.GetComponent<Animator>();
        followManager = gameObject.GetComponent<FollowManager>();
        collisionCounter = 0;
        stop = false;
    }

    //�����ٶ�(����)
    public void SetSpeed(float speed)
    {
        if (animator == null)
            return;
        animator.SetFloat("speed", speed);
    }

    //����
    public void Hit()
    {
        if (animator == null)
            return;
        animator.SetTrigger("Attack");
    }

    //��λ
    public void Restart()
    {
        if (animator == null)
            return;
        animator.SetTrigger("restart");
    }

    //�ж϶���״̬
    public bool IsName(string name)
    {
        if (animator == null)
            return false;
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    //��ײ�¼����������ײʱ��������������ײǽʱ�ı䷽��
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "wall" && collisionCounter > 0.2)
        {
            change = true;
            collisionCounter = 0;
        }
        if (collision.gameObject.tag == "player" && hitCounter > 4)
        {
            Hit();
            followManager.stop = true;
            hitCounter = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall" && collisionCounter > 0.2)
        {
            change = true;
            collisionCounter = 0;
        }
        if (collision.gameObject.tag == "player" && hitCounter > 4)
        {
            Hit();
            followManager.stop = true;
            hitCounter = 0;
        }
    }

    private void Update()
    {
        hitCounter = (hitCounter + Time.deltaTime) > 5 ? 5 : hitCounter + Time.deltaTime;
        collisionCounter = (collisionCounter + Time.deltaTime) > 5 ? 5 : collisionCounter + Time.deltaTime;
        if (followManager.stop && !IsName("Z_Attack"))
            followManager.stop = false;
    }
}
