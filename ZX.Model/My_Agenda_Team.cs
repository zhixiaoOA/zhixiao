using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_Agenda_Team
    /// <summary>
    /// My_Agenda_Team
    /// </summary>
    [DataFieldAttribute("My_Agenda_Team")]
    public class My_Agenda_Team : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_My_AgendaId")]		public Int64? FK_My_AgendaId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ExpectHours")]		public Int32? ExpectHours		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ConsumTime")]		public Int32? ConsumTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("TheTime")]		public Int32? TheTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int64? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int64? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
    }
    #endregion
}
