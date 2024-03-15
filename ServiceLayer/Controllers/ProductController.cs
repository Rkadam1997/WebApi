using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        QuickKartRepository repository;

        public ProductController(QuickKartRepository repository)
        {
            this.repository = repository;   
        }


        #region Get All Product 
        [HttpGet]
        public JsonResult GetAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                products = repository.GetAllProducts();
            }
            catch (Exception)
            {

                products = null;
            }

            return Json(products);
        }
        #endregion

        #region Get product by ID 
        [HttpGet]
        public JsonResult GetProductbyId(string productId)
        {

            Product product = new Product();

            try
            {
                product = repository.GetProductById(productId);
            }
            catch (Exception)
            {

                product = null;
            }

            return Json(product);

        }

        #endregion

        #region Add product using post method 
        [HttpPost]
        public JsonResult AddProductUsingParams(string productName, byte categoryId, decimal price, int quantityAvailable)
        {
            bool status = false;
            string productId;
            string message;
            try
            {
                status = repository.AddProduct(productName, categoryId, price, quantityAvailable, out productId);
                if (status)
                {
                    message = "Successful addition operation, ProductId = " + productId;
                }
                else
                {
                    message = "Unsuccessful addition operation!";
                }
            }
            catch (Exception)
            {
                message = "Some error occured, please try again!";
            }
            return Json(message);
        }



        #endregion

        #region Add product using post method using models

        public JsonResult AddProductByModels(Product product)
        {
            bool status = false;
            string message;


            try
            {
                status = repository.AddProduct(product);
                if (status)
                {
                    message = "Successful addition operation, ProductId = " + product.ProductId; 
                }
                else
                {
                    message = "Unsuccessful addition operation!";
                }

            }
            catch (Exception)
            {

                message = "Some error occured, please try again!";
            }

            return Json(message);
        }


        #endregion

        #region update the existing product

        [HttpPut]
        public bool UpdateProductByEFModels(Product product)
        {
            bool status = false;

            try
            {
                status = repository.UpdateProduct(product);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        #endregion

        #region update using existing product Model

        [HttpPut]
        public bool UpdateProductByAPIModels(Models.Product product)
        {
            bool status = false;

            try
            {
                if (ModelState.IsValid)
                {
                    // map service layer model class with data access layer model class.
                    Product prodObj = new Product();
                    prodObj.ProductId = product.ProductId;
                    prodObj.ProductName = product.ProductName;
                    prodObj.CategoryId = product.CategoryId;
                    prodObj.Price = product.Price;
                    prodObj.QuantityAvailable = product.QuantityAvailable;
                    status = repository.UpdateProduct(prodObj);
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region Delete the product 

        [HttpDelete]
        public JsonResult DeleteProduct(string productId)
        {
            bool status = false;
            try
            {
                status = repository.DeleteProduct(productId);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }
        #endregion

    }
}
