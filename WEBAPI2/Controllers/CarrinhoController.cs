using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.DAO;
using Loja.Models;

namespace WEBAPI2.Controllers
{
    public class CarrinhoController : ApiController
    {
        //[HttpPost]
        public Carrinho Get(int id)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            Carrinho carrinho = dao.Busca(id);
            return carrinho;
        }
    }
}
