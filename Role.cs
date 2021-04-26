using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Role
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Role(string type)
        {
            this.Type = type;
        }
        public Role()
        {
            
        }
        // For new roles
        public void InitializeTypes()
        {
            List<Role> types = new List<Role>();
            types.Add(new Role("Admin"));
            types.Add(new Role("Doctor"));
            types.Add(new Role("Nurse"));
            ConnectDb.InsertUserType(types);
        }
    }
}
