USE [ShanDongOA]
GO
/****** Object:  Table [dbo].[Temporary_Task_Team]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temporary_Task_Team](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_TemporaryTaskId] [int] NULL,
	[FK_UserId] [int] NULL,
	[ExpectHours] [int] NULL,
	[ConsumTime] [int] NULL,
	[TheTime] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_TEMPORARY_TASK_TEMP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Temporary_Task_Log]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temporary_Task_Log](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_TemporaryTaskId] [int] NULL,
	[Remark] [varchar](1000) NULL,
	[StatusId] [int] NULL,
	[StatusName] [varchar](25) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[ActualStart] [datetime] NULL,
 CONSTRAINT [PK_TEMPORARY_TASK_LOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Temporary_Task_Block]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temporary_Task_Block](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BName] [nvarchar](100) NULL,
	[BWidth] [int] NULL,
	[BColor] [varchar](30) NULL,
	[TType] [int] NULL,
	[RowCounts] [int] NULL,
	[BOrderBy] [varchar](50) NULL,
	[TState] [varchar](255) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_TRMPORARY_TASK_BLOCK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Temporary_Task]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temporary_Task](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TName] [varchar](200) NULL,
	[Assigned] [varchar](200) NULL,
	[Priority] [int] NULL,
	[TExpected] [int] NULL,
	[AsTime] [datetime] NULL,
	[TDesc] [varchar](1000) NULL,
	[TState] [int] NULL,
	[TCC] [varchar](2000) NULL,
	[Attach] [varchar](500) NULL,
	[TRemark] [varchar](255) NULL,
	[TSuccess] [varchar](100) NULL,
	[TSucTime] [datetime] NULL,
	[TCancel] [varchar](100) NULL,
	[TCancelTime] [datetime] NULL,
	[TClosed] [varchar](100) NULL,
	[TClosedTime] [datetime] NULL,
	[TClosedWhy] [varchar](500) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[ParentId] [int] NULL,
	[EditCount] [int] NULL,
 CONSTRAINT [PK_TEMPORARY_TASK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_WxParameter]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_WxParameter](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AppId] [varchar](32) NULL,
	[AppSecret] [varchar](64) NULL,
	[MchId] [varchar](32) NULL,
	[Paykey] [varchar](64) NULL,
 CONSTRAINT [PK_Sys_WxParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信AppId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_WxParameter', @level2type=N'COLUMN',@level2name=N'AppId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信AppSecret' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_WxParameter', @level2type=N'COLUMN',@level2name=N'AppSecret'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信商户号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_WxParameter', @level2type=N'COLUMN',@level2name=N'MchId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信商户支付密钥' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_WxParameter', @level2type=N'COLUMN',@level2name=N'Paykey'
GO
/****** Object:  Table [dbo].[Sys_User]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[RealName] [varchar](50) NULL,
	[USex] [varchar](10) NULL,
	[Fk_DeptId] [int] NULL,
	[Fk_RoleId] [int] NULL,
	[Pwd] [varchar](50) NULL,
	[UEmail] [varchar](50) NULL,
	[UPhone] [varchar](50) NULL,
	[UQQ] [varchar](50) NULL,
	[UAddress] [varchar](500) NULL,
	[IPAddress] [varchar](50) NULL,
	[IsDel] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[OpenId] [varchar](100) NULL,
	[FK_CompanyPositionId] [bigint] NULL,
	[USgin] [varchar](100) NULL,
 CONSTRAINT [PK_SYS_USER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'FK_CompanyPositionId'
GO
/****** Object:  Table [dbo].[Sys_Role_User]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Role_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_RoleId] [int] NULL,
	[FK_UserId] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_ROLE_USER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_Role_Menu]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Role_Menu](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_RoleId] [int] NULL,
	[FK_MenuButtonId] [int] NULL,
	[TypeId] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_ROLE_MENU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_Role_Application]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Role_Application](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_RoleId] [int] NULL,
	[FK_ApplicationId] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_ROLE_APPLICATION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_Role]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RName] [varchar](50) NULL,
	[RDesc] [varchar](2000) NULL,
	[RSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_ROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'相当' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Role'
GO
/****** Object:  Table [dbo].[Sys_Parameters]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Parameters](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[CompanyDesc] [varchar](1) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_PARAMETERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_Menu]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Menu](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MName] [varchar](50) NULL,
	[MParentId] [int] NULL,
	[MIcon] [varchar](255) NULL,
	[Mesc] [varchar](500) NULL,
	[MUrl] [varchar](200) NULL,
	[MSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[BlockSign] [varchar](50) NULL,
 CONSTRAINT [PK_SYS_MENU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'英文含义' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'BlockSign'
GO
/****** Object:  Table [dbo].[Sys_Dept]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Dept](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DName] [varchar](50) NULL,
	[DParentId] [int] NULL,
	[FK_UserId] [int] NULL,
	[DDesc] [varchar](500) NULL,
	[DSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_DEPT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_Button]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Button](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BName] [varchar](50) NULL,
	[FK_MenuId] [int] NULL,
	[BDesc] [varchar](500) NULL,
	[BSort] [int] NULL,
	[BType] [int] NULL,
	[BGroup] [int] NULL,
	[BOptionFun] [varchar](50) NULL,
	[BIsUse] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_BUTTON] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_Block]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Block](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_Menu_BlockId] [int] NULL,
	[BName] [nvarchar](100) NULL,
	[BWidth] [int] NULL,
	[BColor] [varchar](30) NULL,
	[BType] [int] NULL,
	[RowCounts] [int] NULL,
	[BSort] [int] NULL,
	[BState] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_SYS_BLOCK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_Application]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Application](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AName] [varchar](50) NULL,
	[ANameAs] [varchar](50) NULL,
	[ACode] [varchar](50) NULL,
	[AUrl] [varchar](200) NULL,
	[IsLeft] [int] NULL,
	[AIcon] [varchar](50) NULL,
	[ASort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_APPLICATION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier_Type]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier_Type](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TName] [varchar](200) NULL,
	[ParentId] [int] NULL,
	[TSort] [int] NULL,
	[TRemark] [varchar](255) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_SUPPLIER_TYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：公司公告 10：新闻' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier_Type', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier_Type', @level2type=N'COLUMN',@level2name=N'TRemark'
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SName] [varchar](200) NULL,
	[SCode] [varchar](100) NULL,
	[SRemark] [varchar](1000) NULL,
	[SAddress] [varchar](500) NULL,
	[SLinkName] [varchar](100) NULL,
	[SPhone] [varchar](50) NULL,
	[STel] [varchar](50) NULL,
	[SEMail] [varchar](100) NULL,
	[SOpenName] [varchar](100) NULL,
	[SOpenBank] [varchar](100) NULL,
	[SOpenBankAccount] [varchar](100) NULL,
	[SOpenMake] [varchar](100) NULL,
	[STaxpayer] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[SSize] [varchar](100) NULL,
	[SIndustry] [varchar](100) NULL,
	[SQQ] [varchar](50) NULL,
	[SWeiXin] [varchar](50) NULL,
	[Qualified] [decimal](18, 1) NULL,
 CONSTRAINT [PK_SUPPLIER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：公司公告 10：新闻' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'SCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'SRemark'
GO
/****** Object:  Table [dbo].[Project_Team]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project_Team](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_ProjectId] [int] NULL,
	[FK_UserId] [int] NULL,
	[Permissions] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_PROJECT_TEAM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：默认 10：管理员 20：受限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Team', @level2type=N'COLUMN',@level2name=N'Permissions'
GO
/****** Object:  Table [dbo].[Project_Task_Team]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project_Task_Team](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_TaskId] [int] NULL,
	[FK_UserId] [int] NULL,
	[ExpectHours] [int] NULL,
	[ConsumTime] [int] NULL,
	[TheTime] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_PROJECT_TASK_TEAM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Task_Team', @level2type=N'COLUMN',@level2name=N'ExpectHours'
GO
/****** Object:  Table [dbo].[Project_Task]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project_Task](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_ProjectId] [int] NULL,
	[TName] [varchar](200) NULL,
	[Assigned] [varchar](200) NULL,
	[Priority] [int] NULL,
	[TExpected] [int] NULL,
	[AsTime] [datetime] NULL,
	[PDesc] [nvarchar](1000) NULL,
	[TState] [int] NULL,
	[TCC] [varchar](2000) NULL,
	[Attach] [varchar](500) NULL,
	[TRemark] [varchar](255) NULL,
	[TSuccess] [varchar](100) NULL,
	[TSucTime] [datetime] NULL,
	[TCancel] [varchar](100) NULL,
	[TCancelTime] [datetime] NULL,
	[TClosed] [varchar](100) NULL,
	[TClosedTime] [datetime] NULL,
	[TClosedWhy] [varchar](1000) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[ParentId] [int] NULL,
	[EditCount] [int] NULL,
 CONSTRAINT [PK_PROJECT_TASK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project_Log]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project_Log](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_TaskId] [int] NULL,
	[FK_ProjectId] [int] NULL,
	[Remark] [varchar](1000) NULL,
	[StatusId] [int] NULL,
	[StatusName] [varchar](20) NULL,
	[ActualStart] [datetime] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_PROJECT_LOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Log', @level2type=N'COLUMN',@level2name=N'Remark'
GO
/****** Object:  Table [dbo].[Project_Block]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project_Block](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BType] [int] NULL,
	[BName] [nvarchar](100) NULL,
	[BWidth] [int] NULL,
	[BColor] [varchar](30) NULL,
	[PTType] [int] NULL,
	[RowCounts] [int] NULL,
	[BOrderBy] [varchar](50) NULL,
	[PTState] [varchar](255) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_PROJECT_BLOCK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PName] [varchar](100) NULL,
	[HeadUser] [varchar](200) NULL,
	[PStatus] [varchar](10) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[PDesc] [varchar](max) NULL,
	[Visitors] [varchar](200) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_PROJECT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project', @level2type=N'COLUMN',@level2name=N'PStatus'
GO
/****** Object:  Table [dbo].[News]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DName] [varchar](200) NULL,
	[DType] [int] NULL,
	[DContent] [nvarchar](max) NULL,
	[DImageUrl] [varchar](500) NULL,
	[DSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_NEWS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：公司公告 10：新闻' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'News', @level2type=N'COLUMN',@level2name=N'DType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'News', @level2type=N'COLUMN',@level2name=N'DContent'
GO
/****** Object:  Table [dbo].[MySealUseDetail]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[MySealUseDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_MySealUseId] [bigint] NULL,
	[ZlName] [nvarchar](64) NULL,
	[ZlCount] [int] NULL,
	[HtNo] [varchar](64) NULL,
	[YJianCount] [int] NULL,
	[FYJianCount] [int] NULL,
 CONSTRAINT [PK_MySealUseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MySealUse]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MySealUse](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[YinzName] [nvarchar](64) NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MySealUse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MySealOutDetail]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MySealOutDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_MySealOutId] [bigint] NULL,
	[ZlName] [nvarchar](64) NULL,
	[ZlCount] [int] NULL,
	[HtNo] [varchar](64) NULL,
 CONSTRAINT [PK_MySealOutDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MySealOut]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MySealOut](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[YinzName] [nvarchar](64) NULL,
	[BgName] [nvarchar](64) NULL,
	[ReturnTime] [datetime] NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MySealOut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保管人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MySealOut', @level2type=N'COLUMN',@level2name=N'BgName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返还时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MySealOut', @level2type=N'COLUMN',@level2name=N'ReturnTime'
GO
/****** Object:  Table [dbo].[MyGooodsUse]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyGooodsUse](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_UserId] [bigint] NULL,
	[YanName] [nvarchar](64) NULL,
	[YanCount] [int] NULL,
	[JiuName] [nvarchar](64) NULL,
	[JiuCount] [int] NULL,
	[OtherName] [nvarchar](64) NULL,
	[OtherCount] [int] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[YanUnitName] [varchar](10) NULL,
	[JiuUnitName] [varchar](10) NULL,
	[OrtherUnitName] [varchar](10) NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MyGooodsUse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'烟名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyGooodsUse', @level2type=N'COLUMN',@level2name=N'YanName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'烟数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyGooodsUse', @level2type=N'COLUMN',@level2name=N'YanCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'酒名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyGooodsUse', @level2type=N'COLUMN',@level2name=N'JiuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'酒数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyGooodsUse', @level2type=N'COLUMN',@level2name=N'JiuCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其他名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyGooodsUse', @level2type=N'COLUMN',@level2name=N'OtherName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其他数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyGooodsUse', @level2type=N'COLUMN',@level2name=N'OtherCount'
GO
/****** Object:  Table [dbo].[MyGiftBuyDetail]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyGiftBuyDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_MyGiftBuyId] [bigint] NULL,
	[Name] [nvarchar](64) NULL,
	[Brand] [nvarchar](64) NULL,
	[Count] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[BuyDeptName] [nvarchar](64) NULL,
	[ZChanType] [nvarchar](64) NULL,
	[GifUnitName] [varchar](10) NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MyGiftBuyDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MyGiftBuy]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyGiftBuy](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ADesc] [text] NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MyGiftBuy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MyEntertain]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyEntertain](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ESxiang] [nvarchar](32) NULL,
	[ELevel] [nvarchar](32) NULL,
	[ADesc] [text] NULL,
	[TotalMoney] [decimal](18, 1) NULL,
	[KeCompanyName] [nvarchar](128) NULL,
	[KeCount] [int] NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[EntertainUnitName] [varchar](10) NULL,
	[AccompanyDinner] [varchar](200) NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MyEntertain] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'招待事项' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyEntertain', @level2type=N'COLUMN',@level2name=N'ESxiang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'招待级别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyEntertain', @level2type=N'COLUMN',@level2name=N'ELevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyEntertain', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'招待费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyEntertain', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客人单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyEntertain', @level2type=N'COLUMN',@level2name=N'KeCompanyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客人人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyEntertain', @level2type=N'COLUMN',@level2name=N'KeCount'
GO
/****** Object:  Table [dbo].[MyCostBill]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyCostBill](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ADesc] [nvarchar](512) NULL,
	[TotalMoney] [decimal](18, 2) NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
	[Advance] [decimal](18, 2) NULL,
	[Retrieve] [decimal](18, 2) NULL,
	[ReimburseUser] [varchar](512) NULL,
	[ReceiveUser] [varchar](512) NULL,
	[FilePath] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MyCost]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyCost](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[YongTu] [varchar](255) NULL,
	[Ht_TotalMoney] [decimal](18, 1) NULL,
	[YiZhiFu] [decimal](18, 1) NULL,
	[This_Money] [decimal](18, 1) NULL,
	[KaiHuHang] [varchar](255) NULL,
	[Number] [varchar](255) NULL,
	[ShouKuanDanWei] [varchar](255) NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
	[Payment] [varchar](50) NULL,
 CONSTRAINT [PK_MyCost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MyClock]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyClock](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OneDate] [date] NULL,
	[OneDesc] [nvarchar](256) NULL,
	[TwoDate] [date] NULL,
	[TwoDesc] [nvarchar](256) NULL,
	[ThreeDate] [date] NULL,
	[ThreeDesc] [nvarchar](256) NULL,
	[OutThreeDate] [date] NULL,
	[OutThreeDesc] [nvarchar](256) NULL,
	[NoClockCount] [int] NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MyClock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MyCartPublicDetail]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyCartPublicDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_MyCartPublicId] [bigint] NULL,
	[CDesc] [nvarchar](256) NULL,
	[StartAddr] [nvarchar](64) NULL,
	[Licheng] [decimal](18, 1) NULL,
	[CDate] [date] NULL,
	[IsLfei] [int] NULL,
	[LichengUnitName] [varchar](10) NULL,
 CONSTRAINT [PK_MyCartPublicDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyCartPublicDetail', @level2type=N'COLUMN',@level2name=N'CDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出差地点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyCartPublicDetail', @level2type=N'COLUMN',@level2name=N'StartAddr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计里程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyCartPublicDetail', @level2type=N'COLUMN',@level2name=N'Licheng'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出发日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyCartPublicDetail', @level2type=N'COLUMN',@level2name=N'CDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有过路费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyCartPublicDetail', @level2type=N'COLUMN',@level2name=N'IsLfei'
GO
/****** Object:  Table [dbo].[MyCartPublic]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyCartPublic](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_MyCartPublic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MyAgreement]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyAgreement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[HtNo] [varchar](64) NULL,
	[HtName] [nvarchar](512) NULL,
	[HtFile] [varchar](256) NULL,
	[JiaName] [nvarchar](128) NULL,
	[YiName] [nvarchar](128) NULL,
	[TotalMoney] [decimal](18, 1) NULL,
	[YinzName] [nvarchar](128) NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[Description] [text] NULL,
	[Party] [varchar](2000) NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MyAgreement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'HtNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'HtName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同附件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'HtFile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'甲方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'JiaName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乙方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'YiName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用印章名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MyAgreement', @level2type=N'COLUMN',@level2name=N'YinzName'
GO
/****** Object:  Table [dbo].[My_Work]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Work](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AType] [int] NULL,
	[StartTime] [datetime] NULL,
	[StartHour] [varchar](100) NULL,
	[EndTime] [datetime] NULL,
	[EndHour] [varchar](100) NULL,
	[ADesc] [varchar](100) NULL,
	[ATotalLength] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
	[FK_ApprovalId] [int] NULL,
 CONSTRAINT [PK_MY_WORK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：工作日加班 10：休息日加班 20：家假日加班 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Work', @level2type=N'COLUMN',@level2name=N'AType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Work', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Work', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_PaidLeave]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_PaidLeave](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_WorkId] [int] NULL,
	[StartTime] [datetime] NULL,
	[StartHour] [varchar](100) NULL,
	[EndTime] [datetime] NULL,
	[EndHour] [varchar](100) NULL,
	[ADesc] [varchar](100) NULL,
	[ATotalLength] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
	[AddWorkDesc] [nvarchar](516) NULL,
 CONSTRAINT [PK_MY_PAIDLEAVE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：工作日加班 10：休息日加班 20：家假日加班 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_PaidLeave', @level2type=N'COLUMN',@level2name=N'FK_WorkId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_PaidLeave', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_PaidLeave', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_Holiday]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Holiday](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[HTypeId] [int] NULL,
	[HName] [varchar](200) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[HDesc] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
 CONSTRAINT [PK_MY_HOLIDAY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：假期 10：补班' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Holiday', @level2type=N'COLUMN',@level2name=N'HTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Holiday', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
/****** Object:  Table [dbo].[My_GoOut]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_GoOut](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BName] [varchar](100) NULL,
	[BKGName] [varchar](100) NULL,
	[StartTime] [datetime] NULL,
	[StartHour] [varchar](100) NULL,
	[EndTime] [datetime] NULL,
	[EndHour] [varchar](100) NULL,
	[ADesc] [varchar](100) NULL,
	[EndCity] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
 CONSTRAINT [PK_MY_GOOUT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_GoOut', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_GoOut', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_Calendar]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Calendar](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[Cdesc] [varchar](255) NULL,
	[AllDay] [varchar](255) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Calendar] [varchar](255) NULL,
	[Data] [varchar](255) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](255) NULL,
	[UpdateAccount] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[My_BusinessTrip]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_BusinessTrip](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Persons] [nvarchar](128) NULL,
	[StartTime] [date] NULL,
	[EndTime] [date] NULL,
	[BLength] [int] NULL,
	[ADesc] [varchar](100) NULL,
	[Luxian] [nvarchar](256) NULL,
	[ApplyMoney] [decimal](18, 1) NULL,
	[Jtgj] [nvarchar](32) NULL,
	[IsDgou] [int] NULL,
	[DgouInfo] [nvarchar](256) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
	[ApplyMoneyUnitName] [varchar](10) NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_MY_BUSINESSTRIP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出差人员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'Persons'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出差时长(天)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'BLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出差事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拟出差路线' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'Luxian'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'ApplyMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拟乘交通工具' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'Jtgj'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要代购车票' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'IsDgou'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代购车票提供个人资料' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BusinessTrip', @level2type=N'COLUMN',@level2name=N'DgouInfo'
GO
/****** Object:  Table [dbo].[My_BaoXiao_Detail]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_BaoXiao_Detail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DDate] [datetime] NULL,
	[FK_BaoXiaoId] [int] NULL,
	[EndHour] [varchar](100) NULL,
	[ADesc] [varchar](100) NULL,
	[BaoXiaoUserId] [varchar](200) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
 CONSTRAINT [PK_MY_BAOXIAO_DETAIL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BaoXiao_Detail', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_BaoXiao]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_BaoXiao](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BName] [varchar](100) NULL,
	[FK_DeptId] [int] NULL,
	[Subject] [varchar](100) NULL,
	[SubjectType] [varchar](100) NULL,
	[Currency] [varchar](50) NULL,
	[EndHour] [varchar](100) NULL,
	[ADesc] [varchar](100) NULL,
	[Attachment] [varchar](200) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
 CONSTRAINT [PK_MY_BAOXIAO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BaoXiao', @level2type=N'COLUMN',@level2name=N'Currency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_BaoXiao', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_AttendanceCount]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_AttendanceCount](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [varchar](50) NULL,
	[URealName] [varchar](50) NULL,
	[LateNo] [varchar](50) NULL,
	[LeaveEarlyNo] [varchar](50) NULL,
	[AskNO] [varchar](50) NULL,
	[AbsenteeismNo] [varchar](50) NULL,
	[ToleranceNo] [varchar](50) NULL,
	[AttendanceOutNo] [varchar](50) NULL,
	[DueDays] [varchar](50) NULL,
	[ActualAttendanceNo] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[My_Attendance]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Attendance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[URealname] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ADesc] [varchar](100) NULL,
	[ANumber] [varchar](20) NULL,
	[ComparisonType] [varchar](20) NULL,
	[CardNo] [varchar](20) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Status] [int] NULL,
	[PmEndTime] [datetime] NULL,
	[PmStartTime] [datetime] NULL,
	[ADate] [varchar](100) NULL,
 CONSTRAINT [PK_MY_ATTENDANCE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Attendance', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Attendance', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_Ask]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Ask](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AType] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ADesc] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[Status] [int] NULL,
	[FK_ApprovalUserId] [int] NULL,
	[FK_ApprovalId] [bigint] NULL,
	[MAttach] [varchar](200) NULL,
 CONSTRAINT [PK_MY_ASK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：事假 10：病假 20：年假 30：调休 40：探亲假 50：婚假 60：产假 70：其它' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Ask', @level2type=N'COLUMN',@level2name=N'AType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Ask', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Ask', @level2type=N'COLUMN',@level2name=N'ADesc'
GO
/****** Object:  Table [dbo].[My_Apply]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[My_Apply](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ADesc] [text] NULL,
	[TotalMoney] [decimal](18, 1) NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[FK_ApprovalId] [bigint] NULL,
 CONSTRAINT [PK_My_Apply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[My_Agenda_Team]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Agenda_Team](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_My_AgendaId] [bigint] NULL,
	[ExpectHours] [int] NULL,
	[ConsumTime] [int] NULL,
	[TheTime] [int] NULL,
	[CreateUserId] [bigint] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_My_Agenda_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[My_Agenda_Log]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Agenda_Log](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_AgendaId] [bigint] NOT NULL,
	[Remark] [varchar](1000) NULL,
	[CreateUserId] [bigint] NULL,
	[CreateTime] [datetime] NULL,
	[AStatus] [varchar](50) NULL,
	[ADoFun] [varchar](50) NULL,
	[CreateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_My_Agenda_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'待办状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda_Log', @level2type=N'COLUMN',@level2name=N'AStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作简述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda_Log', @level2type=N'COLUMN',@level2name=N'ADoFun'
GO
/****** Object:  Table [dbo].[My_Agenda]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[My_Agenda](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ADate] [datetime] NULL,
	[AIsUndetermined] [int] NULL,
	[AType] [int] NULL,
	[APriority] [int] NULL,
	[AName] [nvarchar](50) NULL,
	[AAssigned] [bigint] NULL,
	[ADesc] [nvarchar](1000) NULL,
	[ARemark] [nvarchar](255) NULL,
	[AStatus] [int] NULL,
	[AStartmmhh] [varchar](8) NULL,
	[AEndmmhh] [varchar](8) NULL,
	[AIsNotSet] [int] NULL,
	[AIsPlivate] [int] NULL,
	[AFinishUserId] [bigint] NULL,
	[AFinishTime] [datetime] NULL,
	[ACloseUserId] [bigint] NULL,
	[ACloseTime] [datetime] NULL,
	[ACloseReason] [nvarchar](1000) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [int] NULL,
	[CreateAccount] [varchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateAccount] [varchar](50) NULL,
	[FK_Id] [bigint] NULL,
 CONSTRAINT [PK_My_Agenda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-待定，1-非待定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AIsUndetermined'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 0-自定义，10-项目任务，' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-一般，10-最高，20-较高，30-最低' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'APriority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-未开始，10-进行中，20-已完成，30-已关闭' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'06:30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AStartmmhh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'06:30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AEndmmhh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-设定起止时间，1-暂时不设定起止时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AIsNotSet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-私人事务，1-非私人事务' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'AIsPlivate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外表Id,例如 类型为：项目任务，则此字段存项目任务的id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'My_Agenda', @level2type=N'COLUMN',@level2name=N'FK_Id'
GO
/****** Object:  Table [dbo].[Menu_Block_TypeOrStatus]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu_Block_TypeOrStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_Menu_BlockId] [int] NULL,
	[TSName] [nvarchar](100) NULL,
	[TypeOrStatus] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_MENU_BLOCK_TYPEORSTATUS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Block_Sort]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu_Block_Sort](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_Menu_BlockId] [int] NULL,
	[SName] [nvarchar](100) NULL,
	[SortField] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_MENU_BLOCK_SORT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu_Block]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu_Block](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_MenuId] [int] NULL,
	[MBName] [nvarchar](100) NULL,
	[ToTableName] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_MENU_BLOCK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Member]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OpenId] [varchar](64) NULL,
	[Nickname] [nvarchar](64) NULL,
	[HeadImage] [varchar](1024) NULL,
	[FK_UserId] [bigint] NULL,
	[AddTime] [datetime] NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MapPosition]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MapPosition](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PositionId] [varchar](255) NULL,
	[Title] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
	[Lat] [varchar](255) NULL,
	[Lng] [varchar](255) NULL,
	[AdCode] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[District] [varchar](255) NULL,
	[Province] [varchar](255) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[Cause] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Document_Type]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document_Type](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DName] [varchar](200) NULL,
	[ParentId] [int] NULL,
	[DDescribe] [varchar](100) NULL,
	[DSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[FK_LibraryId] [int] NULL,
 CONSTRAINT [PK_DOCUMENT_TYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Document_Type', @level2type=N'COLUMN',@level2name=N'DDescribe'
GO
/****** Object:  Table [dbo].[Document_Manager]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document_Manager](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_TypeId] [int] NULL,
	[AuthUserId] [varchar](200) NULL,
	[AuthRole] [varchar](200) NULL,
	[DTitle] [varchar](100) NULL,
	[DContent] [nvarchar](max) NULL,
	[DKey] [varchar](200) NULL,
	[DAbs] [varchar](2000) NULL,
	[DUrl] [varchar](500) NULL,
	[Attach] [varchar](100) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[DType] [int] NULL,
	[FK_LibraryId] [int] NULL,
	[Postfix] [nvarchar](200) NULL,
 CONSTRAINT [PK_DOCUMENT_MANAGER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Document_Manager', @level2type=N'COLUMN',@level2name=N'AuthRole'
GO
/****** Object:  Table [dbo].[Document_Library]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document_Library](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DName] [varchar](200) NULL,
	[FK_Id] [int] NULL,
	[DType] [int] NULL,
	[AuthUserAccount] [varchar](200) NULL,
	[AuthRole] [varchar](200) NULL,
	[IsPrivate] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[AuthDept] [varchar](512) NULL,
 CONSTRAINT [PK_DOCUMENT_LIBRARY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：项目文档库 10：自定义文档库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Document_Library', @level2type=N'COLUMN',@level2name=N'DType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Document_Library', @level2type=N'COLUMN',@level2name=N'AuthUserAccount'
GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictionary](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NULL,
	[ParentId] [bigint] NULL,
	[Sort] [int] NULL,
	[AddTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Complaint]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Complaint](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_MemberId] [bigint] NULL,
	[FK_TMemberId] [bigint] NULL,
	[TContent] [nvarchar](512) NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyPosition]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyPosition](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NULL,
	[Sort] [int] NULL,
	[AddTime] [datetime] NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_CompanyPosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyPosition', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyPosition', @level2type=N'COLUMN',@level2name=N'AddTime'
GO
/****** Object:  Table [dbo].[AttendanceTime]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AttendanceTime](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [varchar](50) NULL,
	[ApplicableObject] [varchar](50) NULL,
	[WorkDay] [varchar](50) NULL,
	[AmStartTime] [datetime] NULL,
	[AmEndTime] [datetime] NULL,
	[PmStartTime] [datetime] NULL,
	[PmEndTime] [datetime] NULL,
	[WorkDays] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AssetsUseDetail]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetsUseDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_AssetsUseId] [bigint] NULL,
	[FK_AssetsId] [bigint] NULL,
	[DCount] [int] NULL,
	[NatureOfAssets] [nvarchar](64) NULL,
	[AuthTime] [datetime] NULL,
 CONSTRAINT [PK_AssetsUseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetsUse]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetsUse](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ADesc] [nvarchar](256) NULL,
	[FK_UserId] [bigint] NULL,
	[FK_ApprovalUserId] [bigint] NULL,
	[Status] [int] NULL,
	[AddTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AssetsUse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assets_Type]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Assets_Type](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TName] [varchar](200) NULL,
	[ParentId] [int] NULL,
	[TRemark] [text] NULL,
	[TSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_ASSETS_TYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Assets_Type', @level2type=N'COLUMN',@level2name=N'TRemark'
GO
/****** Object:  Table [dbo].[Assets_Log]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Assets_Log](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_AssetsId] [int] NULL,
	[Remark] [varchar](100) NULL,
	[StatusId] [int] NULL,
	[StatusName] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
 CONSTRAINT [PK_ASSETS_LOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Assets_Log', @level2type=N'COLUMN',@level2name=N'Remark'
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Assets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ACode] [varchar](100) NULL,
	[AName] [varchar](200) NULL,
	[FK_TypeId] [int] NULL,
	[TRemark] [varchar](1) NULL,
	[TSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[QrCodeUrl] [varchar](200) NULL,
	[ImageUrl] [varchar](200) NULL,
	[ANum] [int] NULL,
	[APrice] [decimal](18, 2) NULL,
	[UsePeriod] [int] NULL,
	[UnitName] [varchar](100) NULL,
	[NatureOfAssets] [varchar](100) NULL,
	[IsWhether] [int] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[Degree] [varchar](10) NULL,
	[UsePeriodUnit] [nvarchar](8) NULL,
	[FK_SupplierId] [bigint] NULL,
	[PurchaseDay] [datetime] NULL,
 CONSTRAINT [PK_ASSETS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'TRemark'
GO
/****** Object:  Table [dbo].[Approval_User]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Approval_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_ApprovalId] [bigint] NULL,
	[FK_CompanyPositionId] [bigint] NULL,
	[FlowName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Approval_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Approval_Type]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Approval_Type](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_ApprovalId] [bigint] NULL,
	[AName] [varchar](200) NULL,
	[TRemark] [varchar](1000) NULL,
	[TSort] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[IsSdong] [int] NULL,
 CONSTRAINT [PK_APPROVAL_TYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Approval_Type', @level2type=N'COLUMN',@level2name=N'TRemark'
GO
/****** Object:  Table [dbo].[Approval_Log]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Approval_Log](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_ApplyFlowId] [bigint] NULL,
	[DName] [varchar](200) NULL,
	[LContent] [nvarchar](512) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[FK_TypeId] [bigint] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_APPROVAL_LOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Approval]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Approval](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DName] [varchar](200) NULL,
	[StepId] [int] NULL,
	[AuthUserAccount] [varchar](200) NULL,
	[AuthRole] [varchar](200) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateAccount] [varchar](100) NULL,
	[UpdateAccount] [varchar](100) NULL,
	[IsSdong] [int] NULL,
	[FK_DeptId] [bigint] NULL,
	[FK_TypeId] [bigint] NULL,
 CONSTRAINT [PK_APPROVAL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：有效 10：挂起 20：完成 30：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Approval', @level2type=N'COLUMN',@level2name=N'AuthUserAccount'
GO
/****** Object:  Table [dbo].[ApplyFlow]    Script Date: 10/18/2018 11:36:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplyFlow](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FK_UserId] [bigint] NULL,
	[FK_Approval_TypeId] [bigint] NULL,
	[FK_Approval_UserId] [bigint] NULL,
	[Title] [nvarchar](128) NULL,
	[AContent] [nvarchar](1024) NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AddTime] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ApplyFlow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'FK_UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'FK_Approval_TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'FK_Approval_UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'AContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'BeginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'AddTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:新申请  1:审核中  2:审核通过  3:驳回' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyFlow', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Default [DF_Assets_Log_StatusId]    Script Date: 10/18/2018 11:36:17 ******/
