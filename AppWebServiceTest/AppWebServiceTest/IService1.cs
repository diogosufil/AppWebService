﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AppWebServiceTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        /*
       * AUTHENTICATION
       * */
        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "login?username={username}&password={password}")]
        string logIn(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "logout")]
        void logOut(string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "isAdmin?token={token}")]
        bool isAdmin(string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "isLoggedIn?token={token}")]
        bool isLoggedIn(string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getAllUtilizadores?token={token}")]
        List<UtilizadorWEB> getAllUtilizadores(string token);

        /*
         *  END AUTHENTICATION
         * 
         */
    }

    [DataContract]
    public class UtilizadorWEB
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public Boolean IsAdmin { get; set; }
    }
}
