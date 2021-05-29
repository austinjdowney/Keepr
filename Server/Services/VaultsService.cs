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
      Vault vault = _vr.GetById(v.Id);
      if (vault == null)
      {
        throw new Exception("Invalid Id");
      }
      if (vault.CreatorId != id)
      {
        throw new Exception("NOT YOUR VAULT");
      }
      return _vr.Update(v);
    }

    internal void Delete(int id, string userId)
    {
      Vault vault = GetById(id);
      if (vault.CreatorId != userId)
      {
        throw new Exception("YOU CANT DELETE THIS VAULT.. NOT YOURS");
      }
      _vr.Delete(id);
    }

  }
}