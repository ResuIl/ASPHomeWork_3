using ASPHomeWork_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPHomeWork_3.Controllers;

public class ProductController : Controller
{
    private static List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name="Alma", Price=1},
            new Product { Id = 2, Name="Armud", Price=2},
            new Product { Id = 3, Name="Banan", Price=3}
        };
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View(_products);
    }

    [HttpPost]
    public IActionResult Add(Product addProduct)
    {
        var product = new Product()
        {
            Id = addProduct.Id,
            Name = addProduct.Name,
            Price = addProduct.Price
        };
        _products.Add(product);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> View(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is not null)
        {
            var myProduct = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return await Task.Run(() => View("View", myProduct));
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult View(Product updatedProduct)
    {
        var product = _products.Find(id => updatedProduct.Id == _products.IndexOf(id));
        if (product is not null)
        {
            var newProduct = new Product()
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price
            };
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Search(string searchInput)
    {
        var filteredProducts = _products.Where(p => p.Name.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

        return View(filteredProducts);
    }

    [HttpPost]
    public IActionResult Delete(Product findProduct)
    {
        var product = _products.Find(id => findProduct.Id == _products.IndexOf(id));
        if (product is not null)
            _products.Remove(product);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Update(Product findProduct)
    {
        _products[findProduct.Id - 1].Price = findProduct.Price;
        return RedirectToAction("Index");
    }
}
