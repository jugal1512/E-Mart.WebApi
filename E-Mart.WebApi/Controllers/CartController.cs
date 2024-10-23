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

    [HttpGet]
    [Route("viewCartAsync/{userID}")]
    public async Task<IActionResult> ViewCartAsync()
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
        try
        {
            var cart = await _cartService.getCartDetilsByUserIdAsync(user.Id);
            if (cart == null && !cart.CartItems.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Cart is Empty!" });
            }
            var cartItems = _mapper.Map<CartByUserIdViewModal>(cart);
            return Ok(cartItems);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
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
                await UpdateExistingCart(cartByUserId, cartAddViewModal.CartItem, productPrice);
                return Ok(new Response { Status = "Success", Message = "Cart Item Added Successfully." });
            }

            await CreateNewCart(user.Id, cartAddViewModal, productPrice);
            return Ok(new Response { Status = "Success",Message = "Cart Item Added Successfully."});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPut]
    [Route("updateCartItemsAsync")]
    public async Task<IActionResult> UpdateCartItemsAsync([FromForm]CartItemUpdateViewModal cartItemUpdateViewModal)
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
        try
        {
            var cart = await _cartService.getCartDetilsByUserIdAsync(user.Id);
            if (cart == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Cart notFound!" });
            }

            var cartItem = cart.CartItems.FirstOrDefault(i => i.Id == cartItemUpdateViewModal.Id);
            if (cartItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,new Response { Status = "Error",Message = "Cart Item not Found!"});    
            }
            double productPrice = await _productService.GetProductPriceAsync(cartItem.ProductId);
            cartItem.Quantity = cartItemUpdateViewModal.Quantity;
            cart.Total = cart.CartItems.Sum(i => i.Quantity * i.ProductPrice);
            await _cartItemService.UpdateCartItemAsync(cartItem);
            await _cartService.UpdateAsync(cart);
            return Ok(new Response { Status = "Success",Message = "Cart Item Updated Successfully."});
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("removeCartItemAsync")]
    public async Task<IActionResult> RemoveCartItemAsync(int itemId)
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
            var cart = await _cartService.getCartDetilsByUserIdAsync(user.Id);
            if (cart == null) {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Cart notFound!" });
            }

            var cartItem = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
            if (cartItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Cart Item not found!" });
            }

            cart.Total -= cartItem.ProductPrice * cartItem.Quantity;

            await _cartItemService.RemoveCartItemAsync(itemId);

            var updatedCart = await _cartService.getCartDetilsByUserIdAsync(user.Id);
            updatedCart.Total = cart.Total;
            await _cartService.UpdateAsync(updatedCart);   

            return Ok(new Response { Status = "Success",Message = "Cart Item Remove Successfully."});
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("clearCartAsync/{userId}")]
    public async Task<IActionResult> ClearCartAsync(int userId)
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
            var cart = await _cartService.getCartDetilsByUserIdAsync(user.Id);
            if (cart == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Cart notFound!" });
            }
            cart.Total = 0;
            await _cartService.UpdateAsync(cart);
            await _cartItemService.RemoveCartItemsAsync(cart.Id);
            return Ok(new Response { Status = "Success", Message = "Cart Items Remove Successfully." });
        }
        catch(Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    private async Task UpdateExistingCart(Cart cart,CartItemAddViewModal cartItem,decimal productPrice)
    {
        cart.Total = cart.Total + productPrice;
        await _cartService.UpdateAsync(cart);
        var newCartItem = _mapper.Map<CartItem>(cartItem);
        newCartItem.CartId = cart.Id;
        newCartItem.ProductPrice = (int)productPrice;
        await _cartItemService.AddCartItemAsync(newCartItem);
    }

    private async Task CreateNewCart(int userId, CartAddViewModal cartAddViewModal, decimal productPrice)
    {
        var cart = _mapper.Map<Cart>(cartAddViewModal);
        cart.UserId = userId;        
        cart.Total = productPrice;
        cart.CartItems = new List<CartItem>
        {
                _mapper.Map<CartItem>(cartAddViewModal.CartItem)
        };
        foreach (var cartItem in cart.CartItems)
        {
            cartItem.ProductPrice = (int)productPrice;
        }
        await _cartService.AddAsync(cart);
    }
}