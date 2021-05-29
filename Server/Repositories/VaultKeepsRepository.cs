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

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT k.*,
      vk.id as VKeepId,
      a.*
      FROM vaultkeeps vk
      JOIN keeps k on k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vaultId = @Vaultid;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (k, p) => { k.Creator = p; return k; }, new { id }, splitOn: "id").ToList();
    }

  }
}