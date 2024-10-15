using AutoMapper;
using E_Mart.Domain.Carts;
using E_Mart.Domain.Products;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models.Cart;
using E_Mart.WebApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ProductService _productService;
    private readonly CartService _cartService;
    private readonly CartItemService _cartItemService;
    private readonly IMapper _mapper;
    public CartController(UserService userService, ProductService productService, CartService cartService, CartItemService cartItemService, IMapper mapper)
    {
        _userService = userService;
        _productService = productService;
        _cartService = cartService;
        _cartItemService = cartItemService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("addToCartAsync")]
    public async Task<IActionResult> AddToCartAsync([FromForm]CartAddViewModal cartAddViewModal)
    {
        try
        {
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userService.UserExistsAsync(userName);
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new Response { Status = "Error", Message = "User not authenticated." });
            }
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User Not Found!" });
            }
            var quantity = 1;
            var productPrice = await _productService.GetProductPriceAsync(cartAddViewModal.productId);
            var total = productPrice;
            var cart = _mapper.Map<Cart>(cartAddViewModal);
            cart.UserId = user.Id;
            cart.Total = total;
            var addCart = await _cartService.AddAsync(cart);
            var cartItem = _mapper.Map<CartItem>(cartAddViewModal);
            cartItem.Quantity = 1;
            cartItem.CartId = addCart.Id;
            var addCartItem = await _cartItemService.AddAsync(cartItem);
            return Ok(new Response { Status = "Success",Message = "Cart Item Added Successfully."});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("getCartItemsByUserId/{id}")]
    public async Task<IActionResult> GetCartItemsByUserId(int userID)
    {
        try {

            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }
}