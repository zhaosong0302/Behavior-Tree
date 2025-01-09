using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����ڵ� ����ȥ��һ����Ϊ�Ľڵ� û���ӽڵ�
/// </summary>
public class BTActionNode : BTBaseNode
{
    //�Ѷ����ڵ�Ҫ������Ϊ �׸��ⲿȥ����
    //�����Ҫִ�е���Ϊ������ʧ��������� �޲��޷���ֵ�ļ���
    //private UnityAction action;
    //�����ʧ�ܵ���� ��ô�������з���ֵ��ί��
    private Func<bool> action;

    /// <summary>
    /// �������ö����ڵ�ʱ����Ҫ������ִ�е��߼����ݽ���
    /// </summary>
    /// <param name="action"></param>
    public BTActionNode(Func<bool> action)
    {
        this.action = action;
    }

    /// <summary>
    /// ��Ϊ�ڵ���������
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
