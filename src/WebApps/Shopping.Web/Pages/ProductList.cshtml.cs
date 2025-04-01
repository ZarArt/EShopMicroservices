namespace Shopping.Web.Pages;

public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger) : PageModel
{
    public IEnumerable<string> CategoryList { get; set; } = [];
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        logger.LogInformation("Product list page visited");

        var result = await catalogService.GetProducts();
        CategoryList = result.Products.SelectMany(p => p.Category).Distinct();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            ProductList = result.Products.Where(p => p.Category.Contains(categoryName));
            SelectedCategory = categoryName;
        }
        else
        {
            ProductList = result.Products;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");
        var productResponse = await catalogService.GetProduct(productId);

        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}
