using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using AppWebServiceTest;

namespace DataAccess
{
    public class DBHandler
    {
        Model1Container model = new Model1Container();

        public Boolean Login(String username, String password)
        {
            Boolean isLogged = false;

            if (model.UtilizadorSet.Where(i => i.username == username).First())
            {
                if(model.UtilizadorSet.Where(i => i.password == password)){
                    isLogged = true;
                }
            }
            return isLogged;
        }

        public List<Utilizador> getListaUtilizadores()
        {
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
            listaUtilizadores = model.UtilizadorSet.ToList();
            return listaUtilizadores;
        }
    }
}
