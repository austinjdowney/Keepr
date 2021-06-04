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
    internal List<VaultKeepViewModel> GetAll()
    {
      string sql = @"
      SELECT * FROM vaultkeeps";
      return _db.Query<VaultKeepViewModel>(sql).ToList();
    }

    internal VaultKeepViewModel GetById(int id)
    {
      string sql = @"
      SELECT
       k.*,
      vk.id as vaultKeepId,
      vk.keepId as keepId,
      vk.vaultId as vaultId,
      a.*
      FROM vaultkeeps vk
      JOIN keeps k on k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vk.id = @id;";
      // string sql = " SELECT * FROM vaultkeeps WHERE id=@id";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (k, p) => { k.Creator = p; return k; }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal VaultKeep Create(VaultKeep vk)
    {
      // UPDATE keeps 
      //   SET 
      //   keeps = keeps + 1
      // WHERE id = @Id;
      string sql = @"

        INSERT INTO vaultkeeps
        (creatorId, keepId, vaultId)
        VALUES
        (@CreatorId, @KeepId, @VaultId);
        SELECT LAST_INSERT_ID();";
      vk.Id = _db.ExecuteScalar<int>(sql, vk);
      return vk;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @Id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT
      k.*,
      vk.id AS VaultKeepId,
      a.*
      FROM vaultkeeps vk
      JOIN keeps k on vk.keepId = k.id
      JOIN accounts a ON a.id = k.creatorId
      WHERE vaultId = @id;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, p) => { vk.Creator = p; return vk; }, new { id }, splitOn: "id").ToList();
    }
  }
}