using System;
using System.Collections.Generic;
using Server.Models;
using Server.Repositories;

namespace Server.Services
{
  public class VaultsService
  {

    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    public List<Vault> GetVaults()
    {
      return _vr.GetAll();
    }

    internal Vault GetById(int id)
    {
      Vault vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("invalid id");
      }
      return vault;
    }
  }
}