ALTER TABLE [dbo].[Assets_Log] ADD  CONSTRAINT [DF_Assets_Log_StatusId]  DEFAULT ((0)) FOR [StatusId]
GO
/****** Object:  Default [DF_Dictionary_AddTime]    Script Date: 10/18/2018 11:36:17 ******/
ALTER TABLE [dbo].[Dictionary] ADD  CONSTRAINT [DF_Dictionary_AddTime]  DEFAULT (getdate()) FOR [AddTime]
GO

USE [ShanDongOA]
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_TaskList_C]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_TaskList_C]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT,

	
	@conditions NVARCHAR(200)
	
AS

declare @strSQL   varchar(6000)       -- 主语句 

SET @strSQL='SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project_Task] AS A
	WHERE 1=1  '+@conditions+'
) AS DT 
WHERE RowIndex BETWEEN ('+STR(@pageIndex-1)+')*'+STR(@pageSize+1) +'AND'+ STR(@pageIndex*@pageSize)
EXEC (@strSQL)
SELECT @count


--{"在将 nvarchar 值 'SELECT * FROM (\r\n\tSELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex \r\n\tFROM dbo.[Project_Task] AS A\r\n\tWHERE 1=1  AND A.Assigned=1\r\n) AS DT \r\nWHERE RowIndex BETWEEN (' 转换成数据类型 int 时失败。"}
--{"名称 'SELECT * FROM (\r\n\tSELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex \r\n\tFROM dbo.[Project_Task] AS A\r\n\tWHERE 1=1  @conditions\r\n) AS DT \r\nWHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize' 不是有效的标识符。"}
--{"在将 varchar 值 'SELECT * FROM (\r\n\tSELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex \r\n\tFROM dbo.[Project_Task] AS A\r\n\tWHERE 1=1  @conditions\r\n) AS DT \r\nWHERE RowIndex BETWEEN (' 转换成数据类型 int 时失败。"}
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDataPage]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDataPage]
	@tableName VARCHAR(32),
	@where NVARCHAR(256),
	@orderBy VARCHAR(32)='Id',
	@desc VARCHAR(8)='DESC',
	@fields VARCHAR(1024)='*',
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
IF(@orderBy='')SET @orderBy='Id'
IF(@desc='')SET @desc='DESC'
IF(@where='')SET @where='1=1'
IF(@fields='')SET @fields='*'
DECLARE @sql NVARCHAR(512)
SET @sql='SELECT @count=COUNT(1) FROM ['+@tableName+'] WHERE '+@where
EXEC sys.sp_executesql @sql,N'@count INT OUT',@count OUT
SET @sql='SELECT * FROM(SELECT '+@fields+',ROW_NUMBER() OVER(ORDER BY '+@orderBy+' '+@desc+') AS RowIndex FROM ['+@tableName+'] WHERE '+@where+') AS DT WHERE RowIndex BETWEEN '+STR((@pageIndex-1)*@pageSize+1)+' AND '+STR(@pageIndex*@pageSize)
EXEC(@sql)
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_DictionaryList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_DictionaryList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Dictionary] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Dictionary] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_DeptList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_DeptList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Dept] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Dept] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_ButtonList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_ButtonList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Button] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Button] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_BlockList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_BlockList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Block] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Block] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_ApplicationList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_ApplicationList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Application] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Application] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSupplierList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSupplierList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Supplier] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Supplier] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSupplier_TypeList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSupplier_TypeList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Supplier_Type] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Supplier_Type] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProjectTaskListByProjectId]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProjectTaskListByProjectId]
	@projectId INT
