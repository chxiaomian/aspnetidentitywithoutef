using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Identity.NoEF.Data
{
    internal class UserRoleHandler : BaseHandler
    {
        public List<String> GetRolesByUserId(String userId)
        {
            List<String> Roles = new List<string>();

            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUserRole"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                using (IDataReader reader = db.ExecuteReader(cmd)) 
                {
                    while (reader.Read())
                    {
                        Roles.Add(Convert.ToString(reader["RoleId"]));
                    }
                }
            }

            return Roles;
        }

        public Int32 AddRole(String userId, String roleId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_AddUserRole"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                db.AddInParameter(cmd, "RoleId", DbType.String, roleId);

                return db.ExecuteNonQuery(cmd);
            }
        }

        public Int32 RemoveRole(String userId, String roleId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUserRole"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                db.AddInParameter(cmd, "RoleId", DbType.String, roleId);

                return db.ExecuteNonQuery(cmd);
            }
        }

        public Int32 RemoveAllRoles(String userId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUserRoleAllByUserId"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return db.ExecuteNonQuery(cmd);
            }
        }
    }
}
