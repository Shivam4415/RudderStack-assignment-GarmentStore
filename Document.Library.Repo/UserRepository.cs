using Dapper;
using Document.Library.Entity;
using Document.Library.Globals.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Repo
{
    public class UserRepository
    {

        public UserProfile Add(string name, string email, string phone, string password, string company)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@name", name);
                    param.Add("@email", email);
                    param.Add("@phone", phone);
                    param.Add("@password", password);
                    param.Add("@company", company);
                    return con.Query<UserProfile>("[RudderStack].[RegisterUser]", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }

        }



        public bool IsUserExists(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@email", email);
                    return con.Query<bool>("[RudderStack].[IsUserExists]", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