AS		
SELECT DT.*,C.PName AS ProjectName FROM (
	SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.[Project_Task] AS A
		WHERE TState!=7
		AND (@projectId=-1 OR @projectId=A.FK_ProjectId) ) AS tampFirst
	LEFT JOIN dbo.Project_Task_Team AS B 
	ON tampFirst.Id=B.FK_TaskId			
) AS DT LEFT JOIN dbo.Project C ON DT.FK_ProjectId=C.Id
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProjectTaskListByParentId]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProjectTaskListByParentId]
	@parentId INT
AS		
SELECT DT.*,C.PName AS ProjectName FROM (
	SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.[Project_Task] AS A
		WHERE TState!=7
		AND (@parentId=-1 OR @parentId=A.ParentId )) AS tampFirst
	LEFT JOIN dbo.Project_Task_Team AS B 
	ON tampFirst.Id=B.FK_TaskId			
) AS DT LEFT JOIN dbo.Project C ON DT.FK_ProjectId=C.Id
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProjectTaskList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProjectTaskList]
	@userId int,
	@status varchar(32),
	@pageIndex INT,
	@pageSize INT,		
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Task] AS A
WHERE TState!=7
		AND A.Assigned=@userId 
and (@status='' or A.TState in(select id from f_strSplit(@status,',')))	
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex
	,StatusName=(case 
		when TState=2 then '未开始' 
		when TState=3 then '进行中' 
		when TState=4 then '已完成' end)
	FROM dbo.[Project_Task] AS A
	WHERE TState!=7
		AND A.Assigned=@userId and (ParentId=0 or ParentId is null)
	and (@status='' or A.TState in(select id from f_strSplit(@status,',')))
)AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProjectList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProjectList]
	@key NVARCHAR(64),
	@userId int,
	@status int,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project] AS A
