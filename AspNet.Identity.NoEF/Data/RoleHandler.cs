using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace AspNet.Identity.NoEF.Data
{
    internal class RoleHandler: BaseHandler
    {

        private IdentityRole GetRole(IDataReader reader)
        {
            IdentityRole newRole = null;

            if (reader.Read())
            {
                newRole = new IdentityRole();

                newRole.Id = Convert.ToString(reader["RoleId"]);
                newRole.Name = Convert.ToString(reader["RoleName"]);
                newRole.Description = Convert.ToString(reader["Description"]);
            }

            return newRole;
        }

        public IdentityRole GetRoleById(String roleId) 
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetRole"))
            {
                db.AddInParameter(cmd, "RoleId", DbType.String, roleId);

                return GetRole(db.ExecuteReader(cmd));
            }
        }

        public IdentityRole GetRoleByName(String roleName)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetRole"))
            {
                db.AddInParameter(cmd, "RoleName", DbType.String, roleName);

                return GetRole(db.ExecuteReader(cmd));
            }
        }

        public IdentityRole AddRole(IdentityRole newRole)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_AddRole"))
            {
                db.AddInParameter(cmd, "RoleId", DbType.String, newRole.Id);
                db.AddInParameter(cmd, "RoleName", DbType.String, newRole.Name);
                db.AddInParameter(cmd, "Description", DbType.String, newRole.Description);

                db.ExecuteNonQuery(cmd);

                return GetRoleById(newRole.Id);
            }
        }

        public IdentityRole SaveRole(IdentityRole newRole)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_SaveRole"))
            {
                db.AddInParameter(cmd, "RoleId", DbType.String, newRole.Id);
                db.AddInParameter(cmd, "RoleName", DbType.String, newRole.Name);
                db.AddInParameter(cmd, "Description", DbType.String, newRole.Description);

                db.ExecuteNonQuery(cmd);

                return GetRoleById(newRole.Id);
            }
        }
    }
}
