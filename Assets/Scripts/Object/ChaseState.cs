using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追逐目标的逻辑处理
/// </summary>
public class ChaseState
{
    //用于计数，隔几帧 前往目标点一次
    private int index;

    private Monster monster;

    private bool isChase = false;

    //目标范围检测的距离 目标进入这个范围内 就要脱离循环
    //切换到 追逐状态
    private float checkDis;

    public ChaseState(Monster monster)
    {
        this.monster = monster;
        checkDis = 10;
    }


    public bool Update()
    {
        //就可以通过管理者对象 得到控制的 ai对象 对它进行操作 即可
        //stateMachine.aiObj
        //追逐逻辑 就是让ai对象 不停的朝向我们的目标进行移动即可

        //不停的让ai对象 朝向目标移动即可
        //if(index % 10 == 0)
        monster.Move(monster.targetObjPos);

        //++index;

        //需要注意：一般情况下 我们还需要处理 面朝向 朝向了目标后 再攻击
        //这里可以留给大家思考 应该怎么去判断面朝向相关的逻辑

        //当自己和目标位置小于了自己的攻击范围，那么我们就应该脱离 追逐状态
        //进入攻击状态
        //if (Vector3.Distance(monster.nowPos, monster.targetObjPos) <= monster.atkRange)
        //{
        //    stateMachine.ChangeState(E_AI_State.Atk);
        //}

        ////在追逐过程中 发现超出了 我们的最大距离 就应该切换到回归的状态
        //stateMachine.CheckChangeRun();

        return true;
    }

    public bool CanChase()
    {
        //如果追逐过程中
        //同时满足 距离过远 并且在追逐中
        if (Vector3.Distance(monster.nowPos, monster.bornPos) > 15 && isChase)
        {
            isChase = false;
            return false;
        }

        //如果进入攻击范围了 就不能追逐了 应该是攻击
        if (Vector3.Distance(monster.nowPos, monster.targetObjPos) < checkDis || isChase)
        {
            //1011
            monster.ClearState(11);
            isChase = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 清理状态相关参数的逻辑
    /// </summary>
    public void ClearState()
    {
        isChase = false;
    }
}
