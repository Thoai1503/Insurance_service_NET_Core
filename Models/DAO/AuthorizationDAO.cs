using Insurance_agency.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Insurance_agency.Models.DAO
{
    public sealed class AuthorizationDAO
    {
        private static AuthorizationDAO instance = null;
        private AuthorizationDAO()
        {
        }
        public static AuthorizationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthorizationDAO();
                }
                return instance;
            }
        }
        public bool GetAuthorization(int id_authen, string absoluteUrl)
        {
            try
            {
                string url = absoluteUrl;
                var en = new InsuranceContext();
                var res = en.TblAuthorizations.Any(c => c.AuthenId == id_authen &&  url.Contains(c.Url)||id_authen==3&&url.Contains("/adminarea"));
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting authorization: " + ex.Message);
              //  throw new Exception("Error retrieving active categories: " + ex.Message);
            }
            return false;
        }
    }
}