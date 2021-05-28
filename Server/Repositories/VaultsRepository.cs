using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Server.Models;

namespace Server.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Vault> GetAll()
    {
      string sql = @" 
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, splitOn: "id"
      ).ToList();
    }

    internal Vault GetById(int id)
    {
      string sql = @"SELECT 
      v.*,
      a.* 
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id=@id";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal Vault Create(Vault v)
    {
      string sql = @"
                INSERT INTO 
                vaults(name, description, img, creatorId, isPrivate )
                VALUES (@Name, @Description, @Img, @CreatorId, @IsPrivate);
                SELECT LAST_INSERT_ID();
            ";
      v.Id = _db.ExecuteScalar<int>(sql, v);
      return v;
    }
  }
}