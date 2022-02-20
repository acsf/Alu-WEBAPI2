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
        //[HttpGET]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var dao = new CarrinhoDAO();
                var carrinho = dao.Busca(id);
                return Request.CreateResponse(HttpStatusCode.OK, carrinho);
            }
            catch (KeyNotFoundException)
            {
                string msg = string.Format("O carrinho {0} não foi encontrado", id);
                HttpError error = new HttpError(msg);
                return Request.CreateResponse(HttpStatusCode.NotFound, error);                
            }
           
        }

        //[HttpPOST]
        public HttpResponseMessage Post([FromBody]Carrinho carrinho)
        {
            var dao = new CarrinhoDAO();
            dao.Adiciona(carrinho);

            var response = Request.CreateResponse(HttpStatusCode.Created);

            //Uma boa prática passar a localização ao criar um objeto
            //Para que outros possam localizar
            var location = Url.Link("DefaultApi", new { controller = "carrinho", id = carrinho.Id });
            response.Headers.Location = new Uri(location);
            
            return response;
        }

        //[HttpDELETE]
        [Route("api/carrinho/{idCarrinho}/produto/{idProduto}")]
        public HttpResponseMessage Delete([FromUri]int idCarrinho,[FromUri]int idProduto)
        {
            var dao = new CarrinhoDAO();
            var carrinho = dao.Busca(idCarrinho);
            carrinho.Remove(idProduto);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
