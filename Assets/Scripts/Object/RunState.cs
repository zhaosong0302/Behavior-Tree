using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回归状态处理
/// </summary>
public class RunState
{
    private Monster monster;

    private bool isRun = false;

    public RunState(Monster monster) 
    {
        this.monster = monster;
    }

    public bool Update()
    {
        //进入回归状态时 命令ai对象 回到自己的出生点即可
        //monster.Move(monster.bornPos);
        //判断是否回到了出生点
        //到达出生点后 进入到 巡逻状态即可
        //if (Vector3.Distance(monster.nowPos, monster.bornPos) <= 0.5f)
        //{
        //    stateMachine.ChangeState(E_AI_State.Patrol);
        //}
        return true;
    }

    public bool CanRun()
    {
        //如果当前处于返回逃跑状态 并且已经回到出生点了 就不用在逃跑了
        if(Vector3.Distance(monster.nowPos, monster.bornPos) < 1f && isRun)
        {
            isRun = false;
            monster.StopMove();
            return false;
        }
        //只有超过一定距离时 才会去进行返回
        if (Vector3.Distance(monster.nowPos, monster.bornPos) > 15 || isRun)
        {
            //1101
            monster.ClearState(13);
            isRun = true;
            monster.Move(monster.bornPos);
            return true;
        }
            
        return false;
    }

    /// <summary>
    /// 清理状态相关参数的逻辑
    /// </summary>
    public void ClearState()
    {
        isRun = false;
    }
}
