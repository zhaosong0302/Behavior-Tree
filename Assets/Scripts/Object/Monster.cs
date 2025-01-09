using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour,IAIObj
{
    //�ӵ�Ԥ����
    public GameObject bullet;

    //��Ϊ��AI���ڵ�
    private BTControlNode BT_AI_Root;

    //����Ѱ·���
    private NavMeshAgent navMeshAgent;
    //��ǰ�����λ��
    private Vector3 nowObjPos;

    public Transform objTransform => this.transform;

    public Vector3 nowPos
    {
        get
        {
            nowObjPos = this.transform.position;
            //Ϊ�˺�����AIģ��Ķ�λ������ͬ û�п��� Y�ϵ�λ�� ��Ҫ����xzƽ�����λ��
            nowObjPos.y = 0;
            return nowObjPos;
        }
    }

    //���ڲ����õ�Ŀ�����
    //��������£�Ӧ��ͨ�����붯̬���ٳ�����Ѱ������������Ŀ�� ������������ǲ���
    //����ֱ��ͨ����ק���й���
    public Transform targetTransform;
    public Vector3 targetObjPos
    {
        get
        {
            //ע�⣺�����ȥy�����0.5 ����Ϊ����������������ӣ�����y��������0.5
            //Ϊ�����ϵ��� �������Ǽ�ȥ0.5
            return targetTransform.position - Vector3.up * 0.5f;
        }
    }
    public float atkRange => 2;

    public Vector3 bornPos
    {
        get;
        set;
    }

    //���ڴ���Ѳ���߼����߼�����
    private PatrolState patrolLogic;
    //���ڴ���׷���߼����߼�����
    private ChaseState chaseLogic;
    //���ڴ������ܣ����أ��߼����߼�����
    private RunState runLogic;
    //���ڴ������߼����߼�����
    private AtkState atkLogic;

    // Start is called before the first frame update
    void Start()
    {
        //�������ڵ�
        BT_AI_Root = new BTSelectNode();
        //ȥ�����ĸ��ӽڵ㣨˳��ڵ㣩
        //�������
        atkLogic = new AtkState(this);
        BTSequenceNode atkNode = CreateSequenceNode(atkLogic.CanAtk, atkLogic.Update);
        //�������
        runLogic = new RunState(this);
        BTSequenceNode backNode = CreateSequenceNode(runLogic.CanRun, runLogic.Update);
        //�ƶ����
        chaseLogic = new ChaseState(this);
        BTSequenceNode moveNode = CreateSequenceNode(chaseLogic.CanChase, chaseLogic.Update);
        //Ѳ�����
        patrolLogic = new PatrolState(this);
        BTSequenceNode patrolNode = CreateSequenceNode(patrolLogic.CanPatrol, patrolLogic.Update);
        BT_AI_Root.AddChild(atkNode, backNode, moveNode, patrolNode);

        //��ȡѰ·���
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        //����λ�� ���Ƕ���һ��ʼ���ڵ�λ��
        bornPos = this.transform.position;
    }

    //��Ϊ���ǵĽڵ�ṹ�ǳ������� ������ǽ����װ��һ������ �����������
    private BTSequenceNode CreateSequenceNode(Func<bool> condition, Func<bool> action)
    {
        //˳��ڵ�
        BTSequenceNode node = new BTSequenceNode();
        //�����ж�
        BTConditionNode conditionNode = new BTConditionNode(condition);//��ʱ���� ֮�������ί��
        //��Ϊִ��
        BTActionNode actionNode = new BTActionNode(action);//��ʱ���� ֮�������ί��
        node.AddChild(conditionNode, actionNode);

        return node;
    }

    /// <summary>
    /// ���AI��ĳЩ״̬
    /// </summary>
    /// <param name="state">״̬2������</param>
    public void ClearState(int state)
    {
        //0001 ���� ����״̬
        if ((state & 1) == 1)//�����ж�!=0
            atkLogic.ClearState();
        //0010 ���� ����״̬
        if ((state & 2) == 2)//�����ж�!=0
            runLogic.ClearState();
        //0100 ���� ׷��״̬
        if ((state & 4) == 4)//�����ж�!=0
            chaseLogic.ClearState();
        //1000 ���� Ѳ��״̬
        if ((state & 8) == 8)//�����ж�!=0
            patrolLogic.ClearState();
    }

    // Update is called once per frame
    void Update()
    {
        BT_AI_Root.Execute();
    }

    public void Atk()
    {
        //��ʱ��д ֮��д������AIʱ ��ȥд��
        print("����");

        //��̬�����Զ� ���伴��
        GameObject obj = Instantiate(bullet, this.transform.position + this.transform.forward + Vector3.up * 0.5f, this.transform.rotation);
        Destroy(obj, 5f);
    }

    public void ChangeAction(E_Action action)
    {
        print(action);
    }

    public void Move(Vector3 dirOrPos)
    {
        //����ֹͣ�ƶ�
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(dirOrPos);
    }

    public void StopMove()
    {
        //�÷�����ʱ��
        //navMeshAgent.Stop();
        //ֹͣ�ƶ�
        navMeshAgent.isStopped = true;
    }
}
