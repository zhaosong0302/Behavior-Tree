using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 动作节点 最终去做一个行为的节点 没有子节点
/// </summary>
public class BTActionNode : BTBaseNode
{
    //把动作节点要做的行为 抛给外部去决定
    //如果你要执行的行为不存在失败这种情况 无参无返回值的即可
    //private UnityAction action;
    //如果有失败的情况 那么可以用有返回值的委托
    private Func<bool> action;

    /// <summary>
    /// 在声明该动作节点时，需要把你想执行的逻辑传递进来
    /// </summary>
    /// <param name="action"></param>
    public BTActionNode(Func<bool> action)
    {
        this.action = action;
    }

    /// <summary>
    /// 行为节点做的事情
    /// </summary>
    /// <returns></returns>
    public override E_NodeState Execute()
    {
        //去执行对应的行为
        if (action == null)
            return E_NodeState.Fail;

        return action.Invoke() ? E_NodeState.Success : E_NodeState.Fail;
    }
}
