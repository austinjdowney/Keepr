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

    internal Vault GetById(int id)
    {
      Vault vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("invalid id");
      }
      return vault;
    }

    internal Vault Create(Vault v)
    {
      return _vr.Create(v);
    }

    internal Vault Update(Vault v, string id)
    {
      throw new NotImplementedException();
    }

    internal void Delete(int id1, string id2)
    {
      throw new NotImplementedException();
    }
  }
}