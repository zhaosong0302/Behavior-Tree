using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选择节点
/// 特点：
/// 1.会依次执行自己的子节点
/// 2.如果当前节点执行成功了 就不会继续执行后续节点
/// 3.如果当前节点执行失败了 就会继续往后执行 直到成功
/// 4.若一个都没有成功则返回失败
/// </summary>
public class BTSelectNode : BTControlNode
{
    public override E_NodeState Execute()
    {
        //用于记录当前取出的节点
        BTBaseNode childNode;
        //这里是可以写while循环去执行所有逻辑
        //但是可能会造成 在同一帧当中实现的子节点太多 逻辑过于复杂
        //可能会造成性能的消耗
        //那我们完全可以不在一帧当中做那么多事 而是分帧执行
        //我们就可以去掉while循环了
        //while (childList.Count != 0)
        //{
        childNode = childList[nowIndex];

        switch (childNode.Execute())
        {
            //如果选择节点中 有某个节点执行成功 就不必继续往后执行了
            //直接返回成功即可
            case E_NodeState.Success:
                nowIndex = 0;
                return E_NodeState.Success;
            case E_NodeState.Fail:
                ++nowIndex;
                //已经没有更多的节点可以执行了
                //那证明前面的都失败了
                if (nowIndex == childList.Count)
                {
                    nowIndex = 0;
                    return E_NodeState.Fail;
                }
                break;
            case E_NodeState.Running:
                return E_NodeState.Running;
            default:
                break;
        }
        //}

        //只有当选择节点没有执行完时 并且当前节点失败时 才会来到这
        //证明还希望再下一帧继续往后执行 所以这里返回成功
        return E_NodeState.Success;
    }
}
