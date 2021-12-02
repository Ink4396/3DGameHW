using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void DealDamage(GameObject player);
    public static event DealDamage dealDamage;              //�����¼�����

    public float speed;             //��ǰ�ٶ�
    public float direction;         //��ǰ����λ��
    public bool moveable;           //�Ƿ���ƶ�          
    public int health;              //��ǰѪ��
    float damageCounter;            //���˱���������

    Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        speed = 0;
        direction = 0;
        damageCounter = 0;
    }

    //�����ٶ�(����)
    public void SetSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    //����
    public void Die()
    {
        animator.SetBool("alive", false);
        animator.SetTrigger("die");
    }

    //��λ
    public void Restart()
    {
        animator.SetTrigger("restart");
    }

    //�ж϶���״̬
    public bool IsName(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    //����
    public void Revive()
    {
        Restart();
        animator.SetBool("alive", true);
    }

    //�����ж�
    private void OnTriggerEnter(Collider other)
    {
        //������ܵ����޵Ĺ���Trigger�ж�����д���
        if (other.gameObject.name == "AttackRange" && damageCounter > 1)
        {
            MonsterManager parentManager = other.gameObject.GetComponentInParent<MonsterManager>();
            //ֻ�й��޹���ʱTrigger��Ч����������
            if (!parentManager.IsName("Z_Attack"))
                return;
            damageCounter = 0;
            health--;
            if (health <= 0)
            {
                Die();
            }
            //���������¼�
            dealDamage(gameObject);
        }
    }


    private void Update()
    {
        //��ʱ���й�λ����
        if (!IsName("die"))
            animator.ResetTrigger("restart");
        damageCounter = (damageCounter + Time.deltaTime) > 5 ? 5 : damageCounter + Time.deltaTime;
    }
}
