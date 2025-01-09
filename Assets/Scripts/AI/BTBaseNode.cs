using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行为树基节点
/// </summary>
public abstract class BTBaseNode
{
    /// <summary>
    /// 执行节点逻辑的抽象方法 子类必须去实现该方法
    /// </summary>
    /// <returns></returns>
    public abstract E_NodeState Execute();
}