WHERE (@status=-1 OR A.PStatus=@status)
	AND (@userId=-1 OR (A.CreateUserId=@userId OR A.Visitors LIKE '%'+CONVERT(VARCHAR(4),(SELECT Fk_RoleId FROM dbo.Sys_User WHERE Id=@userId))+'%' OR A.HeadUser=@userId))
	AND a.PStatus<>'30'
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project] AS A
	WHERE (@status=-1 OR A.PStatus=@status)
	AND (@userId=-1 OR (A.CreateUserId=@userId OR A.Visitors LIKE '%'+CONVERT(VARCHAR(4),(SELECT Fk_RoleId FROM dbo.Sys_User WHERE Id=@userId))+'%' OR A.HeadUser=@userId))
	AND a.PStatus<>'30'
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProjectCorrelationByUserIdList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProjectCorrelationByUserIdList]
	@userId int
AS
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project] AS A
	WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.Visitors LIKE '%'+CONVERT(VARCHAR(19),(SELECT Fk_RoleId FROM dbo.Sys_User WHERE Id=@userId))+'%' OR A.HeadUser=@userId))
	AND a.PStatus<>'30'
) AS DT
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_TeamList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_TeamList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Team] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project_Team] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_TaskList_NoChild]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_TaskList_NoChild]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@userId int,
	@tsuccess INT,
	@assigned INT,
	@isMain INT, --(-1——查所有,其它值——仅查主任务)
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Task] AS A
WHERE 	TState!=7
		AND
		(@userId=-1 OR @userId=A.CreateUserId)
		AND
		(@tsuccess=-1 OR @tsuccess=A.TSuccess)
		AND
		(@assigned=-1 OR @assigned=A.Assigned)
		AND
		(@isMain=-1 OR ((A.ParentId IS NULL OR A.ParentId <=0)))
		
SELECT DT.*,C.PName AS ProjectName FROM (
	SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.[Project_Task] AS A
		WHERE TState!=7
		AND
				(@userId=-1 OR @userId=A.CreateUserId)
				AND
				(@tsuccess=-1 OR @tsuccess=A.TSuccess)
				AND
				(@assigned=-1 OR @assigned=A.Assigned)
				AND
				(@isMain=-1 OR (A.ParentId IS NULL OR A.ParentId <=0))) tampFirst 
	LEFT JOIN dbo.Project_Task_Team AS B 
	ON tampFirst.Id=B.FK_TaskId			
) AS DT LEFT JOIN dbo.Project C ON DT.FK_ProjectId=C.Id
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetCompanyPositionList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetCompanyPositionList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[CompanyPosition] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[CompanyPosition] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAuthApplyCount]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAuthApplyCount]
	@userId bigint
AS
declare @cid bigint
declare @applyCount int,@askCount int,@businessTripCount int,
@workCount int,@agreementCount int,@cartPublicCount int,
@clockCount int,@entertainCount int,@giftBuyCount int,
@gooodsUseCount int,@sealOutCount int,@sealUseCount int,@MyCostCount int
select @cid=FK_CompanyPositionId from Sys_User where id=@userId
--获取申请单未审核数量
select @applyCount=COUNT(1) from My_Apply as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取请假单未审核数量
select @askCount=COUNT(1) from My_Ask as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取出差单未审核数量
select @businessTripCount=COUNT(1) from My_BusinessTrip as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取加班单未审核数量
select @workCount=COUNT(1) from My_Work as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取合同单未审核数量
select @agreementCount=COUNT(1) from MyAgreement as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取私车公用单未审核数量
select @cartPublicCount=COUNT(1) from MyCartPublic as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取未打卡证明单未审核数量
select @clockCount=COUNT(1) from MyClock as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取招待申请单未审核数量
select @entertainCount=COUNT(1) from MyEntertain as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取礼品购置单未审核数量
select @giftBuyCount=COUNT(1) from MyGiftBuy as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取所需物品领用单未审核数量
select @gooodsUseCount=COUNT(1) from MyGooodsUse as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取印章外带单未审核数量
select @sealOutCount=COUNT(1) from MySealOut as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id
where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
--获取印章使用单未审核数量
select @sealUseCount=COUNT(1) from MySealUse as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id

--获取费用申请单未审核数量
select @MyCostCount=COUNT(1) from MyCost as A
left join Approval_User as B on A.FK_ApprovalUserId=B.Id

where ([Status]=0 or [Status]=1) and B.FK_CompanyPositionId=@cid
select ApplyCount=@applyCount,AskCount=@askCount,BusinessTripCount=@businessTripCount,
WorkCount=@workCount,AgreementCount=@agreementCount,CartPublicCount=@cartPublicCount,
ClockCount=@clockCount,EntertainCount=@entertainCount,GiftBuyCount=@giftBuyCount,
GooodsUseCount=@gooodsUseCount,SealOutCount=@sealOutCount,SealUseCount=@sealUseCount,
MyCostCount=@MyCostCount
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAssetsUseList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAssetsUseList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[AssetsUse] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.ADesc like '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT * FROM (
	SELECT A.*,B.FlowName,C.RealName AS ApplyUserName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[AssetsUse] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.ADesc like '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAssetsUseDetailList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAssetsUseDetailList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[AssetsUseDetail] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[AssetsUseDetail] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAssetsList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAssetsList]
	@code NVARCHAR(64),
	@name NVARCHAR(64),
	@mid INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Assets] AS A
