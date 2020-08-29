using Dapper;
using Document.Library.Entity;
using Document.Library.Globals;
using Document.Library.Globals.Enum;
using Document.Library.Globals.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Repo
{
    public class ProductRepository
    {
        public IEnumerable<Product> GetAll()
        {
            try
            {
                IEnumerable<Product> _products = null;
                List<int> productIds = new List<int>();
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    _products = con.Query<Product, Price, Product>("[RudderStack].[GetAllProducts]", (product, price) => {
                        productIds.Add(product.Id);
                        product.Price = price;
                        return product;
                    } , splitOn: "Value", commandType: CommandType.StoredProcedure);

                    var param = new DynamicParameters();
                    param.Add("@products", GlobalFunctions.ListToIdDataTable(productIds).AsTableValuedParameter("[RudderStack].[IntArrayTableType]"));

                    con.Query<Product, int, Product>("[RudderStack].[GetProductColor]", (product, colorId) => 
                    {
                        _products.ForEach(p =>
                        {
                            int x = p.Id;
                            if (p.Id == product.Id)
                            {
                                Color color;
                                if (Enum.TryParse(colorId.ToString(), out color))
                                {
                                    if (p.Color == null) p.Color = new List<Color>();
                                    p.Color.Add(color);
                                }   
                            }
                        });
                        return product;
                    }, param, splitOn: "ColorId", commandType: CommandType.StoredProcedure);





                    con.Query<Product, int, Product>("[RudderStack].[GetProductSize]", (product, sizeId) => {
                        foreach (Product p in _products)
                        {
                            if (p.Id == product.Id)
                            {
                                Size size;
                                if (Enum.TryParse(sizeId.ToString(), out size))
                                {
                                    if (p.Size == null) p.Size = new List<Size>();
                                    p.Size.Add(size);
                                }   
                            }
                        }
                        return product;
                    }, param, splitOn: "SizeId", commandType: CommandType.StoredProcedure);

                    return _products;

                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<UserProduct> Get(int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@userId", userId);
                    return con.Query<UserProduct, Product, int, int, int, UserProduct>("[RudderStack].[GetUserProducts]", (user, product, color, size, value)=> 
                    {
                        Color c;
                        if (Enum.TryParse(color.ToString(), out c))
                        {
                            if (product.Color == null) product.Color = new List<Color>();
                            product.Color.Add(c);
                        }
                        Size s;
                        if (Enum.TryParse(size.ToString(), out s))
                        {
                            if (product.Size == null) product.Size = new List<Size>();
                            product.Size.Add(s);
                        }
                        user.Product = product;
                        user.Product.Price = new Price() { Value = value };
                        return user;
                    }, param, splitOn: "Name, Color, Size, Value", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        public void SaveProducts(int userId, List<int> ids)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@products", GlobalFunctions.ListToIdDataTable(ids).AsTableValuedParameter("[RudderStack].[IntArrayTableType]"));
                    param.Add("@userId", userId);
                    con.Execute("[RudderStack].[SaveUserProducts]", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }

        public UserProduct AddToCart(UserProduct product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@userId", product.UserId);
                    param.Add("@productId", product.Product.Id);
                    param.Add("@productQuantity", product.Product.Quantity);
                    param.Add("@productPrice", product.Product.Price.Value);
                    param.Add("@productColor", product.Product.Color[0]);
                    param.Add("@productSize", product.Product.Size[0]);

                    return con.Query<UserProduct, Product, int, UserProduct>("[RudderStack].[SaveUserProduct]", (user, p, price) => {
                        user.Product = p;
                        user.Product.Price = new Price() { Value = price };
                        return user;
                    }, param, splitOn: "Quantity, Value", commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Update(int id, int price, int quantity)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", id);
                    param.Add("@price", price);
                    param.Add("@quantity", quantity);
                    con.Execute("[RudderStack].[UpdateUserProduct]", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }


        public void Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnection.Editor))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", id);
                    con.Execute("[RudderStack].[DeleteProduct]", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
