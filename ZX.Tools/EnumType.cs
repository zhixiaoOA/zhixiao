using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Tools
{
    /// <summary>
    /// 状态代码
    /// </summary>
    public enum StatusCodeEnum
    {
        /// <summary>
        /// 200
        /// </summary>
        成功 = 200,
        /// <summary>
        /// 300
        /// </summary>
        失败 = 300,
        /// <summary>
        /// 301
        /// </summary>
        超时 = 301,
        /// <summary>
        /// 500
        /// </summary>
        轮循 = 500
    }
    public enum AuthType
    {
        /// <summary>
        /// 菜单权限
        /// </summary>
        MenuAuth = 1,
        /// <summary>
        /// 按钮权限
        /// </summary>
        ButtonAuth = 2
    }

    /// <summary>
    /// 菜单可操作类型
    /// </summary>
    public enum ButtonType
    {
        // 0-"",1-新增记录,2-编辑记录,3-删除记录,4-查看记录
        [Description("")]
        nullspace = 0,
        [Description("新增记录")]
        add = 1,
        [Description("编辑记录")]
        edit = 2,
        [Description("删除记录")]
        del = 3,
        [Description("查看记录")]
        select = 4

    }

    /// <summary>
    /// 项目状态
    /// </summary>
    public enum ProjectStatus
    {
        // 0：有效 10：挂起 20：完成 30：删除
        [Description("有效")]
        hg = 0,
        [Description("挂起")]
        hblhggsy = 10,
        [Description("完成")]
        dzdgjtx = 20,
        [Description("删除")]
        bgwjjgy = 30

    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TaskStatus
    {
        未完成 = 1,
        未开始,
        进行中,
        已完成,
        已取消,
        已关闭
    }

    /// <summary>
    /// 项目问题状态
    /// </summary>
    public enum ProjectBugStatus
    {
        /// <summary>
        /// 1
        /// </summary>
        新项目 = 1,
        /// <summary>
        /// 2
        /// </summary>
        开发中 = 2,
        /// <summary>
        /// 3
        /// </summary>
        测试中 = 3,
        /// <summary>
        /// 4
        /// </summary>
        待收款 = 4,
        /// <summary>
        /// 5
        /// </summary>
        已完结 = 5
    }
    /// <summary>
    /// 首页展示模块
    /// </summary>
    public enum MoldType
    {
        公司公告 = 1,
        公司活动 = 2,
        //公司资源 = 3,
        记事便签 = 4,
        工作任务 = 5
    }

    public enum ApplyStatus
    {
        新申请, 审核中, 已审核, 驳回, 草稿
    }

    #region 申请单类型
    /// <summary>
    /// 申请单类型
    /// </summary>
    public enum ApplyType
    {
        加班及调休申请单 = 36,
        出差申请单,
        申请单,
        所需物品领用申请单,
        物资采购申请单,
        合同协议审批申请单,

        未打卡证明申请单 = 43,
        印章使用审批申请单,
        印章外带审批申请单,
        /// <summary>
        /// 事假
        /// </summary>
        员工请假申请单,
        招待审批申请单,
        资产领取申请单 = 49,
        员工请假申请单_婚假 = 62,
        员工请假申请单_产假,
        员工请假申请单_丧假,
        员工请假申请单_工伤假,
        员工请假申请单_病假,
        费用申请单 = 88
    }
    #endregion
    #region
    /// <summary>
    /// 付款方式
    /// </summary>
    public enum Payment
    {
        现金 = 1,
        汇款 = 2,
        转账 = 3
    }
    #endregion

    #region 请假单类型
    /// <summary>
    /// 请假单类型
    /// </summary>
    public enum AskType
    {
        事假 = 2,
        婚假,
        病假,
        产假,
        丧假,
        工伤假 = 55
    }
    #endregion

    #region 供应商管理

    public enum SupplierSize
    {
        [Description("")]
        nullspace = 0,
        [Description("大型(100人以上)")]
        hg = 1,
        [Description("中型(50-100人)")]
        hblhggsy = 2,
        [Description("小型(10人-50人)")]
        dzdgjtx = 3,
        [Description("微型(10人以下)")]
        bgwjjgy = 4
    }

    public enum MyAttendanceType
    {
        内部考勤 = 113,
        外出考勤 = 114,
        考勤统计 = 115,
        考勤设置 = 116
    }

    /// <summary>
    /// 供应商行业
    /// </summary> 
    public enum SupplierIndustry
    {
        [Description("")]
        nullspace = 0,
        [Description("化工")]
        hg = 1,
        [Description("环保、绿化、公共事业")]
        hblhggsy = 2,
        [Description("电子电工及通讯")]
        dzdgjtx = 3,
        [Description("办公文教及光仪")]
        bgwjjgy = 4,
        [Description("影视、新闻、出版")]
        ysxwcb = 6,
        [Description("电脑、网络、软件")]
        dnwlrj = 7,
        [Description("农林牧渔")]
        nlmy = 8,
        [Description("生活消费")]
        shxf = 9,
        [Description("家电及家居用品")]
        jdjjjyp = 10,
        [Description("交通运输、物流")]
        jtyswl = 11,
        [Description("文化教育、培训")]
        whjypx = 12,
        [Description("玩具及儿童用品")]
        wjjetyp = 14,
        [Description("科研、设计、监测")]
        kysjjc = 15,
        [Description("礼品、美术、工艺品")]
        lpmsgy = 16,
        [Description("医疗、医药、保健")]
        ylyybj = 17,
        [Description("金融、保险、投资")]
        jrbxtz = 18,
        [Description("机械、机电、设备")]
        jxjdsb = 19,
        [Description("旅游、运动、休闲")]
        lyydxx = 20,
        [Description("政府及社会团体")]
        zfjshtt = 21,
        [Description("工业制品及工业用品")]
        gyzpjgyyp = 22,
        [Description("包装、印刷、纸品")]
        bzyszp = 23,
        [Description("商业机构")]
        syjg = 24,
        [Description("纺织、皮革、印染")]
        fzpgry = 25,
        [Description("安全、保安")]
        zyfw = 26,
        [Description("专业服务")]
        nykczj = 27,
        [Description("能源、矿产、冶金")]
        nykcyj = 28,
        [Description("服装、服饰")]
        fzfs = 29,
        [Description("生活服务")]
        shfw = 30,
        [Description("宠物及用品")]
        jzfdc = 32,
        [Description("建筑、装饰、房地产")]
        spylyj = 33,
        [Description("食品、饮料、烟酒")]
        spylyj22 = 34
    }
    #endregion
}
