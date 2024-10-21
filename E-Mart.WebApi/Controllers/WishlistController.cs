using AutoMapper;
using E_Mart.Domain.Users;
using E_Mart.Domain.Wishlists;
using E_Mart.WebApi.Models.Response;
using E_Mart.WebApi.Models.Wishlist;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly UserService _userService;
    private readonly WishlistService _wishlistService;
    private readonly IMapper _mapper;
    public WishlistController(UserService userService, WishlistService wishlistService, IMapper mapper)
    {
        _userService = userService;
        _wishlistService = wishlistService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("getWishListDetailsAsync")]
    public async Task<IActionResult> GetWishListDetailsAsync()
    {
        try
        {
            var getWishlists = await _wishlistService.GetAllAsync();
            var wishlists = _mapper.Map<List<WishlistViewModal>>(getWishlists);
            return Ok(wishlists);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("getWishListDetailsByUserAsync/{userId}")]
    public async Task<IActionResult> GetWishListDetailsByUserAsync(int userId)
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
            var getWishListByUser = await _wishlistService.getwishListsByUserAsync(userId);
            if (getWishListByUser == null)

            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Wishlist is Empty!" });
            }
            var wishLists = _mapper.Map<List<WishlistViewModal>>(getWishListByUser);
            return Ok(wishLists);
        }
        catch(Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost]
    [Route("addWishListAsync")]
    public async Task<IActionResult> addWishListAsync(WishlistAddViewModal wishlistAddViewModal)
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
            var wishlist = _mapper.Map<wishlist>(wishlistAddViewModal);
            wishlist.UserId = user.Id;
            _wishlistService.AddAsync(wishlist);
            return Ok(new Response { Status = "Success", Message = "Product Add Successfully in Wishlist." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("removeWishlistItemAsync/{itemId}")]
    public async Task<IActionResult> RemoveWishlistItemAsync(int itemId)
    {
        try
        {
            await _wishlistService.SoftDeleteAsync(itemId);
            return Ok(new Response { Status = "Success", Message = "Product Item Remove Successfully." });
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("clearWishlistAsync/{userId}")]
    public async Task<IActionResult> ClearWishlistAsync(int userId)
    {
        try
        {
            await _wishlistService.ClearWishlistAsync(userId);
            return Ok(new Response { Status = "Success", Message = "Product Item's Remove Successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }
}