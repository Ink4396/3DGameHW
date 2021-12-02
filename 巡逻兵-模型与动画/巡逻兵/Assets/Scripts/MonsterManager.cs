using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public bool moveable;           //释放可移动
    public bool stop;               //释放暂停移动
    public bool change;             //需要改变方向
    Animator animator;
    FollowManager followManager;
    float hitCounter;               //攻击冷却
    float collisionCounter;         //撞墙冷却
    private void Start()
    {
        moveable = false;
        animator = gameObject.GetComponent<Animator>();
        followManager = gameObject.GetComponent<FollowManager>();
        collisionCounter = 0;
        stop = false;
    }

    //设置速度(动画)
    public void SetSpeed(float speed)
    {
        if (animator == null)
            return;
        animator.SetFloat("speed", speed);
    }

    //攻击
    public void Hit()
    {
        if (animator == null)
            return;
        animator.SetTrigger("Attack");
    }

    //归位
    public void Restart()
    {
        if (animator == null)
            return;
        animator.SetTrigger("restart");
    }

    //判断动画状态
    public bool IsName(string name)
    {
        if (animator == null)
            return false;
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    //碰撞事件，与玩家碰撞时触发攻击操作，撞墙时改变方向
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
