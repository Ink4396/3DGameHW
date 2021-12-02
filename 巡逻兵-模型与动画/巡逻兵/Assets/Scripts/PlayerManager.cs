using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void DealDamage(GameObject player);
    public static event DealDamage dealDamage;              //受伤事件发布

    public float speed;             //当前速度
    public float direction;         //当前方向位移
    public bool moveable;           //是否可移动          
    public int health;              //当前血量
    float damageCounter;            //受伤保护计数器

    Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        speed = 0;
        direction = 0;
        damageCounter = 0;
    }

    //设置速度(动画)
    public void SetSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    //死亡
    public void Die()
    {
        animator.SetBool("alive", false);
        animator.SetTrigger("die");
    }

    //归位
    public void Restart()
    {
        animator.SetTrigger("restart");
    }

    //判断动画状态
    public bool IsName(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    //复活
    public void Revive()
    {
        Restart();
        animator.SetBool("alive", true);
    }

    //受伤判定
    private void OnTriggerEnter(Collider other)
    {
        //如果接受到怪兽的攻击Trigger判定则进行处理
        if (other.gameObject.name == "AttackRange" && damageCounter > 1)
        {
            MonsterManager parentManager = other.gameObject.GetComponentInParent<MonsterManager>();
            //只有怪兽攻击时Trigger有效，避免误判
            if (!parentManager.IsName("Z_Attack"))
                return;
            damageCounter = 0;
            health--;
            if (health <= 0)
            {
                Die();
            }
            //发布受伤事件
            dealDamage(gameObject);
        }
    }


    private void Update()
    {
        //及时进行归位消除
        if (!IsName("die"))
            animator.ResetTrigger("restart");
        damageCounter = (damageCounter + Time.deltaTime) > 5 ? 5 : damageCounter + Time.deltaTime;
    }
}
