using CashFlow.Service.Configurations;
using CashFlow.Service.Interfaces;
using CashFlow.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Api.Controllers;

public class UserAssetsController : BaseController
{
    private readonly IUserAssetService _userAssetService;

    public UserAssetsController(IUserAssetService userAssetService)
    {
        _userAssetService = userAssetService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> PostAsync(
    [Required(ErrorMessage = "Please, select file ...")]
    [DataType(DataType.Upload)] IFormFile file)
    => Ok(await _userAssetService.AddAsync(file));

    [HttpGet("{user-id}")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, [FromRoute] long userId)
        => Ok(await _userAssetService.RetrieveAllAsync(userId, @params));

    [HttpGet("{user-id}/{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "userId")] long userId, [FromRoute(Name = "id")] long id)
        => Ok(await _userAssetService.RetrieveByIdAsync(userId, id));

    [HttpDelete("{user-id}/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "userId")] long userId, [FromRoute(Name = "id")] long id)
        => Ok(await _userAssetService.RemoveAsync(userId, id));
}