using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 条件节点 最终去进行一个条件判断 没有子节点
/// 条件判断成功就返回 成功 失败就返回 失败 一般不会有进行中
/// </summary>
public class BTConditionNode : BTBaseNode
{
    //条件节点的具体判断 是应该抛给使用AI的对象去进行判断的
    //拖过委托将判断逻辑抛给外部
    private Func<bool> action;

    public BTConditionNode(Func<bool> action)
    {
        this.action = action;
    }

    /// <summary>
    /// 条件判断逻辑执行
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
