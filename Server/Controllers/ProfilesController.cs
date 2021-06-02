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
    private readonly VaultsService _vs;

    public ProfilesController(AccountService acnts, VaultsService vs)
    {
      _acnts = acnts;
      _vs = vs;
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
    public ActionResult<List<Keep>> GetKeepsByProfile(string id)
    {
      try
      {
        List<Keep> keeps = _acnts.GetKeepsByProfileId(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfileId(string id)
    {
      try
      {

        List<Vault> vaults = _acnts.GetVaultsByProfileId(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}