LEFT JOIN Assets_Type t ON a.FK_TypeId=t.Id
WHERE (@code='' OR a.ACode LIKE '%'+@code+'%') 
AND (@name='' OR a.AName LIKE '%'+@name+'%') 
AND(a.FK_TypeId=@mid OR @mid=-1)
SELECT * FROM (
	SELECT A.*,t.TName,SupplierName=B.SName,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex FROM dbo.[Assets] AS A
	LEFT JOIN Assets_Type t ON a.FK_TypeId=t.Id
	LEFT JOIN dbo.Supplier AS B ON A.FK_SupplierId=B.Id
	WHERE (@code='' OR a.ACode LIKE '%'+@code+'%') 
	AND (@name='' OR a.AName LIKE '%'+@name+'%') 
	AND(a.FK_TypeId=@mid OR @mid=-1)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAssets_TypeList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAssets_TypeList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Assets_Type] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Assets_Type] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAssets_LogListNotPage]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAssets_LogListNotPage]
	@key NVARCHAR(64),
	@createAccount NVARCHAR(64)
AS
SELECT *,StrCreateTime=CONVERT(VARCHAR(20),CreateTime,23),StrUpdateTime=CONVERT(VARCHAR(20),UpdateTime,23) FROM (
	SELECT ROW_NUMBER() OVER(ORDER BY t.Id DESC) AS RowIndex ,t.*,a.ACode,a.AName FROM [Assets_Log] t
    LEFT JOIN  dbo.Assets a ON t.FK_AssetsId=a.Id
	WHERE (@key='' OR a.AName LIKE '%'+@key+'%') AND (@createAccount='' OR a.CreateAccount LIKE '%'+@createAccount+'%')
) AS DT
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAssets_LogList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAssets_LogList]
	@key NVARCHAR(64),
	@createAccount NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM (
	SELECT t.*,a.ACode,a.AName FROM [Assets_Log] t
    LEFT JOIN  dbo.Assets a ON t.FK_AssetsId=a.Id
	WHERE (@key='' OR a.AName LIKE '%'+@key+'%') AND (@createAccount='' OR a.CreateAccount LIKE '%'+@createAccount+'%')
) AS A
WHERE 1=1
SELECT * FROM (
	SELECT ROW_NUMBER() OVER(ORDER BY t.Id DESC) AS RowIndex ,t.*,a.ACode,a.AName FROM [Assets_Log] t
    LEFT JOIN  dbo.Assets a ON t.FK_AssetsId=a.Id
	WHERE (@key='' OR a.AName LIKE '%'+@key+'%') AND (@createAccount='' OR a.CreateAccount LIKE '%'+@createAccount+'%')
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetApprovalStatisticalList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetApprovalStatisticalList]
    @startTime VARCHAR(20),
	@endTime VARCHAR(20),
	@realname VARCHAR(50),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.Sys_User AS A
WHERE @realname='' OR A.RealName LIKE '%'+@realname+'%' 
SELECT * FROM (
	SELECT tmp.*,ROW_NUMBER() OVER(ORDER BY tmp.Id DESC) AS RowIndex 
	FROM (
			SELECT a.Id,a.realname
				,My_Workcount=(select count(1) from [dbo].My_Work as b  where (@startTime='' OR b.CreateTime>=@startTime) and (@endTime='' OR b.CreateTime<=@endTime) and a.id=b.[CreateUserId] AND Status=2)
				,My_BusinessTripcount=(select count(1) from [dbo].[My_BusinessTrip] as b    where (@startTime='' OR b.CreateTime>=@startTime) and (@endTime='' OR b.CreateTime<=@endTime) and a.id=b.[CreateUserId] AND Status=2)
				,My_Applycount=(select count(1) from [dbo].My_Apply as b  where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,MyGooodsUsecount=(select count(1) from [dbo].MyGooodsUse as b	  where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,MyGiftBuycount=(select count(1) from [dbo].MyGiftBuy as b	 where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,MyAgreementcount=(select count(1) from [dbo].MyAgreement as b where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,MyCartPubliccount=(select count(1) from [dbo].MyCartPublic as b where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,MyClockcount=(select count(1) from [dbo].MyClock as b where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)       
				,MySealUsecount=(select count(1) from [dbo].MySealUse as b	where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,MySealOutcount=(select count(1) from [dbo].MySealOut as b	 where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
				,My_Askcount=(select count(1) from [dbo].My_Ask as b where (@startTime='' OR b.CreateTime>=@startTime) and (@endTime='' OR b.CreateTime<=@endTime) and a.id=b.CreateUserId AND Status=2)
				,MyEntertaincount=(select count(1) from [dbo].MyEntertain as b	 where (@startTime='' OR b.AddTime>=@startTime) and (@endTime='' OR b.AddTime<=@endTime) and a.id=b.FK_UserId AND Status=2)
			from sys_user as a WHERE @realname='' OR A.RealName LIKE '%'+@realname+'%' 
	) AS tmp
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetApprovalList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetApprovalList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Approval] AS A
LEFT JOIN dbo.Sys_Dept AS B ON A.FK_DeptId=B.Id
WHERE (@key='' OR A.DName LIKE '%'+@key+'%' OR B.DName LIKE '%'+@key+'%')
SELECT * FROM (
	SELECT A.*,ISNULL(B.DName,'所有部门') AS DeptName,C.Name AS TypeName,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Approval] AS A
	LEFT JOIN dbo.Sys_Dept AS B ON A.FK_DeptId=B.Id
	LEFT JOIN dbo.Dictionary AS C ON A.FK_TypeId=C.Id AND C.ParentId=35
	WHERE (@key='' OR A.DName LIKE '%'+@key+'%' OR B.DName LIKE '%'+@key+'%')
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetApproval_UserList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetApproval_UserList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Approval_User] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Approval_User] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetApproval_TypeList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetApproval_TypeList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Approval_Type] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,B.DName,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Approval_Type] AS A
	LEFT JOIN dbo.Approval AS B ON A.FK_ApprovalId=B.Id
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetApproval_LogList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetApproval_LogList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Approval_Log] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Approval_Log] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetApplyFlowList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetApplyFlowList]
	@typeId BIGINT,
	@name NVARCHAR(32),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[ApplyFlow] AS A
LEFT JOIN dbo.Sys_User AS B ON A.FK_UserId=B.Id
WHERE A.FK_Approval_TypeId=@typeId
AND (@name='' OR A.Title LIKE '%'+@name+'%')
AND (@beginTime='' OR A.AddTime>=@beginTime)
AND (@endTime='' OR A.AddTime<=@endTime)
AND (@status=-1 OR A.[Status]=@status)
SELECT * FROM (
	SELECT A.*,UserName=B.RealName,CurrAuthName=C.FlowName
	,StatusName=(CASE WHEN A.[Status]=0 THEN '新申请' WHEN A.[Status]=1 THEN '审核中' WHEN A.[Status]=2 THEN '已审核' WHEN A.[Status]=3 THEN '已驳回' END)	
	,ROW_NUMBER() OVER(ORDER BY A.Id,A.[Status]) AS RowIndex 
	FROM dbo.[ApplyFlow] AS A
	LEFT JOIN dbo.Sys_User AS B ON A.FK_UserId=B.Id
	LEFT JOIN dbo.Approval_User AS C ON A.FK_Approval_UserId=C.Id
	WHERE A.FK_Approval_TypeId=@typeId
	AND (@name='' OR A.Title LIKE '%'+@name+'%')
	AND (@beginTime='' OR A.AddTime>=@beginTime)
	AND (@endTime='' OR A.AddTime<=@endTime)
	AND (@status=-1 OR A.[Status]=@status)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAllReport]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAllReport]
	@btime varchar(32),
	@etime varchar(32)
as
declare @scount int,@addCount int,@addUCount int,@addACount int,@zcMoney decimal(18,2)
--闭环任务（完成任务）
select @scount=SUM(SCount) from (
	select COUNT(1) as SCount from Temporary_Task where TState=4 and TSucTime>=@btime and TSucTime<=@etime
	union all
	select COUNT(1) as SCount from Project_Task where TState=4 and TSucTime>=@btime and TSucTime<=@etime
) as T
--新增任务
select @addCount=SUM(SCount) from (
	select COUNT(1) as SCount from Temporary_Task where TState=4 and CreateTime>=@btime and CreateTime<=@etime
	union all
	select COUNT(1) as SCount from Project_Task where TState=4 and CreateTime>=@btime and CreateTime<=@etime
) as T
--新增员工
select @addUCount=COUNT(1) from Sys_User where CreateTime>=@btime and CreateTime<=@etime
--新增资产
select @addACount=COUNT(1) from Assets where CreateTime>=@btime and CreateTime<=@etime
--资产开销
select @zcMoney=SUM(ANum*APrice) from Assets where CreateTime>=@btime and CreateTime<=@etime
select SCount=@scount,AddCount=@addCount,AddUCount=@addUCount,AddACount=@addACount,ZcMoney=@zcMoney
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetAllApplyNoticeList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetAllApplyNoticeList]
	@fk_CompanyPositionId INT
AS
SELECT * FROM
(
SELECT '申请单' AS Title,
'/My_Apply/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  my_Apply WHERE [Status] IN(0,1))AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION  ALL
SELECT '请假申请' AS Title,
'/My_Ask/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  My_Ask WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION ALL	
SELECT '出差申请' AS Title,
'/My_BusinessTrip/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  My_BusinessTrip WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
		
UNION ALL	
SELECT '加班及调休' AS Title,
'/My_Work/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  My_Work WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	

UNION ALL	
SELECT '私车临时公用' AS Title,
'/MyCartPublic/AuthIndex?mid=6' AS ApplyAction,
'私车临时公用' AS ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT [Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MyCartPublic WHERE [Status]  IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id		
	
UNION ALL	
SELECT '未打卡证明' AS Title,
'/MyClock/AuthIndex?mid=6' AS ApplyAction,
OneDesc AS ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT OneDesc ,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MyClock WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id		

UNION ALL	
SELECT '招待审批申请' AS Title,
'/MyEntertain/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MyEntertain WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	

UNION ALL	
SELECT '合同协议申请审批' AS Title,
'/MyAgreement/AuthIndex?mid=6' AS ApplyAction,
'合同协议申请审批' AS ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT [Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  dbo.MyAgreement WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	

	
UNION ALL	
SELECT '礼品购置' AS Title,
'/MyGiftBuy/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MyGiftBuy WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION ALL	
SELECT 
'所需物品领用' AS Title,
'/MyGooodsUse/AuthIndex?mid=6' AS ApplyAction,
'所需物品领用' AS ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName 
FROM
(SELECT [Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MyGooodsUse WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id


UNION ALL	
SELECT '印章外带审批' AS Title,
'/MySealOut/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT YinzName AS ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MySealOut WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION ALL	
SELECT '印章使用审批' AS Title,
'/MySealUse/AuthIndex?mid=6' AS ApplyAction,
ADesc,
CurrentState,
AUId,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT YinzName AS ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId FROM  MySealUse WHERE [Status] IN(0,1)) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	) AS DT WHERE (DT.FK_CompanyPositionId=@fk_CompanyPositionId OR @fk_CompanyPositionId=-1)
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_TaskList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_TaskList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@userId int,
	@status INT,
	@tsuccess INT,
	@tcancel INT, 
	@tclosed INT,
	@assigned INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Task] AS A
WHERE TState!=7
		AND
		(@status=-1 OR @status=A.TState)	
		AND
		(
			@userId=-1
			OR @userId=A.CreateUserId 
			OR @tsuccess=A.TSuccess
			OR @tcancel=A.TCancel
			OR @tclosed=A.TClosed
			OR @assigned=A.Assigned	
		)
		
SELECT DT.*,B.PName AS ProjectName FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project_Task] AS A
	WHERE TState!=7
		AND
		(@status=-1 OR @status=A.TState)	
		AND
		(
			@userId=-1
			OR @userId=A.CreateUserId 
			OR @tsuccess=A.TSuccess
			OR @tcancel=A.TCancel
			OR @tclosed=A.TClosed
			OR @assigned=A.Assigned	
		)
		
)AS DT LEFT JOIN dbo.Project B ON DT.FK_ProjectId=B.Id
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_Task_TeamList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_Task_TeamList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Task_Team] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project_Task_Team] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_LogList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_LogList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Log] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project_Log] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetProject_BlockList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetProject_BlockList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Block] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Project_Block] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetPersonalTaskList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetPersonalTaskList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@userId int,
	@tsuccess INT,
	@assigned INT,
	@isMain INT, --(-1——查所有,其它值——仅查主任务)
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Task] AS A
WHERE 	TState!=7
		AND
		(@userId=-1 OR @userId=A.CreateUserId)
		AND
		(@tsuccess=-1 OR @tsuccess=A.TSuccess)
		AND
		(@assigned=-1 OR @assigned=A.Assigned)
		AND
		(@isMain=-1 OR ((A.ParentId IS NULL OR A.ParentId <=0)))
		
