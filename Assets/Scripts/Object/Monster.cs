using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour,IAIObj
{
    //子弹预设体
    public GameObject bullet;

    //行为树AI根节点
    private BTControlNode BT_AI_Root;

    //网格寻路组件
    private NavMeshAgent navMeshAgent;
    //当前对象的位置
    private Vector3 nowObjPos;

    public Transform objTransform => this.transform;

    public Vector3 nowPos
    {
        get
        {
            nowObjPos = this.transform.position;
            //为了和我们AI模块的定位规则相同 没有考虑 Y上的位置 主要是在xz平面进行位移
            nowObjPos.y = 0;
            return nowObjPos;
        }
    }

    //用于测试用的目标对象
    //正常情况下，应该通过代码动态的再场景中寻找满足条件的目标 我们这里仅仅是测试
    //所以直接通过拖拽进行关联
    public Transform targetTransform;
    public Vector3 targetObjPos
    {
        get
        {
            //注意：这里减去y方向的0.5 是因为我们用立方体举例子，它的y往上升了0.5
            //为了贴合地面 所以我们减去0.5
            return targetTransform.position - Vector3.up * 0.5f;
        }
    }
    public float atkRange => 2;

    public Vector3 bornPos
    {
        get;
        set;
    }

    //用于处理巡逻逻辑的逻辑对象
    private PatrolState patrolLogic;
    //用于处理追逐逻辑的逻辑对象
    private ChaseState chaseLogic;
    //用于处理逃跑（返回）逻辑的逻辑对象
    private RunState runLogic;
    //用于处理攻击逻辑的逻辑对象
    private AtkState atkLogic;

    // Start is called before the first frame update
    void Start()
    {
        //声明根节点
        BT_AI_Root = new BTSelectNode();
        //去声明四个子节点（顺序节点）
        //攻击相关
        atkLogic = new AtkState(this);
        BTSequenceNode atkNode = CreateSequenceNode(atkLogic.CanAtk, atkLogic.Update);
        //返回相关
        runLogic = new RunState(this);
        BTSequenceNode backNode = CreateSequenceNode(runLogic.CanRun, runLogic.Update);
        //移动相关
        chaseLogic = new ChaseState(this);
        BTSequenceNode moveNode = CreateSequenceNode(chaseLogic.CanChase, chaseLogic.Update);
        //巡逻相关
        patrolLogic = new PatrolState(this);
        BTSequenceNode patrolNode = CreateSequenceNode(patrolLogic.CanPatrol, patrolLogic.Update);
        BT_AI_Root.AddChild(atkNode, backNode, moveNode, patrolNode);

        //获取寻路组件
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        //出生位置 就是对象一开始所在的位置
        bornPos = this.transform.position;
    }

    //因为我们的节点结构非常的类似 因此我们将其封装成一个方法 避免代码冗余
    private BTSequenceNode CreateSequenceNode(Func<bool> condition, Func<bool> action)
    {
        //顺序节点
        BTSequenceNode node = new BTSequenceNode();
        //条件判断
        BTConditionNode conditionNode = new BTConditionNode(condition);//暂时传空 之后再添加委托
        //行为执行
        BTActionNode actionNode = new BTActionNode(action);//暂时传空 之后再添加委托
        node.AddChild(conditionNode, actionNode);

        return node;
    }

    /// <summary>
    /// 清空AI的某些状态
    /// </summary>
    /// <param name="state">状态2进制数</param>
    public void ClearState(int state)
    {
        //0001 代表 攻击状态
        if ((state & 1) == 1)//或者判断!=0
            atkLogic.ClearState();
        //0010 代表 逃跑状态
        if ((state & 2) == 2)//或者判断!=0
            runLogic.ClearState();
        //0100 代表 追逐状态
        if ((state & 4) == 4)//或者判断!=0
            chaseLogic.ClearState();
        //1000 代表 巡逻状态
        if ((state & 8) == 8)//或者判断!=0
            patrolLogic.ClearState();
    }

    // Update is called once per frame
    void Update()
    {
        BT_AI_Root.Execute();
    }

    public void Atk()
    {
        //暂时不写 之后写到攻击AI时 再去写它
        print("攻击");

        //动态创建自动 发射即可
        GameObject obj = Instantiate(bullet, this.transform.position + this.transform.forward + Vector3.up * 0.5f, this.transform.rotation);
        Destroy(obj, 5f);
    }

    public void ChangeAction(E_Action action)
    {
        print(action);
    }

    public void Move(Vector3 dirOrPos)
    {
        //结束停止移动
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(dirOrPos);
    }

    public void StopMove()
    {
        //该方法过时了
        //navMeshAgent.Stop();
        //停止移动
        navMeshAgent.isStopped = true;
    }
}
