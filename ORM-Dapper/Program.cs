using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper;

public class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connString = config.GetConnectionString("DefaultConnection");

        IDbConnection conn = new MySqlConnection(connString);

        #region Department Selection
        var departmentRepo = new DapperDepartmentRepository(conn);

        var departments = departmentRepo.GetAllDepartments();

        foreach (var department in departments)
        {
            Console.WriteLine(department.DepartmentID);
            Console.WriteLine(department.Name);
            Console.WriteLine();
            Console.WriteLine();


        }
        #endregion

        var productRepository = new DapperProductRepository(conn);

        var productToUpdate = productRepository.GetProduct(957);

        productToUpdate.Name = "UPDATED!";
        productToUpdate.OnSale = false;
        productToUpdate.Price = 12;
        productToUpdate.CategoryID = 4;
        productToUpdate.StockLevel = 959;


        productRepository.UpdateProduct(productToUpdate);

        var products = productRepository.GetAllProducts();
        foreach (var product in products) 
        {
            Console.WriteLine(product.ProductId);
            Console.WriteLine(product.Name);
            Console.WriteLine(product.Price);
            Console.WriteLine(product.CategoryID);
            Console.WriteLine(product.OnSale);
            Console.WriteLine(product.StockLevel);
            Console.WriteLine();
            Console.WriteLine();

        
        }
    }
}