SELECT DT.*,C.PName AS ProjectName,C.EndTime AS PEndTime FROM (
	SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.[Project_Task] AS A
		WHERE  TState!=7
				AND
				(@userId=-1 OR @userId=A.CreateUserId)
				AND
				(@tsuccess=-1 OR @tsuccess=A.TSuccess)
				AND
				(@assigned=-1 OR @assigned=A.Assigned)
				AND
				(@isMain=-1 OR (A.ParentId IS NULL OR A.ParentId <=0))) tampFirst 
	LEFT JOIN dbo.Project_Task_Team AS B 
	ON tampFirst.Id=B.FK_TaskId			
) AS DT LEFT JOIN dbo.Project C ON DT.FK_ProjectId=C.Id
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetNewsListByUserId]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetNewsListByUserId]
	@key NVARCHAR(64),
	@typeId int,
	@pageIndex INT,
	@pageSize INT,	
	@userId INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[News] AS A
WHERE (@key='' or A.DName like '%'+@key+'%') and (@typeId=-1 or @typeId=A.DType) AND (@userId=-1 OR @userId=CreateUserId)
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[News] AS A
	WHERE (@key='' or A.DName like '%'+@key+'%') and (@typeId=-1 or @typeId=A.DType) AND (@userId=-1 OR @userId=CreateUserId) 
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetNewsList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetNewsList]
	@key NVARCHAR(64),
	@typeId int,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[News] AS A
WHERE (@key='' or A.DName like '%'+@key+'%') and (@typeId=-1 or @typeId=A.DType)
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[News] AS A
	WHERE (@key='' or A.DName like '%'+@key+'%') and (@typeId=-1 or @typeId=A.DType)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMySealUseList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMySealUseList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MySealUse] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.YinzName LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MySealUse] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.YinzName LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMySealUseDetailList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMySealUseDetailList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[MySealUseDetail] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MySealUseDetail] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMySealUseAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMySealUseAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MySealUse as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=44
	where g.CreateAccount=@userId 
	and (@key='' or a.YinzName like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MySealUse as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=44
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.YinzName like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMySealOutList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMySealOutList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MySealOut] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.YinzName LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId)  FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MySealOut] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.YinzName LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMySealOutDetailList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMySealOutDetailList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[MySealOutDetail] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MySealOutDetail] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMySealOutAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMySealOutAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MySealOut as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=45
	where g.CreateAccount=@userId 
	and (@key='' or a.YinzName like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MySealOut as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=45
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.YinzName like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyGooodsUseList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyGooodsUseList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyGooodsUse] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyGooodsUse] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyGooodsUseAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyGooodsUseAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MyGooodsUse as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=39
	where g.CreateAccount=@userId 
	and (@key='' or a.YanName like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MyGooodsUse as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=39
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.YanName like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyGiftBuyList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyGiftBuyList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyGiftBuy] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,TotalMoney=(SELECT SUM([Count]*Price) FROM [MyGiftBuyDetail] WHERE A.Id=FK_MyGiftBuyId)
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyGiftBuy] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyGiftBuyDetailList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyGiftBuyDetailList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[MyGiftBuyDetail] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyGiftBuyDetail] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyGiftBuyAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyGiftBuyAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MyGiftBuy as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=40
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select  A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MyGiftBuy as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=40
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyEntertainList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyEntertainList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyEntertain] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyEntertain] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyEntertainAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyEntertainAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MyEntertain as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=47
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select  A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MyEntertain as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=47
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyCreateApplyNoticeList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyCreateApplyNoticeList]
	@userId INT
AS
SELECT * FROM
(
SELECT '申请单' AS Title,
'/My_Apply/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,AddTime AS CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,AddTime FROM  My_Apply WHERE FK_UserId =@userId )AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION  ALL
SELECT '请假申请' AS Title,
'/My_Ask/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,CreateTime FROM  My_Ask WHERE CreateUserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION ALL	
SELECT '出差申请' AS Title,
'/My_BusinessTrip/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,CreateTime FROM  My_BusinessTrip WHERE CreateUserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
		
UNION ALL	
SELECT '加班及调休' AS Title,
'/My_Work/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,CreateTime FROM  My_Work WHERE CreateUserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	

UNION ALL	
SELECT '私车临时公用' AS Title,
'/MyCartPublic/Index?mid=8' AS ApplyAction,
'私车临时公用' AS ADesc,
CurrentState,
AUId,
AddTime AS CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT [Status] AS CurrentState ,FK_ApprovalUserId AS AUId,AddTime FROM  MyCartPublic WHERE FK_UserId =@userId ) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id		
	
UNION ALL	
SELECT '未打卡证明' AS Title,
'/MyClock/Index?mid=8' AS ApplyAction,
OneDesc AS ADesc,
CurrentState,
AUId,
AddTime AS CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT OneDesc ,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,AddTime FROM  MyClock WHERE FK_UserId =@userId ) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id		

UNION ALL	
SELECT '招待审批申请' AS Title,
'/MyEntertain/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,
AddTime AS CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,AddTime FROM  MyEntertain WHERE FK_UserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	
	
UNION ALL	
SELECT '礼品购置' AS Title,
'/MyGiftBuy/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,AddTime AS CreateTime FROM  MyGiftBuy WHERE FK_UserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION ALL	
SELECT 
'所需物品领用' AS Title,
'/MyGooodsUse/Index?mid=8' AS ApplyAction,
'所需物品领用' AS ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName 
FROM
(SELECT [Status] AS CurrentState ,FK_ApprovalUserId AS AUId ,AddTime AS CreateTime FROM  MyGooodsUse WHERE FK_UserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id


UNION ALL	
SELECT 
'合同协议申请' AS Title,
'/MyAgreement/Index?mid=8' AS ApplyAction,
'合同协议申请' AS ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName 
FROM
(SELECT [Status] AS CurrentState ,FK_ApprovalUserId AS AUId ,AddTime AS CreateTime FROM  MyAgreement WHERE FK_UserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id


UNION ALL	
SELECT '印章外带审批' AS Title,
'/MySealOut/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT YinzName AS ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId,AddTime AS CreateTime FROM  MySealOut WHERE FK_UserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id
	
UNION ALL	
SELECT '印章使用审批' AS Title,
'/MySealUse/Index?mid=8' AS ApplyAction,
ADesc,
CurrentState,
AUId,
CreateTime,
Approval_User.FK_ApprovalId,
Approval_User.FK_CompanyPositionId,
Approval_User.FlowName  FROM
(SELECT YinzName AS ADesc,[Status] AS CurrentState ,FK_ApprovalUserId AS AUId, AddTime AS CreateTime FROM  MySealUse WHERE FK_UserId =@userId) AS tempTabel 
LEFT JOIN Approval_User 
	ON tempTabel.AUId=dbo.Approval_User.Id	) AS DT ORDER BY DT.CreateTime DESC
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyCostBillList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyCostBillList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyCostBill] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.ADesc like '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyCostBill] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.ADesc like '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyCostBillAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyCostBillAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MyCostBill as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=87
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MyCostBill as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=87
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyClockList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyClockList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyClock] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.OneDesc LIKE '%'+@key+'%' OR A.TwoDesc LIKE '%'+@key+'%' OR A.ThreeDesc LIKE '%'+@key+'%' OR A.OutThreeDesc LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId)  FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=E.Name,DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyClock] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.OneDesc LIKE '%'+@key+'%' OR A.TwoDesc LIKE '%'+@key+'%' OR A.ThreeDesc LIKE '%'+@key+'%' OR A.OutThreeDesc LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyClockAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyClockAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MyClock as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=43
	where g.CreateAccount=@userId 
	and (@key='' or a.OneDesc like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MyClock as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=43
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 	
	and (@key='' or a.OneDesc like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyCartPublicList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyCartPublicList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyCartPublic] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
--AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId)  FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,CDesc=(SELECT TOP 1 CDesc FROM [MyCartPublicDetail] WHERE A.Id=FK_MyCartPublicId)
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyCartPublic] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	--AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyCartPublicDetailList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyCartPublicDetailList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[MyCartPublicDetail] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyCartPublicDetail] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyApplyList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyApplyList]
	@userId INT,
	@appUserId INT,
	@typeId INT,
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
--IF(@typeId=62)--婚假
--BEGIN
--	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
--	SELECT @count=COUNT(1) FROM dbo.My_Ask AS A
--	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--	WHERE (@userId=-1 or A.CreateUserId=@userId)
--	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--	SELECT * FROM (
--		SELECT Title=A.ADesc,AddTime=A.CreateTime,[Status]
--		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
--		FROM dbo.My_Ask AS A
--		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--		WHERE (@userId=-1 or A.CreateUserId=@userId)
--		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--		AND A.AType=3
--	) AS DT 
--	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
--END
--ELSE IF(@typeId=63)--产假
--BEGIN
--	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
--	SELECT @count=COUNT(1) FROM dbo.My_Ask AS A
--	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--	WHERE (@userId=-1 or A.CreateUserId=@userId)
--	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--	SELECT * FROM (
--		SELECT Title=A.ADesc,AddTime=A.CreateTime,[Status]
--		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
--		FROM dbo.My_Ask AS A
--		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--		WHERE (@userId=-1 or A.CreateUserId=@userId)
--		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--		AND A.AType=5
--	) AS DT 
--	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
--END
--ELSE IF(@typeId=64)--丧假
--BEGIN
--	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
--	SELECT @count=COUNT(1) FROM dbo.My_Ask AS A
--	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--	WHERE (@userId=-1 or A.CreateUserId=@userId)
--	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--	SELECT * FROM (
--		SELECT Title=A.ADesc,AddTime=A.CreateTime,[Status]
--		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
--		FROM dbo.My_Ask AS A
--		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--		WHERE (@userId=-1 or A.CreateUserId=@userId)
--		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--		AND A.AType=6
--	) AS DT 
--	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
--END
--ELSE IF(@typeId=65)--工伤假
--BEGIN
--	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
--	SELECT @count=COUNT(1) FROM dbo.My_Ask AS A
--	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--	WHERE (@userId=-1 or A.CreateUserId=@userId)
--	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--	SELECT * FROM (
--		SELECT Title=A.ADesc,AddTime=A.CreateTime,[Status]
--		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
--		FROM dbo.My_Ask AS A
--		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--		WHERE (@userId=-1 or A.CreateUserId=@userId)
--		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--		AND A.AType=55
--	) AS DT 
--	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
--END
--ELSE IF(@typeId=66)--病假
--BEGIN
--	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
--	SELECT @count=COUNT(1) FROM dbo.My_Ask AS A
--	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--	WHERE (@userId=-1 or A.CreateUserId=@userId)
--	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--	SELECT * FROM (
--		SELECT Title=A.ADesc,AddTime=A.CreateTime,[Status]
--		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
--		FROM dbo.My_Ask AS A
--		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
--		WHERE (@userId=-1 or A.CreateUserId=@userId)
--		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
--		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
--		AND A.AType=4
--	) AS DT 
--	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
--END
--ELSE 
IF(@typeId=46)--事假
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.My_Ask AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.CreateUserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.ADesc,AddTime=A.CreateTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.My_Ask AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.CreateUserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=36)--加班及调休申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.My_Work AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.CreateUserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.ADesc,AddTime=A.CreateTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.My_Work AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.CreateUserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=37)--出差申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.My_BusinessTrip AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.CreateUserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.ADesc,AddTime=A.CreateTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.My_BusinessTrip AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.CreateUserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=38)--申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.My_Apply AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.ADesc,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.My_Apply AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=39)--所需物品领用申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MyGooodsUse AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.YanName+','+A.JiuName+','+A.OtherName,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MyGooodsUse AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=40)--礼品购置申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MyGiftBuy AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.ADesc,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MyGiftBuy AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=41)--合同（协议）审批申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MyAgreement AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.HtName+'('+A.HtNo+')',AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MyAgreement AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=42)--私车临时公用审批申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MyCartPublic AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=(SELECT TOP 1 CDesc FROM [MyCartPublicDetail] WHERE A.Id=FK_MyCartPublicId),AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MyCartPublic AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=43)--未打卡证明申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MyClock AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.OneDesc,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MyClock AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=44)--印章使用审批申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MySealUse AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.YinzName,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MySealUse AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=45)--印章外带审批申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MySealOut AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.YinzName,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MySealOut AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
ELSE IF(@typeId=47)--招待审批申请单
BEGIN
	SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
	SELECT @count=COUNT(1) FROM dbo.MyEntertain AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	SELECT * FROM (
		SELECT A.Id,Title=A.ADesc,AddTime=A.AddTime,[Status]
		,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.MyEntertain AS A
		LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
		WHERE (@userId=-1 or A.FK_UserId=@userId)
		AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
		AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyApplyCount]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyApplyCount]
	@userId bigint
