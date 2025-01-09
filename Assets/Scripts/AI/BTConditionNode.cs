using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ڵ� ����ȥ����һ�������ж� û���ӽڵ�
/// �����жϳɹ��ͷ��� �ɹ� ʧ�ܾͷ��� ʧ�� һ�㲻���н�����
/// </summary>
public class BTConditionNode : BTBaseNode
{
    //�����ڵ�ľ����ж� ��Ӧ���׸�ʹ��AI�Ķ���ȥ�����жϵ�
    //�Ϲ�ί�н��ж��߼��׸��ⲿ
    private Func<bool> action;

    public BTConditionNode(Func<bool> action)
    {
        this.action = action;
    }

    /// <summary>
    /// �����ж��߼�ִ��
    /// </summary>
    /// <returns></returns>
    public override E_NodeState Execute()
    {
        //ȥִ�ж�Ӧ����Ϊ
        if (action == null)
            return E_NodeState.Fail;

        return action.Invoke() ? E_NodeState.Success : E_NodeState.Fail;
    }
}
