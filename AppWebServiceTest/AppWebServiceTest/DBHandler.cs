using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace AppWebServiceTest
{
    public class DBHandler
    {
        Model1Container model = new Model1Container();

        public Boolean DBLogin(String username, String password)
        {
            Boolean isLogged = false;
            Utilizador u = new Utilizador();

            try
            {
                u = model.UtilizadorSet.Where(i => i.username == username).First();
                if (u.password.Equals(password))
                {
                    isLogged = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isLogged;
        }

        public List<Utilizador> getListaUtilizadoresDB()
        {
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
            listaUtilizadores = model.UtilizadorSet.ToList();
            return listaUtilizadores;
        }
    }
}
