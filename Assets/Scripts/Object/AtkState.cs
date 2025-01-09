using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkState
{
    //下一次攻击的时间
    private float nextAtkTime;

    //下次攻击等待的时间
    private float waitTime = 2f;

    private Monster monster;

    public AtkState(Monster monster)
    {
        this.monster = monster;
    }

    //public override void EnterState()
    //{
    //    Debug.Log("进入攻击状态了");
    //    //进入攻击状态时 认为此时此刻就要攻击
    //    nextAtkTime = Time.time;
    //}


    public bool Update()
    {
        //进入AI状态后 不停的让ai对象去攻击即可
        if (Time.time >= nextAtkTime)
        {
            monster.Atk();
            nextAtkTime = Time.time + waitTime;
        }

        //如果目标和我的距离过远了，我们应该去切换到追逐状态 ，追到了再继续打它
        //if (Vector3.Distance(monster.nowPos, monster.targetObjPos) > monsteratkRange)
        //{

        //}

        //我们可以利用向量和四元数相关知识 让ai对象看向目标对象 也可以简单粗暴的用LookAt
        //我们在这里只是举例子 就使用LookAt来节约一些事件 之后 大家可以根据自己的需求去进行制作
        monster.objTransform.LookAt(monster.targetObjPos + Vector3.up * 0.5f);


        //在追逐过程中 发现超出了 我们的最大距离 就应该切换到回归的状态
        //stateMachine.CheckChangeRun();

        return true;
    }

    public bool CanAtk()
    {
        if (Vector3.Distance(monster.nowPos, monster.targetObjPos) < monster.atkRange)
        {
            //1110
            monster.ClearState(14);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 清理状态相关参数的逻辑
    /// </summary>
    public void ClearState()
    {

    }
}
