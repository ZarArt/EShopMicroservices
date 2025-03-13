using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CashedBasketRepository(IBasketRepository basketRepository, IDistributedCache cashe) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cashedBasket = await cashe.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cashedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cashedBasket)!;
        }

        var basket = await basketRepository.GetBasket(userName, cancellationToken);
        await cashe.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasket(shoppingCart, cancellationToken);
        await cashe.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);
        return shoppingCart;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);

        await cashe.RemoveAsync(userName, cancellationToken);

        return true;
    }

}
