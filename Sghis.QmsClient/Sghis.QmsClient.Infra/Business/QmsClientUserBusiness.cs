using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.Core.Entity.QmsClient.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sghis.QmsClient.Infra.Business
{
    public class QmsClientUserBusiness : IQmsClientUserBusiness
    {
        private IQmsClientDataManager _dataManager;

        public QmsClientUserBusiness(IQmsClientDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<QmsClientMenuAccess> GetMenuAccess(int userId, int moduleId)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine(" SELECT a.Id, b.ParentID ParentMenuId, b.FeatureID SubMenuId, b.Name SubMenuName, c.Name ParentMenuName , b.MenuURL Url");
            query.AppendLine(" FROM HISGLOBAL.ACCESS_USERFEATURES a");
            query.AppendLine(" JOIN HISGLOBAL.MENU_ACCESS b ON a.Feature_Id = b.FeatureID");
            query.AppendLine(" JOIN HISGLOBAL.MENU_PARENT c ON b.ParentID = c.MenuID");
            query.AppendLine(" WHERE a.Module_Id = "+ moduleId + " and a.USERID = "+ userId + " AND a.Deleted = 0");

            var menus = _dataManager.MenuAccess.GetAll(query.ToString(), null);

            return menus;
        }
    }
}
