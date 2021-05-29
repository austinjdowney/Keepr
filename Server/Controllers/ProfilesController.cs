using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;
using Server.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {

    private readonly AccountService _acnts;
    public ProfilesController(AccountService acnts)
    {
      _acnts = acnts;
    }


    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(string id)
    {
      try
      {
        Profile p = _acnts.GetProfileById(id);
        return Ok(p);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
    {
      try
      {
        List<Keep> keeps = _acnts.GetKeepsByProfileId(profileId);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
    {
      try
      {
        List<Vault> vaults = _acnts.GetVaultsByProfileId(profileId);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}