using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpGet("{id}")]

    public ActionResult<Vault> GetOneVault(int id)
    {
      try
      {
        Vault vault = _vs.GetById(id);
        return Ok(vault);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]

    public ActionResult<List<VaultKeepViewModel>> GetKeepsByVaultId(int id)
    {
      try
      {
        List<VaultKeepViewModel> vkvm = _vks.GetKeepsByVaultId(id);
        return Ok(vkvm);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault v)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        v.CreatorId = userInfo.Id;
        Vault newVault = _vs.Create(v);
        newVault.Creator = userInfo;
        return Ok(newVault);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault v)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        v.Id = id;
        Vault newV = _vs.Update(v, userInfo.Id);
        newV.Creator = userInfo;
        return Ok(newV);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{id}")]

    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vs.Delete(id, userInfo.Id);
        return Ok("Successfully Deleted Vault");

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}