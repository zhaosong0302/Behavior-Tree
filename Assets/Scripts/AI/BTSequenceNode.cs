using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���нڵ� 
/// �ص㣺
/// 1.������ִ���Լ����ӽڵ�
/// 2.���ĳһ���ӽڵ�ִ��ʧ���� �ͻ�ͣ���� Ȼ�󷵻�ʧ��
/// 3.���û��һ���ڵ�ʧ�ܣ���ô��ִ���������ӽڵ���߼� ���ҷ��سɹ�
/// </summary>
public class BTSequenceNode : BTControlNode
{
    public override E_NodeState Execute()
    {
        //���ڼ�¼��ǰȡ���Ľڵ�
        BTBaseNode childNode;

        //�����Լ����ӽڵ㣬����ȥִ���Լ����ӽڵ�
        //while (childList.Count != 0)
        //{

        //ȡ����ǰλ���ӽڵ�
        childNode = childList[nowIndex];

        //ִ���ӽڵ� �����ӽڵ��� ��������������ô��
        switch (childNode.Execute())
        {
            case E_NodeState.Success:
                //�����ǰ�ڵ�ִ�гɹ��ˣ���ô����ִ����һ���ڵ���߼�
                ++nowIndex;
                if(nowIndex == childList.Count)
                {
                    //֮��������Ϊ0 ����Ϊ ��˳��ڵ����нڵ㶼ִ�й�һ���� ��һ����ִ��
                    //��Ӧ�ôӵ�һ���ӽڵ㿪ʼ��
                    nowIndex = 0;
                    return E_NodeState.Success;
                }
                break;
            case E_NodeState.Fail:
                //���ʧ���ˣ���һ��ҲӦ�ô�ͷ��ʼִ��
                nowIndex = 0;
                return E_NodeState.Fail;
            case E_NodeState.Running:
                return E_NodeState.Running;
            default:
                break;
        }
        //}
        //ֻ��һ���������ⷵ�� 
        //�ɹ� ���ҽڵ�֮����Ҫ����ִ��
        return E_NodeState.Success;
    }
}
