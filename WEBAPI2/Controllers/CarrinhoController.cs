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
        public HttpResponseMessage Get(int id)
        {
            try
            {
                CarrinhoDAO dao = new CarrinhoDAO();
                Carrinho carrinho = dao.Busca(id);
                return Request.CreateResponse(HttpStatusCode.OK, carrinho);
            }
            catch (KeyNotFoundException)
            {
                string msg = string.Format("O carrinho {0} não foi encontrado", id);
                HttpError error = new HttpError(msg);
                return Request.CreateResponse(HttpStatusCode.NotFound, error);                
            }
           
        }

        public HttpResponseMessage Post([FromBody]Carrinho carrinho)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            dao.Adiciona(carrinho);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

            //Uma boa prática passar a localização ao criar um objeto
            //Para que outros possam localizar
            string location = Url.Link("DefaultApi", new { controller = "carrinho", id = carrinho.Id });
            response.Headers.Location = new Uri(location);
            
            return response;
        }
    }
}
