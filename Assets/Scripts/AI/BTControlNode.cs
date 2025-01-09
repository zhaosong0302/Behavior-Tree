using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ƽڵ���� ����Ҫ�ӽڵ� ������Ҫ��ط���
/// </summary>
public abstract class BTControlNode : BTBaseNode
{
    //���ڴ洢�ӽڵ������ �ýڵ�������ӽڵ㶼��洢�ڸ�List��
    protected List<BTBaseNode> childList = new List<BTBaseNode>();

    //��ǰִ���߼����ӽڵ����
    protected int nowIndex = 0;

    /// <summary>
    /// ����ӽڵ�ķ��� ʹ�ñ䳤���� ��Ϊһ���ڵ������n���ӽڵ� ͨ���䳤���� ���ӵķ���
    /// </summary>
    public virtual void AddChild(params BTBaseNode[] nodes)
    {
        //���ⲿ��ӽ������ӽڵ� ����������
        for (int i = 0; i < nodes.Length; i++)
        {
            childList.Add(nodes[i]);
        }
    }
}
