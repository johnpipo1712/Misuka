using System;

namespace Misuka.Domain.Security
{
  public static class PermissionUtils
  {
    /// <returns>true/false</returns>
    public static bool IsMemberOfRole(Guid userId, Guid roleId)
    {
      //DbParameter pUserIdentityId = DBParameterFactory.CreateParameter("UserIdentityId", DbType.Guid);
      //DbParameter pRoleId = DBParameterFactory.CreateParameter("RoleId", DbType.Guid);
      //pUserIdentityId.Value = userIdentityId;
      //pRoleId.Value = roleId;

      //DataTable table = ADO.ExecuteDataTable(CommandType.StoredProcedure, "fwIsMemberOfRole", pUserIdentityId, pRoleId);
      //return (int)table.Rows[0][0] == 0 ? false : true;
      return true;
    }
  }
}
