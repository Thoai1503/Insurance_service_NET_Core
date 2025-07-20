using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Web;



namespace Insurance_agency.Models.DAO
{
    public class AccountDAO
    {
        
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public bool CreateMember(User entity)
        {
            try
            {
                var conn = new InsuranceContext();
                var member = new TblUser
                {
                    
                    FullName = entity.full_name,
                    Email = entity.email,
                    Password = entity.password,
                    Phone = entity.phone,
                    Address = entity.address,
                    AuthId = entity.auth_id,
                    Avatar = entity.avatar,

                    Active = 1 // Assuming 1 means active
                };
                conn.TblUsers.Add(member);
                conn.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error creating category: " + ex.Message);
            }
            return false;
        }
             
            public User GetMember(string username, string password)
        {
            try
            {
                var en = new InsuranceContext();
                var pass = Function.MD5Hash(password);
                var res = en.TblUsers.FirstOrDefault(InsuranceContext => InsuranceContext.Email == username && InsuranceContext.Password == password|| 
                InsuranceContext.Email == username&&InsuranceContext.Password==pass);
                if (res != null)
                {
                    return new User
                    {
                        id = res.Id,
                        full_name = res.FullName,
                        email = res.Email,
                        password = res.Password,
                        phone = res.Phone,
                        address = res.Address ?? "",
                        auth_id = res.AuthId ?? 0,
                        avatar = res.Avatar ?? "",
                        created_date = DateTime.Now, // Assuming created_at is set to current time
                        active = res.Active ?? 1 // Assuming 1 means active
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error getting member: " + ex.Message);
            }
            return null;
        }

    }

}