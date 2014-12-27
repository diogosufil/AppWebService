using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AppWebServiceTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DBHandler dbHandler = new DBHandler();
        private Dictionary<string, UtilizadorWEB> utilizadores;
        private Dictionary<string, Token> tokens;

        public Service1()
        {

            this.utilizadores = new Dictionary<string, UtilizadorWEB>();
            this.tokens = new Dictionary<string, Token>();
        }
        private class Token
        {
            private string value;
            private DateTime dataLogin;
            private DateTime dataExpirar;
            private int HORAS;
            private UtilizadorWEB utilizador;

            public Token(UtilizadorWEB utilizador) : this(utilizador, DateTime.Now) { }

            public Token(UtilizadorWEB utilizador, DateTime dataLogin)
            {
                HORAS = 10;
                this.value = Guid.NewGuid().ToString();
                this.dataLogin = dataLogin;
                this.dataExpirar = dataLogin.AddHours(HORAS);
                this.utilizador = utilizador;
            }

            public string Value { get { return value; } }
            public DateTime DataExpirar { get { return dataExpirar; } }
            public UtilizadorWEB Utilizador { get { return utilizador; } }
            public string Username { get { return utilizador.Username; } }
            public Boolean isTimeOutExpired() { return dataExpirar < DateTime.Now; }

        }

        //authentication

        public string logIn(String username, String password)
        {
            cleanUpTokens();
            lercontasBD();

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && password.Equals(utilizadores[username].Password))
            {
                Token tokenObject = new Token(utilizadores[username]);
                tokens.Add(tokenObject.Value, tokenObject);
                return tokenObject.Value;
            }
            else
            {
                throw new ArgumentException("ERROR: invalid username/password combination");
            }
            // return handler.logIn(username, password);
        }

        private void lercontasBD()
        {
            List<UtilizadorWEB> listaUtilizadoresWeb = new List<UtilizadorWEB>();
            List<Utilizador> listaUtilizador = dbHandler.getListaUtilizadoresDB();

            foreach (Utilizador c in listaUtilizador)
            {
                UtilizadorWEB con = new UtilizadorWEB();
                con.Username = c.username;
                con.Password = c.password;
                con.IsAdmin = c.isAdmin;
                con.id = c.Id;
                if (!verificaConta(con))
                    utilizadores.Add(con.Username, con);
            }



        }

        private bool verificaConta(UtilizadorWEB util)
        {
            foreach (KeyValuePair<String, UtilizadorWEB> c in utilizadores)
            {
                if (c.Value.Username.Equals(util.Username))
                    return true;
            }
            return false;
        }

        public void logOut(string token)
        {
            tokens.Remove(token);
            cleanUpTokens();

        }

        public bool isAdmin(string token)
        {
            return tokens[token].Utilizador.IsAdmin;
        }

        public bool isLoggedIn(string token)
        {
            bool res = true;
            try
            {
                checkAuthentication(token, false);
            }
            catch (ArgumentException)
            {
                res = false;
            }
            return res;
        }

        private void cleanUpTokens()
        {
            foreach (Token tokenObject in tokens.Values)
            {

                tokens.Remove(tokenObject.Username);

            }
        }

        private Token checkAuthentication(string token, bool mustBeAdmin)
        {
            Token tokenObject;
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Error: invalid token value.");
            }
            try
            {
                tokenObject = tokens[token];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Error: user is not logged in /expired session?).");
            }
            if (tokenObject.isTimeOutExpired())
            {
                tokens.Remove(tokenObject.Username);
                throw new Exception("Error: the session has expired. Please Loged in again.");
            }
            if (mustBeAdmin && !tokens[token].Utilizador.IsAdmin)
            {
                throw new ArgumentException("Error: only admins are allowed to perform this operation.");
            }
            return tokenObject;

        }


        public List<UtilizadorWEB> getAllUtilizadores(string token)
        {
            checkAuthentication(token, false);
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
            List<UtilizadorWEB> listaFinal = new List<UtilizadorWEB>();

            listaUtilizadores = dbHandler.getListaUtilizadoresDB();

            foreach (Utilizador u in listaUtilizadores)
            {
                UtilizadorWEB util = new UtilizadorWEB();
                util.id = u.Id;
                util.IsAdmin = u.isAdmin;
                util.Username = u.username;
                util.Password = u.password;
                listaFinal.Add(util);
            }

            return listaFinal;
        }


        //fim authentication
    }
}
