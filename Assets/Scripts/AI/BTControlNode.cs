using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制节点基类 就需要子节点 并且需要相关方法
/// </summary>
public abstract class BTControlNode : BTBaseNode
{
    //用于存储子节点的容器 该节点的所有子节点都会存储在该List中
    protected List<BTBaseNode> childList = new List<BTBaseNode>();

    //当前执行逻辑的子节点序号
    protected int nowIndex = 0;

    /// <summary>
    /// 添加子节点的方法 使用变长参数 因为一个节点可能有n个子节点 通过变长参数 更加的方便
    /// </summary>
    public virtual void AddChild(params BTBaseNode[] nodes)
    {
        //把外部添加进来的子节点 存入容器中
        for (int i = 0; i < nodes.Length; i++)
        {
            childList.Add(nodes[i]);
        }
    }
}
