using System;
using System.Collections.Generic;
using Server.Models;
using Server.Repositories;

namespace Server.Services
{
  public class AccountService
  {
    private readonly AccountRepository _repo;
    private readonly KeepsRepository _kr;
    private readonly VaultsRepository _vr;


    public AccountService(AccountRepository repo, KeepsRepository kr, VaultsRepository vr)
    {
      _repo = repo;
      _kr = kr;
      _vr = vr;

    }

    internal string GetProfileEmailById(string id)
    {
      return _repo.GetById(id).Email;
    }
    internal Account GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Profile GetProfileById(string id)
    {
      return _repo.GetById(id);
    }

    internal List<Keep> GetKeepsByProfileId(int profileId)
    {
      return _kr.GetKeepsByProfileId(profileId);
    }

    internal List<Vault> GetVaultsByProfileId(int profileId)
    {
      return _vr.GetKeepsByProfileId(profileId);
    }

    internal Account Edit(Account editData, string userEmail)
    {
      Account original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }
  }
}