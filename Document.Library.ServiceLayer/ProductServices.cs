using Document.Library.Entity;
using Document.Library.Entity.Exceptions;
using Document.Library.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.ServiceLayer
{
    public static class ProductServices
    {

        private static DataTable GetProductsDataTable(List<Product> products)
        {
            DataTable dataTable = new DataTable();
            // add static columns
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Quantity", typeof(int));
            dataTable.Columns.Add("Price", typeof(int));
            foreach (Product product in products)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["Id"] = product.Id;
                dataRow["Quantity"] = product.Quantity;
                dataRow["Price"] = product.Price;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;

        }
        public static IEnumerable<Product> GetAll()
        {
            try
            {
                return new ProductRepository().GetAll();
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<UserProduct> Get(int userId)
        {
            try
            {
                return new ProductRepository().Get(userId);
            }
            catch
            {
                throw;
            }
        }

        public static void Save(int userId, List<Product> products)
        {
            try
            {
                List<int> ids = new List<int>();
                products.ForEach(p =>
                {
                    ids.Add(p.Id);
                });
                new ProductRepository().SaveProducts(userId, ids);
            }
            catch
            {
                throw;
            }
        }

        public static UserProduct AddToCart(UserProduct product)
        {
            try
            {

                return new ProductRepository().AddToCart(product);
            }
            catch
            {
                throw;
            }
        }

        public static void Update(int id, int price, int quantity)
        {
            try
            {
                new ProductRepository().Update(id, price, quantity);
            }
            catch
            {
                throw;
            }
        }

        public static void Delete(int id)
        {
            try
            {
                new ProductRepository().Delete(id);
            }
            catch
            {
                throw;
            }
        }


    }
}