AS
declare @applyCount int,@askCount int,@businessTripCount int,
@workCount int,@agreementCount int,@cartPublicCount int,
@clockCount int,@entertainCount int,@giftBuyCount int,
@gooodsUseCount int,@sealOutCount int,@sealUseCount int,@MyCostCount int
--获取申请单未审核数量
select @applyCount=COUNT(1) from My_Apply as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取请假单未审核数量
select @askCount=COUNT(1) from My_Ask as A
where ([Status]=0 or [Status]=1) and A.CreateUserId=@userId
--获取出差单未审核数量
select @businessTripCount=COUNT(1) from My_BusinessTrip as A
where ([Status]=0 or [Status]=1) and A.CreateUserId=@userId
--获取加班单未审核数量
select @workCount=COUNT(1) from My_Work as A
where ([Status]=0 or [Status]=1) and A.CreateUserId=@userId
--获取合同单未审核数量
select @agreementCount=COUNT(1) from MyAgreement as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取私车公用单未审核数量
select @cartPublicCount=COUNT(1) from MyCartPublic as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取未打卡证明单未审核数量
select @clockCount=COUNT(1) from MyClock as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取招待申请单未审核数量
select @entertainCount=COUNT(1) from MyEntertain as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取礼品购置单未审核数量
select @giftBuyCount=COUNT(1) from MyGiftBuy as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取所需物品领用单未审核数量
select @gooodsUseCount=COUNT(1) from MyGooodsUse as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取印章外带单未审核数量
select @sealOutCount=COUNT(1) from MySealOut as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId
--获取印章使用单未审核数量
select @sealUseCount=COUNT(1) from MySealUse as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId

--获取费用申请单未审核数量
select @MyCostCount=COUNT(1) from MyCost as A
where ([Status]=0 or [Status]=1) and A.FK_UserId=@userId

select ApplyCount=@applyCount,AskCount=@askCount,BusinessTripCount=@businessTripCount,
WorkCount=@workCount,AgreementCount=@agreementCount,CartPublicCount=@cartPublicCount,
ClockCount=@clockCount,EntertainCount=@entertainCount,GiftBuyCount=@giftBuyCount,
GooodsUseCount=@gooodsUseCount,SealOutCount=@sealOutCount,SealUseCount=@sealUseCount,
MyCostCount=@MyCostCount
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyAgreementList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyAgreementList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[MyAgreement] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.HtName LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT * FROM (
	SELECT A.*,B.FlowName,D.RealName AS ApplyUserName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,D.UPhone
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[MyAgreement] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.HtName LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMyAgreementAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMyAgreementAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from MyAgreement as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=41	
	where g.CreateAccount=@userId 
	and (@key='' or a.HtName like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select  A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from MyAgreement as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=41
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 
	and (@key='' or A.HtName like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_WorkList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_WorkList]
	@key NVARCHAR(64),
	@userId BIGINT,
	@appUserId BIGINT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.My_Work AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.CreateUserId=@userId)
AND (@key='' OR A.ADesc like '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or A.StartTime>=@beginTime)
AND (@endTime='' or A.EndTime<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Work AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN dbo.Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.CreateUserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.CreateUserId=@userId)
	AND (@key='' OR A.ADesc like '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or A.StartTime>=@beginTime)
	AND (@endTime='' or A.EndTime<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_WorkAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_WorkAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from My_Work as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=36	
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.CreateTime>=@beginTime)
	and (@endTime='' or a.CreateTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from My_Work as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=36
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.CreateUserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 
	and (@key='' or A.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.CreateTime>=@beginTime)
	and (@endTime='' or A.CreateTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_HolidayList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_HolidayList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Holiday] AS A
WHERE (@key='' or A.HName like '%'+@key+'%')
SELECT * FROM (
	SELECT A.*,b.name as TypeName,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[My_Holiday] AS A
	left join Dictionary as b on a.HTypeId=b.id
	WHERE (@key='' or A.HName like '%'+@key+'%')
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_BusinessTripList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_BusinessTripList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.My_BusinessTrip AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.CreateUserId=@userId)
AND (@key='' OR A.ADesc like '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or A.StartTime>=@beginTime)
AND (@endTime='' or A.EndTime<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_BusinessTrip AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.CreateUserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.CreateUserId=@userId)
	AND (@key='' OR A.ADesc like '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or A.StartTime>=@beginTime)
	AND (@endTime='' or A.EndTime<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_BusinessTripAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_BusinessTripAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from My_BusinessTrip as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=37	
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.CreateTime>=@beginTime)
	and (@endTime='' or a.CreateTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from My_BusinessTrip as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=37
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.CreateUserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 
	and (@key='' or A.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.CreateTime>=@beginTime)
	and (@endTime='' or A.CreateTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_BaoXiao_DetailList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_BaoXiao_DetailList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_BaoXiao_Detail] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[My_BaoXiao_Detail] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AttendanceList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AttendanceList]
	@key NVARCHAR(64),
	@startTime VARCHAR(32),
	@endTime VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT * INTO #DT FROM dbo.My_Attendance WHERE 1=1 AND (@key='' OR My_Attendance.URealname LIKE '%'+@key+'%')
AND (@startTime='' OR My_Attendance.StartTime>=@startTime) AND (@endTime='' OR My_Attendance.StartTime<=@endTime)


SELECT @count=COUNT(1) FROM #DT AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.CreateTime DESC) AS RowIndex 
	FROM #DT AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AttendanceCountList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AttendanceCountList]
	@key VARCHAR(64),
	@startTime VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT * INTO #DT FROM dbo.My_AttendanceCount WHERE 1=1 AND (@key='' OR My_AttendanceCount.URealName LIKE '%'+@key+'%')
AND (@startTime='' OR My_AttendanceCount.Date LIKE '%'+@startTime+'%') 
SELECT T.URealname,T.LateNo,T.LeaveEarlyNo,T.AskNO,T.AbsenteeismNo,T.ToleranceNo,T.AttendanceOutNo,T.DueDays,T.ActualAttendanceNo
INTO #DT2 FROM #DT AS T GROUP BY URealName,LateNo,LeaveEarlyNo,AskNO,AbsenteeismNo,ToleranceNo,AttendanceOutNo,DueDays,ActualAttendanceNo

SELECT @count=COUNT(1) FROM #DT2 AS A
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.URealName DESC) AS RowIndex 
	FROM #DT2 AS A
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AskList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AskList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(20),
	@endTime VARCHAR(20),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[My_Ask] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.CreateUserId=@userId)
AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.CreateTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.CreateTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,G.Name AS TypeName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[My_Ask] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.CreateUserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	LEFT JOIN dbo.Dictionary AS G ON A.AType=G.Id
	WHERE (@userId=-1 or A.CreateUserId=@userId)
	AND (@key='' OR A.ADesc LIKE '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.CreateTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.CreateTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AskAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AskAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from My_Ask as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=46	
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.CreateTime>=@beginTime)
	and (@endTime='' or a.CreateTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select distinct A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from My_Ask as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=46
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.CreateUserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 
	and (@key='' or A.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.CreateTime>=@beginTime)
	and (@endTime='' or A.CreateTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_ApplyList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_ApplyList]
	@key NVARCHAR(64),
	@userId INT,
	@appUserId INT,
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),
	@status VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
DECLARE @companyPositionId BIGINT
SELECT @companyPositionId=FK_CompanyPositionId FROM dbo.Sys_User WHERE Id=@appUserId
SELECT @count=COUNT(1) FROM dbo.[My_Apply] AS A
LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
WHERE (@userId=-1 or A.FK_UserId=@userId)
AND (@key='' OR A.ADesc like '%'+@key+'%')
AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
SELECT *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=dt.FK_CompanyPositionId) FROM (
	SELECT A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[My_Apply] AS A
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	--LEFT JOIN Sys_User AS C ON B.FK_CompanyPositionId=C.FK_CompanyPositionId
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id
	WHERE (@userId=-1 or A.FK_UserId=@userId)
	AND (@key='' OR A.ADesc like '%'+@key+'%')
	AND (@appUserId=-1 or B.FK_CompanyPositionId=@companyPositionId)
	AND (@beginTime='' or CONVERT(VARCHAR(10),A.AddTime,20)>=@beginTime)
	AND (@endTime='' or CONVERT(VARCHAR(10),A.AddTime,20)<=@endTime)
	AND (@status='' or A.[Status] IN(SELECT * FROM f_strSplit(@status,',')))
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_ApplyAppList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_ApplyAppList]
	@key NVARCHAR(64),
	@beginTime VARCHAR(32),
	@endTime VARCHAR(32),	
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
as
select @count=COUNT(1) from (
	select distinct a.Id from My_Apply as a
	inner join Approval_Log as g on a.Id=g.FK_ApplyFlowId and FK_TypeId=38	
	where g.CreateAccount=@userId 
	and (@key='' or a.ADesc like '%'+@key+'%')
	and (@beginTime='' or a.AddTime>=@beginTime)
	and (@endTime='' or a.AddTime <=@endTime)
) as t
select *,ApplyUserName=(SELECT TOP 1 RealName FROM Sys_User WHERE FK_CompanyPositionId=t.FK_CompanyPositionId) 
from (
	select  A.*,B.FlowName,D.RealName,PositionName=ISNULL(E.Name,'普通员工'),DeptName=F.DName,b.FK_CompanyPositionId
	,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	from My_Apply as A
	inner join Approval_Log as G on A.Id=G.FK_ApplyFlowId and FK_TypeId=38
	LEFT JOIN Approval_User AS B ON A.FK_ApprovalUserId=B.Id
	LEFT JOIN dbo.Sys_User AS D ON A.FK_UserId=D.Id
	LEFT JOIN dbo.CompanyPosition AS E ON D.FK_CompanyPositionId=E.Id
	LEFT JOIN dbo.Sys_Dept AS F ON D.Fk_DeptId=F.Id	
	where g.CreateAccount=@userId 
	and (@key='' or A.ADesc like '%'+@key+'%')
	and (@beginTime='' or A.AddTime>=@beginTime)
	and (@endTime='' or A.AddTime <=@endTime)
) as t
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AgendaUnFinishList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AgendaUnFinishList]
	@key NVARCHAR(64),
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda] AS A
WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(A.AStatus=22 OR A.AStatus=23)
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Agenda AS A
	WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(A.AStatus=22 OR A.AStatus=23)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AgendaListByUserId]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AgendaListByUserId]
	@userId INT,
	@status INT
