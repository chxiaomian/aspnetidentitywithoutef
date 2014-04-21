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
    /// <summary>
    /// Handler for 'AspNetIdentity_User' table.
    /// </summary>
    internal class UserHandler : BaseHandler
    {
        /// <summary>
        /// Returns the Identity User object.
        /// </summary>
        /// <param name="reader">IDataReader object with Identity user data.</param>
        /// <returns>Returns IdentityUser.</returns>
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

        /// <summary>
        /// Returns the User object by user ID.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Returns User.</returns>
        public IdentityUser GetUserById(String userId)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUser"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return GetUser(db.ExecuteReader(cmd));
            }
        }

        /// <summary>
        /// Returns the User object by user name.
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>Returns User.</returns>
        public IdentityUser GetUserByUserName(String userName)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUser"))
            {
                db.AddInParameter(cmd, "UserName", DbType.String, userName);

                return GetUser(db.ExecuteReader(cmd));
            }
        }

        /// <summary>
        /// Adds new record to IdentityUser table.
        /// </summary>
        /// <param name="newUser">User to add.</param>
        /// <returns>Returns User.</returns>
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

        /// <summary>
        /// Deletes a record from IdentityUser table.
        /// </summary>
        /// <param name="newUser">User to delete.</param>
        /// <returns>No of rows deleted.</returns>
        public Int32 RemoveUser(IdentityUser newUser)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUser"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, newUser.Id);

                return db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Updates the IdentityUser record.
        /// </summary>
        /// <param name="newRole">User object with updated values.</param>
        /// <returns>Returns User.</returns>
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

        /// <summary>
        /// Returns the password hash by user ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Returns hashed password string.</returns>
        public String GetPasswordHash(String userId)
        {
            IdentityUser user = GetUserById(userId);

            return user.PasswordHash;
        }
    }
}
