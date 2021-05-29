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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpGet]
    public ActionResult<List<VaultKeep>> GetAllVaultKeeps()
    {
      try
      {
        List<VaultKeep> vaultkeeps = _vks.GetAllVaultKeeps();
        return Ok(vaultkeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<VaultKeep> GetOneVaultKeep(int id)
    {
      try
      {
        VaultKeep vaultkeep = _vks.GetById(id);
        return Ok(vaultkeep);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vk)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vk.CreatorId = userInfo.Id;
        VaultKeep newVK = _vks.Create(vk);
        newVK.CreatorId = userInfo.Id;
        return Ok(newVK);
      }
      catch (System.Exception e)
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
        _vks.Delete(id, userInfo.Id);
        return Ok("Successfully Deleted VK");

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}