using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly SqlConnection _sqlconn;

        public MediaTypeRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool MediaTypeExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckMediaType", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("MediaTypeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<MediaType> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetMediaType", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<MediaType>)) as List<MediaType>;
            return converted;
        }

        public MediaType GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetMediaTypeDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("MediaTypeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<MediaType>)) as List<MediaType>;

            return converted.FirstOrDefault();
        }

        public MediaType Add(MediaType newMediaType)
        {
            return newMediaType;
        }

        public bool Update(MediaType mediaType)
        {
            if (!MediaTypeExists(mediaType.MediaTypeId))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}