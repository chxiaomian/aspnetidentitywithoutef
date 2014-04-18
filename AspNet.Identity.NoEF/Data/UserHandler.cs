using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AspNet.Identity.NoEF.Data
{
    internal class UserHandler : BaseHandler
    {
        private IdentityUser GetUser(IDataReader reader)
        {
            IdentityUser newUser = null;

            if (reader.Read()) {

                newUser = new IdentityUser();

                newUser.Id = Convert.ToString(reader["UserId"]);
                newUser.UserName = Convert.ToString(reader["UserName"]);
                newUser.PasswordHash = Convert.ToString(reader["PasswordHash"]);
                newUser.SecurityStamp = Convert.ToString(reader["SecurityStamp"]);
            }

            return newUser;
        }

        public IdentityUser GetUserById(String userId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUser"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return GetUser(db.ExecuteReader(cmd));
            }
        }

        public IdentityUser GetUserByUserName(String userName)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUser"))
            {
                db.AddInParameter(cmd, "UserName", DbType.String, userName);

                return GetUser(db.ExecuteReader(cmd));
            }
        }

        public IdentityUser AddUser(IdentityUser newUser) 
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_AddUser"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, newUser.Id);
                db.AddInParameter(cmd, "UserName", DbType.String, newUser.UserName);
                db.AddInParameter(cmd, "PasswordHash", DbType.String, newUser.PasswordHash);
                db.AddInParameter(cmd, "SecurityStamp", DbType.String, newUser.SecurityStamp);

                db.ExecuteNonQuery(cmd);

                return GetUserById(newUser.Id);
            }
        }

        public Int32 RemoveUser(IdentityUser newUser)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUser"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, newUser.Id);

                return db.ExecuteNonQuery(cmd);
            }
        }

        public IdentityUser SaveUser(IdentityUser newUser)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_SaveUser"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, newUser.Id);
                db.AddInParameter(cmd, "UserName", DbType.String, newUser.UserName);
                db.AddInParameter(cmd, "PasswordHash", DbType.String, newUser.PasswordHash);
                db.AddInParameter(cmd, "SecurityStamp", DbType.String, newUser.SecurityStamp);

                db.ExecuteNonQuery(cmd);

                return GetUserById(newUser.Id);
            }
        }

        public String GetPasswordHash(String userId)
        {
            IdentityUser user = GetUserById(userId);

            return user.PasswordHash;
        }

        public String SetPasswordHash(String userId, String passwordHash)
        {
            IdentityUser user = GetUserById(userId);

            user.PasswordHash = passwordHash;

            SaveUser(user);

            return user.PasswordHash;
        }
    }
}
