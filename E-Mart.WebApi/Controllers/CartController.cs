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
    public CartController(UserService userService, ProductService productService, CartService cartService,CartItemService cartItemService, IMapper mapper)
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
        try {
            var productPrice = await _productService.GetProductPriceAsync(cartAddViewModal.CartItem.ProductId);
            var cartByUserId = await _cartService.getCartDetilsByUserIdAsync(user.Id);
            if (cartByUserId != null)
            {
                cartByUserId.Total = cartByUserId.Total + productPrice;
                var cartData = _mapper.Map<Cart>(cartByUserId);
                await _cartService.UpdateAsync(cartData);
                cartData.CartItems = new List<CartItem>
                {
                    _mapper.Map<CartItem>(cartAddViewModal.CartItem)
                };
                await _cartItemService.AddCartItemAsync(cartData.CartItems.FirstOrDefault());
                return Ok(new Response { Status = "Success", Message = "Cart Item Added Successfully." });
            }
            var total = productPrice;
            var cart = _mapper.Map<Cart>(cartAddViewModal);
            cart.UserId = user.Id;
            cart.Total = total;
            cart.CartItems = new List<CartItem> 
            {
                 _mapper.Map<CartItem>(cartAddViewModal.CartItem)
            }; 
            await _cartService.AddAsync(cart);
            return Ok(new Response { Status = "Success",Message = "Cart Item Added Successfully."});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("getCartItemsByUserId/{userID}")]
    public async Task<IActionResult> GetCartItemsByUserId(int userID)
    {
        try {
            var cart = await _cartService.GetAllAsync();
            if (cart == null && cart.Count() > 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Cart Items Not Found!" });
            }
            var cartItems = _mapper.Map<List<CartByUserIdViewModal>>(cart);
            return Ok(cartItems);
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }
}