AS
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Agenda AS A
	WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(@status=-1 OR @status=A.AStatus)
) AS DT
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AgendaList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AgendaList]
	@key NVARCHAR(64),
	@userId INT,
	@status INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda] AS A
WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(@status=-1 OR @status=A.AStatus)
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Agenda AS A
	WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(@status=-1 OR @status=A.AStatus)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AgendaAssignedOtherList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AgendaAssignedOtherList]
	@key NVARCHAR(64),
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda] AS A
WHERE 
	 A.AAssigned=@userId AND a.CreateUserId=@userId	
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Agenda AS A
	WHERE 
	 A.AAssigned!=@userId AND a.CreateUserId=@userId	 
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AgendaAssignedMyList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AgendaAssignedMyList]
	@key NVARCHAR(64),
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda] AS A
WHERE 
	 A.AAssigned=@userId	
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Agenda AS A
	WHERE 
	 A.AAssigned=@userId	 
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_AgendaAIsUndeterminedList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_AgendaAIsUndeterminedList]
	@key NVARCHAR(64),
	@userId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda] AS A
WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(A.AIsUndetermined=0)
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.My_Agenda AS A
	WHERE 
	(@userId=-1 OR (A.CreateUserId=@userId OR A.AAssigned=@userId))
	AND(A.AIsUndetermined=0)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_Agenda_TeamList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_Agenda_TeamList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda_Team] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[My_Agenda_Team] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMy_Agenda_LogList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMy_Agenda_LogList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[My_Agenda_Log] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[My_Agenda_Log] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMenu_BlockList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMenu_BlockList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Menu_Block] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Menu_Block] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMenu_Block_TypeOrStatusList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMenu_Block_TypeOrStatusList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Menu_Block_TypeOrStatus] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Menu_Block_TypeOrStatus] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMenu_Block_SortList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMenu_Block_SortList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Menu_Block_Sort] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Menu_Block_Sort] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMemberList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetMemberList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Member] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Member] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetMapPostionList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Proc_GetMapPostionList] 
	@key NVARCHAR(64),
	@startTime VARCHAR(32),
	@endTime VARCHAR(32),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
BEGIN
	SELECT dbo.MapPosition.* INTO #DT FROM dbo.MapPosition join Sys_User u on MapPosition.CreateUserId = u.Id WHERE 1=1 AND (@key='' OR u.RealName LIKE '%'+@key+'%')
	AND (@startTime='' OR MapPosition.CreateTime>=@startTime) AND (@endTime='' OR MapPosition.CreateTime<=@endTime)

	SELECT CONVERT(VARCHAR(100),T.CreateTime,20) AS ADate,T.Title,u.RealName,t.Address  
	INTO #DT2 FROM #DT AS T join dbo.Sys_User u on T.CreateUserId = u.Id

	SELECT @count=COUNT(1) FROM #DT2 AS A
	SELECT * FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.ADate DESC) AS RowIndex 
		FROM #DT2 AS A
	) AS DT 
	WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDocument_TypeList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDocument_TypeList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Document_Type] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Document_Type] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDocument_ManagerList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDocument_ManagerList]
	@key NVARCHAR(64),
	@userId INT,
	@roleId VARCHAR(200),
	@fk_LibraryId INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Document_Manager] AS A
WHERE (@userId=-1 OR A.CreateUserId=@userId 
OR A.AuthUserId LIKE ('%'+convert(varchar,@userId)+'%') OR @userId=-1
OR A.AuthRole LIKE ('%'+@roleId+'%') OR @roleId=-1)
AND (
	A.FK_TypeId IN(SELECT Id FROM dbo.Document_Type WHERE FK_LibraryId=@fk_LibraryId) OR @fk_LibraryId=-1
  )
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Document_Manager] AS A
	WHERE (@userId=-1 OR A.CreateUserId=@userId 
OR A.AuthUserId LIKE ('%'+convert(varchar,@userId)+'%') OR @userId=-1
OR A.AuthRole LIKE ('%'+@roleId+'%') OR @roleId=-1)
AND (
	A.FK_TypeId IN(SELECT Id FROM dbo.Document_Type WHERE FK_LibraryId=@fk_LibraryId) OR @fk_LibraryId=-1
  )
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDocument_LibraryList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDocument_LibraryList]
	@key NVARCHAR(64),
	@dType INT,
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Document_Library] AS A
WHERE  (A.DType=@dType OR @dType=-1)
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Document_Library] AS A
	WHERE (A.DType=@dType OR @dType=-1)
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetDictionaryList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetDictionaryList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Dictionary] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Dictionary] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetUserMenu]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetUserMenu]
 @UserId BIGINT 
 AS 
SELECT B.* FROM dbo.Sys_Role_Menu AS A
LEFT JOIN dbo.Sys_Menu AS B ON A.FK_MenuButtonId=B.Id
LEFT JOIN dbo.Sys_User AS C ON A.FK_RoleId=C.Fk_RoleId
WHERE C.Id=@UserId AND A.TypeId=1
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTemporaryTaskListByParentId]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetTemporaryTaskListByParentId]
	@parentId INT
AS		
SELECT * FROM (
	SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.Temporary_Task AS A
		WHERE @parentId=-1 OR @parentId=A.ParentId ) AS tampFirst
	LEFT JOIN dbo.Temporary_Task_Team AS B 
	ON tampFirst.Id=B.FK_TemporaryTaskId			
) AS DT
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTemporaryTaskList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetTemporaryTaskList]
	@userId int,
	@status varchar(32),
	@pageIndex INT,
	@pageSize INT,		
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Project_Task] AS A
WHERE A.Assigned=@userId 
and (@status='' or A.TState in(select id from f_strSplit(@status,',')))	
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex
	,StatusName=(case 
		when TState=2 then '未开始' 
		when TState=3 then '进行中' 
		when TState=4 then '已完成' end)
	FROM dbo.Temporary_Task AS A
	WHERE A.Assigned=@userId and (ParentId=0 or ParentId is null)
	and (@status='' or A.TState in(select id from f_strSplit(@status,',')))
)AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTemporary_TaskList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetTemporary_TaskList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@userId int,
	@tsuccess INT,
	@assigned INT,
	@isParent INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Temporary_Task] AS A
WHERE 	
			(@userId=A.CreateUserId 
			OR @tsuccess=A.TSuccess
			OR @assigned=A.Assigned)
			AND
			(@isParent=-1 OR (A.ParentId IS NULL OR A.ParentId <=0))
SELECT * FROM (
	SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (
		SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
		FROM dbo.[Temporary_Task] AS A
		WHERE 
			(@userId=A.CreateUserId 
			OR @tsuccess=A.TSuccess
			OR @assigned=A.Assigned)
			AND
			(@isParent=-1 OR (A.ParentId IS NULL OR A.ParentId <=0))) tampFirst 
	LEFT JOIN dbo.Temporary_Task_Team AS B 
	ON tampFirst.Id=B.FK_TemporaryTaskId
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTemporary_Task_TeamList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetTemporary_Task_TeamList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Temporary_Task_Team] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Temporary_Task_Team] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTemporary_Task_LogList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetTemporary_Task_LogList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Temporary_Task_Log] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Temporary_Task_Log] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetTemporary_Task_BlockList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetTemporary_Task_BlockList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Temporary_Task_Block] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Temporary_Task_Block] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_UserList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_UserList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_User] AS A
WHERE 1=1 AND (@key='' OR RealName LIKE '%'+@key+'%')
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_User] AS A
	WHERE 1=1 AND (@key='' OR RealName LIKE '%'+@key+'%')
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_RoleList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_RoleList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Role] AS A
WHERE Id!=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Role] AS A
	WHERE Id!=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_Role_UserList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_Role_UserList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Role_User] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Role_User] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_Role_MenuList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_Role_MenuList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Role_Menu] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Role_Menu] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_Role_ApplicationList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_Role_ApplicationList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Role_Application] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Role_Application] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_ParametersList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_ParametersList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Parameters] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Parameters] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetSys_MenuList]    Script Date: 10/18/2018 11:39:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proc_GetSys_MenuList]
	@key NVARCHAR(64),
	@pageIndex INT,
	@pageSize INT,
	@count INT OUTPUT
AS
SELECT @count=COUNT(1) FROM dbo.[Sys_Menu] AS A
WHERE 1=1
SELECT * FROM (
	SELECT A.*,ROW_NUMBER() OVER(ORDER BY A.Id DESC) AS RowIndex 
	FROM dbo.[Sys_Menu] AS A
	WHERE 1=1
) AS DT 
WHERE RowIndex BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
GO
