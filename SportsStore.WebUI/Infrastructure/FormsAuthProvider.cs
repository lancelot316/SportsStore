﻿using SportsStore.WebUI.Models.Domain;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {

            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}