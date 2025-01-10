# Behavior-Tree
    使用代码实现行为树功能，以此为基础模拟AI，同时不断对框架设计进行优化，以得到更好的表现--Implement the function of behavior tree by using code, to achieve AI function. At the same time, constantly optimize the frame design to get better perform.

    行为树(Behavior Tree, BT)在游戏Al中是一种用于控制游戏角色的人工智能技术。在游戏Al中，对象的动作和行为往往是提前编辑好的（比如移动，攻击等)
但是在什么时间或者什么地点执行这些行为是不确定的(比如何时朝目标移动，何时攻击目标等)因此我们需要使用行为树或者状态机来实现这些对象的决策任务。

    由于行为树插件无法拓展的性质，在此使用代码编程实现行为树逻辑功能，在需要拓展时可以方便使用

    主要功能由几个类实现

1.节点基类--BaseNode

所有叶子节点继承自此节点，类中仅包含执行方法，不包含存储子节点的容器

2.控制节点--ControlNode

继承自节点基类，类中添加了存储子节点的容器List<BaseNode>,使用父类装子类，所有非叶子节点继承自此类

3.节点状态枚举类--ENodeState

包含成功Success,运行中Running,失败Fail三种状态

4.选择节点--SelectNode

依次执行子节点，遇到成功返回成功，否则返回失败，继承自非叶子节点基类

5.序列节点--SequenceNode

依次执行子节点，遇到失败返回失败，否则返回成功，继承自非叶子节点基类

注意：执行节点默认认为是叶子节点继承自节点基类，不特别书写一个类声明

如图一种简单的实现方式

![AHJ8AFQCC(S(7R35WO7%60J](https://github.com/user-attachments/assets/fb626bdd-a47d-474b-a353-2025548f7478)

对结构进行优化时对优先级首先进行区别，依次从高到低构建行为树，优先级由高到低依次为

1.最大距离

当超出最大距离时停止一切行为回归出生点

2.追逐距离

在不超过最大距离的情况下进入追逐距离进行追逐

3.攻击距离

在上述两个距离内时进入攻击距离判断，进入攻击距离进行攻击

进入范围后追逐

![1~ {F FL)~3XN$G`G%CG0}O](https://github.com/user-attachments/assets/e874cc74-4f79-4640-982c-66965c30b3a8)

进入范围后攻击，释放一个白色小球模拟子弹

![)YL@64HF`Q(S4@(H{7 3IDU](https://github.com/user-attachments/assets/60ac12fa-8028-460a-b758-a7dd8acaa5e3)

追逐后超出最大范围返回

![XX{HCOG0DX_`PSP}4HC9236](https://github.com/user-attachments/assets/8e22dde3-011f-4e58-9c5a-6af4a2517792)

![({ZG$OJ@UPUUR~K7R~0K4 9](https://github.com/user-attachments/assets/73e41a98-847a-4573-a6f0-a5a103434e0d)
