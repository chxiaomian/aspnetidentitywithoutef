using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Identity.NoEF
{
    public class RoleStore: IRoleStore<IdentityRole>
    {
        public Task CreateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
