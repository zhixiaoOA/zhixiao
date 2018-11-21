using System;
using ZX.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public interface IBulkHelper<T> where T : BaseModel
    {
        int AddModel(T t);
        int AddModel(List<T> list);
        int MergeModel(List<T> list, string where, string deleteWhere);
        int MergeNotDelModel(List<T> list, string where);
        int DelModel(AiExpConditions<T> exp);
        int DelModel(string where);
        int UpdateModel(T t, AiExpConditions<T> exp);
        int UpdateModel(T t, string fields, string where);
        List<T> GetList();
        List<T> GetList(string strWhere);
        List<T> GetList(AiExpConditions<T> exp);
        T GetModel(AiExpConditions<T> exp);
        bool CheckModel(AiExpConditions<T> exp);
        object GetCount(AiExpConditions<T> exp, string countName);
        object GetSum(string sumName, AiExpConditions<T> exp);
        object GetMax(string maxName, AiExpConditions<T> exp);
        List<T> GetTopList(int top, AiExpConditions<T> exp);
    }
}
