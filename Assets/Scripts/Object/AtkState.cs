using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkState
{
    //��һ�ι�����ʱ��
    private float nextAtkTime;

    //�´ι����ȴ���ʱ��
    private float waitTime = 2f;

    private Monster monster;

    public AtkState(Monster monster)
    {
        this.monster = monster;
    }

    //public override void EnterState()
    //{
    //    Debug.Log("���빥��״̬��");
    //    //���빥��״̬ʱ ��Ϊ��ʱ�˿̾�Ҫ����
    //    nextAtkTime = Time.time;
    //}


    public bool Update()
    {
        //����AI״̬�� ��ͣ����ai����ȥ��������
        if (Time.time >= nextAtkTime)
        {
            monster.Atk();
            nextAtkTime = Time.time + waitTime;
        }

        //���Ŀ����ҵľ����Զ�ˣ�����Ӧ��ȥ�л���׷��״̬ ��׷�����ټ�������
        //if (Vector3.Distance(monster.nowPos, monster.targetObjPos) > monsteratkRange)
        //{

        //}

        //���ǿ���������������Ԫ�����֪ʶ ��ai������Ŀ����� Ҳ���Լ򵥴ֱ�����LookAt
        //����������ֻ�Ǿ����� ��ʹ��LookAt����ԼһЩ�¼� ֮�� ��ҿ��Ը����Լ�������ȥ��������
        monster.objTransform.LookAt(monster.targetObjPos + Vector3.up * 0.5f);


        //��׷������� ���ֳ����� ���ǵ������� ��Ӧ���л����ع��״̬
        //stateMachine.CheckChangeRun();

        return true;
    }

    public bool CanAtk()
    {
        if (Vector3.Distance(monster.nowPos, monster.targetObjPos) < monster.atkRange)
        {
            //1110
            monster.ClearState(14);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ����״̬��ز������߼�
    /// </summary>
    public void ClearState()
    {

    }
}
