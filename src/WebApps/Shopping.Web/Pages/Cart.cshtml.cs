namespace Shopping.Web.Pages;

public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
{
    public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket();

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveFromCartAsync(Guid productId)
    {
        logger.LogInformation("Remove to cart button clicked");

        Cart = await basketService.LoadUserBasket();

        Cart.Items.RemoveAll(x => x.ProductId == productId);

        return RedirectToPage();
    }
}
