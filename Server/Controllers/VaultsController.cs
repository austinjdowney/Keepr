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
        Vault v = _vs.GetById(id);
        return Ok(v);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]

    public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsByVaultId(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault v = _vs.GetById(id);
        List<VaultKeepViewModel> vk = _vks.GetKeepsByVaultId(id);
        if (userInfo == null && v.IsPrivate == true)
        {
          throw new Exception("HEAVILY GUARDED VAULT");
        }
        if (v.IsPrivate == true)
        {
          if (userInfo.Id == v.CreatorId)
          {
            return Ok(vk);
          }
          throw new Exception("NOT YOUR VAULT");
        }
        return Ok(vk);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
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
    [HttpPut("{id}")]
    [Authorize]

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
    [HttpDelete("{id}")]
    [Authorize]

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