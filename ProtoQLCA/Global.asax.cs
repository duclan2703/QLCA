using DAO_QLAC.BUS;
using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ProtoQLCA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Bootstrapper.InitializeContainer();
        }
        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;
                        Account account = new Account();
                        AccountRolService service = new AccountRolService();
                        RolService service1 = new RolService();
                        AccountService accservice = new AccountService();
                        // username = 'admin'
                        // AcountService.GetUserIdByName(string username); => userid
                        // AcountRolService.GetListRoleIdByUserId(int userid) => listRoleId
                        // RoleService.GetListRoleNameByListRoleId( List<int> listRoldId) =>  listRoleName
                        // lstRoleName => string. Vi du:  "admin;user"
                        var userid = accservice.GetUserIDByUsername(username);
                        var listrole = service.GetListRoleIDByUserID(userid);                        
                        var listrolename = service1.GetListRoleNameByListRoleId(listrole);
                        
                        roles = String.Join(";", listrolename.ToArray());
                        //let us extract the roles from our own custom cookie


                        //Let us set the Pricipal with our user specific details
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split());
                    }
                    catch (Exception ex)
                    {
                        
                        //somehting went wrong
                    }
                }
            }
        }
    }
}
