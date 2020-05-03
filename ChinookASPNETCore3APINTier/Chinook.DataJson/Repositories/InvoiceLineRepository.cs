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
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly SqlConnection _sqlconn;

        public InvoiceLineRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool InvoiceLineExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckInvoiceLine", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceLineId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<InvoiceLine> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as List<InvoiceLine>;
            return converted;
        }

        public InvoiceLine GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceLineId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as List<InvoiceLine>;

            return converted.FirstOrDefault();
        }

        public List<InvoiceLine> GetByInvoiceId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceLineByInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as List<InvoiceLine>;
            return converted;
        }

        public List<InvoiceLine> GetByTrackId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceLineByTrack", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("TrackId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            try
            {
                var converted =
                    JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as List<InvoiceLine>;
                return converted;
            }
            catch(JsonException)
            {
                return null;
            }
        }

        public InvoiceLine Add(InvoiceLine newInvoiceLine)
        {
            return newInvoiceLine;
        }

        public bool Update(InvoiceLine invoiceLine)
        {
            if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
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