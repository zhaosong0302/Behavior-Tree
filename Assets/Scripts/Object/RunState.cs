using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ع�״̬����
/// </summary>
public class RunState
{
    private Monster monster;

    private bool isRun = false;

    public RunState(Monster monster) 
    {
        this.monster = monster;
    }

    public bool Update()
    {
        //����ع�״̬ʱ ����ai���� �ص��Լ��ĳ����㼴��
        //monster.Move(monster.bornPos);
        //�ж��Ƿ�ص��˳�����
        //���������� ���뵽 Ѳ��״̬����
        //if (Vector3.Distance(monster.nowPos, monster.bornPos) <= 0.5f)
        //{
        //    stateMachine.ChangeState(E_AI_State.Patrol);
        //}
        return true;
    }

    public bool CanRun()
    {
        //�����ǰ���ڷ�������״̬ �����Ѿ��ص��������� �Ͳ�����������
        if(Vector3.Distance(monster.nowPos, monster.bornPos) < 1f && isRun)
        {
            isRun = false;
            monster.StopMove();
            return false;
        }
        //ֻ�г���һ������ʱ �Ż�ȥ���з���
        if (Vector3.Distance(monster.nowPos, monster.bornPos) > 15 || isRun)
        {
            //1101
            monster.ClearState(13);
            isRun = true;
            monster.Move(monster.bornPos);
            return true;
        }
            
        return false;
    }

    /// <summary>
    /// ����״̬��ز������߼�
    /// </summary>
    public void ClearState()
    {
        isRun = false;
    }
}
