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

    internal Vault GetById(int id)
    {
      string sql = @"SELECT 
      v.*,
      a.* 
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id=@Id";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal List<Vault> GetVaultsByProfileId(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId= @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) => { v.Creator = p; return v; }, new { id }, splitOn: "id").ToList();
    }



    internal Vault Create(Vault v)
    {
      string sql = @"
                INSERT INTO 
                vaults(name, description, img, isPrivate, creatorId )
                VALUES (@Name, @Description, @Img, @IsPrivate, @CreatorId);
                SELECT LAST_INSERT_ID();
            ";
      v.Id = _db.ExecuteScalar<int>(sql, v);
      return v;
    }

    internal Vault Update(Vault v)
    {
      string sql = @"
            UPDATE vaults 
            SET 
                name = @Name,
                description = @Description,
                img = @Img,
                isPrivate = @IsPrivate
            WHERE id = @Id;
            ";
      _db.Execute(sql, v);
      return v;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}