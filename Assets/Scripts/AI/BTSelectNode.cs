using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ѡ��ڵ�
/// �ص㣺
/// 1.������ִ���Լ����ӽڵ�
/// 2.�����ǰ�ڵ�ִ�гɹ��� �Ͳ������ִ�к����ڵ�
/// 3.�����ǰ�ڵ�ִ��ʧ���� �ͻ��������ִ�� ֱ���ɹ�
/// 4.��һ����û�гɹ��򷵻�ʧ��
/// </summary>
public class BTSelectNode : BTControlNode
{
    public override E_NodeState Execute()
    {
        //���ڼ�¼��ǰȡ���Ľڵ�
        BTBaseNode childNode;
        //�����ǿ���дwhileѭ��ȥִ�������߼�
        //���ǿ��ܻ���� ��ͬһ֡����ʵ�ֵ��ӽڵ�̫�� �߼����ڸ���
        //���ܻ�������ܵ�����
        //��������ȫ���Բ���һ֡��������ô���� ���Ƿ�ִ֡��
        //���ǾͿ���ȥ��whileѭ����
        //while (childList.Count != 0)
        //{
        childNode = childList[nowIndex];

        switch (childNode.Execute())
        {
            //���ѡ��ڵ��� ��ĳ���ڵ�ִ�гɹ� �Ͳ��ؼ�������ִ����
            //ֱ�ӷ��سɹ�����
            case E_NodeState.Success:
                nowIndex = 0;
                return E_NodeState.Success;
            case E_NodeState.Fail:
                ++nowIndex;
                //�Ѿ�û�и���Ľڵ����ִ����
                //��֤��ǰ��Ķ�ʧ����
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

        //ֻ�е�ѡ��ڵ�û��ִ����ʱ ���ҵ�ǰ�ڵ�ʧ��ʱ �Ż�������
        //֤����ϣ������һ֡��������ִ�� �������ﷵ�سɹ�
        return E_NodeState.Success;
    }
}
