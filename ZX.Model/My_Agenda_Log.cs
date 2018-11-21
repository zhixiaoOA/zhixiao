using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_Agenda_Log
    /// <summary>
    /// My_Agenda_Log
    /// </summary>
    [DataFieldAttribute("My_Agenda_Log")]
    public class My_Agenda_Log : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_AgendaId")]		public Int64? FK_AgendaId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Remark")]		public string Remark		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int64? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 待办状态描述		/// </summary>		[DataFieldAttribute("AStatus")]		public string AStatus		{			get;			set;		}		/// <summary>		/// 操作简述		/// </summary>		[DataFieldAttribute("ADoFun")]		public string ADoFun		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}
    }
    #endregion
}
