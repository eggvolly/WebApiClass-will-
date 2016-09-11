using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi0911.Models;

namespace WebApi0911.Controllers
{
    [RoutePrefix("clients")]
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ClientsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Clients
        [Route("")]
        public IQueryable<Client> GetClient()
        {
            return db.Client;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("{id:int}",Name="GetClientById")]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [Route("~/clients/type2/{id:int}")]
        public Client GetClientType2(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return null;
            }

            return client;
        }

        [Route("~/clients/type3/{id:int}")]
        public IHttpActionResult GetClientType3(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return null;
            }

            return Json(client); //回傳的東西就一定會是Json
        }

        [Route("~/clients/type4/{id:int}")]
        public HttpResponseMessage GetClientType4(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return null;
            }

            return Request.CreateResponse(HttpStatusCode.OK, client);
        }

        [Route("~/clients/type5/{id:int}")]
        public HttpResponseMessage GetClientType5(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return null;
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                ReasonPhrase="Hello World",
                Content = new ObjectContent<Client>(client, GlobalConfiguration.Configuration.Formatters.JsonFormatter)
            };  //可以控制內容資訊
        }

        [Route("~/clients/type6/{id:int}")]
        public HttpResponseMessage GetClientType6(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return null;
            }

            var res = new HttpResponseMessage(HttpStatusCode.OK)
            {
                ReasonPhrase = "Hello World",                
                Content = new StringContent("希望等下不要下雨", Encoding.GetEncoding("Big5"))
            };  //可以控制內容資訊
            res.Headers.Add("X-Job", "1");
            res.Headers.Add("AA", "安安");

            return res;
        }

        [Route("{id}/orders")]
        public IHttpActionResult GetClientOrders(int id)
        {
            var orders = db.Order.Where(p => p.ClientId == id);

            return Ok(orders);
        }

        [ResponseType(typeof(Order))]
        [Route("{id}/orders/{pending:alpha}")]
        public IHttpActionResult GetClientPending(int id, string pending)
        {
            var client = db.Order.Where(p => p.ClientId == id && p.OrderStatus == pending);


            return Ok(client);
        }

        [ResponseType(typeof(Order))]
        [Route("{id}/orders/{orderId:int}")]
        public IHttpActionResult GetClient(int id, int orderId)
        {
            var order = db.Order.FirstOrDefault(p => p.ClientId == id && p.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [ResponseType(typeof(Order))]
        [Route("{id}/orders/{*orderdate}")]
        public IHttpActionResult GetClient(int id, DateTime orderdate)
        {
            var order = db.Order.Where(p => p.ClientId == id && p.OrderDate.Value.Year==orderdate.Year
                && p.OrderDate.Value.Month==orderdate.Month
                && p.OrderDate.Value.Day==orderdate.Day);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        [Route("")]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            //return Created(Url.Link("GetClientById", new { id = client.ClientId }), client);
            return CreatedAtRoute("GetClientById", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}