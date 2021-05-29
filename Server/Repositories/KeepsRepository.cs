using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Server.Models;

namespace Server.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @" 
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id"
      ).ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
            SELECT 
                k.*, 
                a.* 
            FROM keeps k
            JOIN accounts a ON a.id = k.CreatorId
            WHERE k.id = @id";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId;";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) => { k.CreatorId = p.Id; return k; }, new { profileId }, splitOn: "id").ToList();
    }


    internal Keep Create(Keep k)
    {
      string sql = @"
                INSERT INTO 
                keeps(name, description, img, views, shares, keeps, creatorId)
                VALUES (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
                SELECT LAST_INSERT_ID();
            ";
      k.Id = _db.ExecuteScalar<int>(sql, k);
      return k;
    }


    internal Keep Update(Keep k)
    {
      throw new NotImplementedException();
    }

    internal void Delete(int id)
    {
      throw new NotImplementedException();
    }
  }
}