using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.NoEF.Data
{
    internal class UserLoginHandler : BaseHandler
    {

        public List<UserLoginInfo> GetLoginByUserId(String userId) 
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();

            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUserLogin"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        UserLoginInfo login = new UserLoginInfo( Convert.ToString( reader["LoginProvider"] ), Convert.ToString( reader["ProviderKey"] ));

                        logins.Add(login);
                    }
                }
            }

            return logins;
        }


        public String GetUserIdByLogin(UserLoginInfo login)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUserIdByLogin"))
            {
                db.AddInParameter(cmd, "ProviderKey", DbType.String, login.ProviderKey);
                db.AddInParameter(cmd, "LoginProvider", DbType.String, login.LoginProvider);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return Convert.ToString(reader["UserId"]);
                    }
                }
            }

            return String.Empty;
        }

        public Int32 AddLogin(UserLoginInfo login, String userId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_AddUserLogin"))
            {
                db.AddInParameter(cmd, "ProviderKey", DbType.String, login.ProviderKey);
                db.AddInParameter(cmd, "LoginProvider", DbType.String, login.LoginProvider);

                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return db.ExecuteNonQuery(cmd);
            }
        }

        public Int32 RemoveLogin(UserLoginInfo login, String userId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUserLogin"))
            {
                db.AddInParameter(cmd, "ProviderKey", DbType.String, login.ProviderKey);
                db.AddInParameter(cmd, "LoginProvider", DbType.String, login.LoginProvider);

                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return db.ExecuteNonQuery(cmd);
            }
        }

        public Int32 RemoveAllLogins(String userId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUserLoginAllByUserId"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return db.ExecuteNonQuery(cmd);
            }
        }


    }
}
