using Document.App.Api.CustomResponse;
using Document.App.App_Start;
using Document.Library.Entity;
using Document.Library.Entity.Exceptions;
using Document.Library.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Document.App.Api.v1
{
    [RoutePrefix("api/v1/products")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Working
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            try
            {
                UserProfile _user = AuthManager.CurrentUser;
                if (_user == null)
                    throw ExceptionResponse.Forbidden(Request, Messages.InvalidCredentials);

                return ProductServices.GetAll();

            }
            catch (RequestForbidden ex)
            {
                throw ExceptionResponse.Forbidden(Request, ex.Message);
            }
            catch (Exception ex)
            {
                throw ExceptionResponse.ServerErrorResponse(Request);
            }
        }

        /// <summary>
        /// Working
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("{userId}")]
        public IEnumerable<UserProduct> Get(int userId)
        {
            try
            {
                UserProfile _user = AuthManager.CurrentUser;
                if (_user == null || userId != _user.Id)
                    throw ExceptionResponse.Forbidden(Request, Messages.InvalidCredentials);

                return ProductServices.Get(userId);
            }
            catch (RequestForbidden ex)
            {
                throw ExceptionResponse.Forbidden(Request, ex.Message);
            }
            catch (Exception ex)
            {
                throw ExceptionResponse.ServerErrorResponse(Request);
            }
        }

        /// <summary>
        /// Working
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="products"></param>
        [Route("{userId}")]
        [HttpPost]
        public void Save(int userId, [FromBody]List<Product> products)
        {
            try
            {
                if(products.Count == 0)
                {
                    throw new RequestForbidden("Invalid Request");
                }
                UserProfile _user = AuthManager.CurrentUser;
                if (_user == null || userId != _user.Id)
                    throw ExceptionResponse.Forbidden(Request, Messages.InvalidCredentials);

                ProductServices.Save(userId, products);

            }
            catch (RequestForbidden ex)
            {
                throw ExceptionResponse.Forbidden(Request, ex.Message);
            }
            catch (Exception ex)
            {
                throw ExceptionResponse.ServerErrorResponse(Request);
            }
        }


        /// <summary>
        /// Working
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public UserProduct AddToCart(UserProduct product)
        {
            try
            {
                UserProfile _user = AuthManager.CurrentUser;
                if (_user == null || product.UserId != _user.Id)
                    throw ExceptionResponse.Forbidden(Request, Messages.InvalidCredentials);

                return ProductServices.AddToCart(product);
            }
            catch (RequestForbidden ex)
            {
                throw ExceptionResponse.Forbidden(Request, ex.Message);
            }
            catch (Exception ex)
            {
                throw ExceptionResponse.ServerErrorResponse(Request);
            }
        }

        /// <summary>
        /// Working
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [Route("{id}")]
        [HttpPut]
        public void Update(int id, Product product)
        {
            try
            {
                UserProfile _user = AuthManager.CurrentUser;
                if (_user == null)
                    throw ExceptionResponse.Forbidden(Request, Messages.InvalidCredentials);

                ProductServices.Update(id, product.Price.Value, product.Quantity);

            }
            catch (RequestForbidden ex)
            {
                throw ExceptionResponse.Forbidden(Request, ex.Message);
            }
            catch (Exception ex)
            {
                throw ExceptionResponse.ServerErrorResponse(Request);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [Route("{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                UserProfile _user = AuthManager.CurrentUser;
                if (_user == null)
                    throw ExceptionResponse.Forbidden(Request, Messages.InvalidCredentials);

                ProductServices.Delete(id);

            }
            catch (RequestForbidden ex)
            {
                throw ExceptionResponse.Forbidden(Request, ex.Message);
            }
            catch (Exception ex)
            {
                throw ExceptionResponse.ServerErrorResponse(Request);
            }

        }


    }
}