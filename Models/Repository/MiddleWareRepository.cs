using Insurance_agency.Models.DAO;
using Insurance_agency.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurance_agency.Models.Repository
{
    public class MiddleWareRepository
    {
        private static MiddleWareRepository instance = null;
        private MiddleWareRepository()
        {
        }
        public static MiddleWareRepository Instance
        {
            get
            {
                if (instance == null)
                {   
                    instance = new MiddleWareRepository();
                }
                return instance;
            }
        }
        public User CheckLogin(string username, string password)
        {
            return AccountDAO.Instance.GetMember(username, password);
        }
        public bool CheckAuthorization(int id_authen, string absoluteUrl)
        {
            return AuthorizationDAO.Instance.GetAuthorization(id_authen, absoluteUrl);
        }
    }
}   