using System;
using System.Collections.Generic;
using Server.Models;
using Server.Repositories;

namespace Server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    private readonly KeepsRepository _kr;

    public VaultKeepsService(VaultKeepsRepository vkr, KeepsRepository kr)
    {
      _vkr = vkr;
      _kr = kr;
    }

    internal List<VaultKeepViewModel> GetAllVaultKeeps()
    {
      return _vkr.GetAll();
    }

    internal VaultKeepViewModel GetById(int id)
    {
      VaultKeepViewModel vaultkeep = _vkr.GetById(id);
      if (vaultkeep == null)
      {
        throw new Exception("invalid id");
      }
      return vaultkeep;
    }
    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      return _vkr.GetKeepsByVaultId(id);
    }

    internal VaultKeep Create(VaultKeep vk)
    {
      Keep keep = _kr.GetById(vk.KeepId);
      keep.Keeps++;
      _kr.Update(keep);
      return _vkr.Create(vk);
    }

    internal void Delete(int id, string userId)
    {
      VaultKeepViewModel vkeep = _vkr.GetById(id);
      if (vkeep.CreatorId != userId)
      {
        throw new Exception("YOU CANT DELETE THIS KEEP.. NOT YOURS");
      }
      _vkr.Delete(id);
    }
  }
}