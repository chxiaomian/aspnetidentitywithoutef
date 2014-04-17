using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Identity.NoEF.Data
{
    internal class UserClaimHandler : BaseHandler
    {
        public ClaimsIdentity GetClaimsByUserId(String userId) 
        {
            ClaimsIdentity claims = new ClaimsIdentity();

            using (DbCommand cmd = db.GetStoredProcCommand("AI_GetUserClaim"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Claim claim = new Claim(Convert.ToString(reader["ClaimType"]), Convert.ToString(reader["ClaimValue"]));

                        claims.AddClaim(claim);
                    }
                }
            }

            return claims;
        }

        public Int32 AddClaim(Claim claim, String userId) {

            using (DbCommand cmd = db.GetStoredProcCommand("AI_AddUserClaim"))
            {
                db.AddInParameter(cmd, "ClaimType", DbType.String, claim.Type);
                db.AddInParameter(cmd, "ClaimValue", DbType.String, claim.Value);

                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                
                return db.ExecuteNonQuery(cmd);
            }
        }

        public Int32 RemoveClaim(Claim claim, String userId) {

            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUserClaim"))
            {
                db.AddInParameter(cmd, "ClaimType", DbType.String, claim.Type);
                db.AddInParameter(cmd, "ClaimValue", DbType.String, claim.Value);

                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return db.ExecuteNonQuery(cmd);
            }
        }

        public Int32 RemoveAllClaims(String userId)
        {

            using (DbCommand cmd = db.GetStoredProcCommand("AI_DeleteUserClaimAllByUserId"))
            {
                db.AddInParameter(cmd, "UserId", DbType.String, userId);

                return db.ExecuteNonQuery(cmd);
            }
        }

    }
}
