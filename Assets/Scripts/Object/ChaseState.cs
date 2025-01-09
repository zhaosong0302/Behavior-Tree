using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ׷��Ŀ����߼�����
/// </summary>
public class ChaseState
{
    //���ڼ���������֡ ǰ��Ŀ���һ��
    private int index;

    private Monster monster;

    private bool isChase = false;

    //Ŀ�귶Χ���ľ��� Ŀ����������Χ�� ��Ҫ����ѭ��
    //�л��� ׷��״̬
    private float checkDis;

    public ChaseState(Monster monster)
    {
        this.monster = monster;
        checkDis = 10;
    }


    public bool Update()
    {
        //�Ϳ���ͨ�������߶��� �õ����Ƶ� ai���� �������в��� ����
        //stateMachine.aiObj
        //׷���߼� ������ai���� ��ͣ�ĳ������ǵ�Ŀ������ƶ�����

        //��ͣ����ai���� ����Ŀ���ƶ�����
        //if(index % 10 == 0)
        monster.Move(monster.targetObjPos);

        //++index;

        //��Ҫע�⣺һ������� ���ǻ���Ҫ���� �泯�� ������Ŀ��� �ٹ���
        //��������������˼�� Ӧ����ôȥ�ж��泯����ص��߼�

        //���Լ���Ŀ��λ��С�����Լ��Ĺ�����Χ����ô���Ǿ�Ӧ������ ׷��״̬
        //���빥��״̬
        //if (Vector3.Distance(monster.nowPos, monster.targetObjPos) <= monster.atkRange)
        //{
        //    stateMachine.ChangeState(E_AI_State.Atk);
        //}

        ////��׷������� ���ֳ����� ���ǵ������� ��Ӧ���л����ع��״̬
        //stateMachine.CheckChangeRun();

        return true;
    }

    public bool CanChase()
    {
        //���׷�������
        //ͬʱ���� �����Զ ������׷����
        if (Vector3.Distance(monster.nowPos, monster.bornPos) > 15 && isChase)
        {
            isChase = false;
            return false;
        }

        //������빥����Χ�� �Ͳ���׷���� Ӧ���ǹ���
        if (Vector3.Distance(monster.nowPos, monster.targetObjPos) < checkDis || isChase)
        {
            //1011
            monster.ClearState(11);
            isChase = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// ����״̬��ز������߼�
    /// </summary>
    public void ClearState()
    {
        isChase = false;
    }
}
