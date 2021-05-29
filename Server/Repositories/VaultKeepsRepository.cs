using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Server.Models;

namespace Server.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<VaultKeep> GetAll()
    {
      string sql = @"
      SELECT * FROM vaultKeeps";
      return _db.Query<VaultKeep>(sql).ToList();
    }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal VaultKeep Create(VaultKeep vk)
    {
      string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, keepId, vaultId)
        VALUES
        (@CreatorId, @KeepId, @VaultId);
        SELECT LAST_INSERT_ID();";
      vk.Id = _db.ExecuteScalar<int>(sql, vk);
      return vk;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}