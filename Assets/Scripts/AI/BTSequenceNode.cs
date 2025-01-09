using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 序列节点 
/// 特点：
/// 1.会依次执行自己的子节点
/// 2.如果某一个子节点执行失败了 就会停下来 然后返回失败
/// 3.如果没有一个节点失败，那么会执行完所有子节点的逻辑 并且返回成功
/// </summary>
public class BTSequenceNode : BTControlNode
{
    public override E_NodeState Execute()
    {
        //用于记录当前取出的节点
        BTBaseNode childNode;

        //遍历自己的子节点，依次去执行自己的子节点
        //while (childList.Count != 0)
        //{

        //取出当前位置子节点
        childNode = childList[nowIndex];

        //执行子节点 根据子节点结果 来决定接下来怎么做
        switch (childNode.Execute())
        {
            case E_NodeState.Success:
                //如果当前节点执行成功了，那么继续执行下一个节点的逻辑
                ++nowIndex;
                if(nowIndex == childList.Count)
                {
                    //之所以设置为0 是因为 该顺序节点所有节点都执行过一次了 下一次再执行
                    //就应该从第一个子节点开始了
                    nowIndex = 0;
                    return E_NodeState.Success;
                }
                break;
            case E_NodeState.Fail:
                //如果失败了，下一次也应该从头开始执行
                nowIndex = 0;
                return E_NodeState.Fail;
            case E_NodeState.Running:
                return E_NodeState.Running;
            default:
                break;
        }
        //}
        //只有一种情况会从这返回 
        //成功 并且节点之后还需要继续执行
        return E_NodeState.Success;
    }
}
