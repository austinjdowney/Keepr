using System;
using System.Collections.Generic;
using Server.Models;
using Server.Repositories;

namespace Server.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    public List<Keep> GetKeeps()
    {
      return _kr.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep keep = _kr.GetById(id);
      if (keep == null)
      {
        throw new Exception("invalid id");
      }
      return keep;
    }

    internal Keep Create(Keep k)
    {
      return _kr.Create(k);
    }

    internal Keep Update(Keep k, string id)
    {
      Keep keep = _kr.GetById(k.Id);
      if (keep == null)
      {
        throw new Exception("Invalid Id");
      }
      if (keep.CreatorId != id)
      {
        throw new Exception("NOT YOUR KEEP");
      }
      return _kr.Update(k);
    }

    //passing in string for userId to compare against the creatorId to make certain the person who is logged in is the one who created it
    internal void Delete(int id, string userId)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("YOU CANT DELETE THIS KEEP.. NOT YOURS");
      }
      _kr.Delete(id);
    }


  }
}