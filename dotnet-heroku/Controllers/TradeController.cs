using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using System.Net;

namespace dotnet_heroku.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        //private readonly ITradeService _service;
        private readonly NinjaTraderContext _context;
        private SQLiteConnection _conn = null;

        public TradeController()
        {
           // _service = service;
        }        
        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TradeResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get() 
            => Ok(await GetTrades()); 

        public IActionResult Update(TradeRequest tradeRequest)
        { 
            Update(tradeRequest);
            return Ok();
        }

        public async Task<List<TradeDTO>> GetTrades()
        {
            using (_conn = BaseRepository.GetConnection())
            {
                await _conn.OpenAsync();

                try
                {
                    List<TradeDTO> tradesDTO = new List<TradeDTO>();

                    string query = @"SELECT 
                                        t.Id, 
                                        t.GananciaPerdida, 
                                        t.Cantidad, 
                                        t.PrecioEntrada, 
                                        t.UltimoPrecio, 
                                        e.Descripcion as Estrategia,
                                        t.TipoOperacion, 
                                        t.InstrumentId, 
                                        t.ExecutionId, 
                                        t.OrderID, 
                                        t.Observaciones,
                                        t.FechaHora,
                                        ten.Descripcion as Tendencia
                                FROM Trades t 
                                LEFT JOIN Estrategias e ON e.Id = t.IdEstrategia
                                LEFT JOIN Tendencias ten ON ten.Id = t.Tendencia ORDER BY t.Id Desc;";

                    SQLiteCommand cmd = new SQLiteCommand(query, _conn);
                    var reader = await cmd.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        TradeDTO tradeDTO = new TradeDTO
                        {

                            Id = Convert.ToInt64(reader["Id"]),
                            GananciaPerdida = reader["GananciaPerdida"] is DBNull ? 0 : Convert.ToDouble(reader["GananciaPerdida"]),
                            Cantidad = reader["Cantidad"] is DBNull ? (int?)null : Convert.ToInt32(reader["Cantidad"]),
                            PrecioEntrada = reader["PrecioEntrada"] is DBNull ? 0 : Convert.ToDouble(reader["PrecioEntrada"]),
                            UltimoPrecio = reader["UltimoPrecio"] is DBNull ? 0 : Convert.ToDouble(reader["UltimoPrecio"]),
                            Estrategia = reader["Estrategia"] is DBNull ? string.Empty : reader["Estrategia"].ToString(),
                            TipoOperacion = reader["TipoOperacion"] is DBNull ? string.Empty : reader["TipoOperacion"].ToString(),
                            InstrumentId = reader["InstrumentId"] is DBNull ? 0 : Convert.ToInt64(reader["InstrumentId"]),
                            ExecutionId = reader["ExecutionId"] is DBNull ? string.Empty : reader["ExecutionId"].ToString(),
                            OrderId = reader["OrderId"] is DBNull ? string.Empty : reader["OrderId"].ToString(),
                            Observaciones = reader["Observaciones"] is DBNull ? string.Empty : reader["Observaciones"].ToString(),
                            FechaHora = reader["FechaHora"] is DBNull ? (int?)null : Convert.ToInt64(reader["FechaHora"]),
                            Tendencia = reader["Tendencia"] is DBNull ? string.Empty : reader["Tendencia"].ToString(),

                        };
                        tradesDTO.Add(tradeDTO);
                    }
                    /*
                    var queryMovil = tradesDTO.AsQueryable();

                    if (patente != null)
                    {
                        queryMovil = queryMovil.Where(x => x.PATE_MOVI.ToLower().Contains(patente.ToLower()));
                    }

                    if (marca != null)
                    {
                        queryMovil = queryMovil.Where(x => x.NOMB_MOVI.ToLower().Contains(marca.ToLower()));
                    }

                    if (modelo != null)
                    {
                        queryMovil = queryMovil.Where(x => x.MODE_MOVI.ToString().Contains(modelo.ToString()));
                    }*/

                    _conn.Close();

                    return tradesDTO;

                }
                catch (Exception ex)
                {
                    _conn.Close();
                    throw ex;
                }
            }

        }

        public void Update(Trade trade)
        {
            using (_conn = BaseRepository.GetConnection())
            {
                string message = "";

                _conn.Open();
                SQLiteTransaction transaction = _conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    string query = "UPDATE Trades SET IdEstrategia =" + (trade.IdEstrategia == null ? 0 : trade.IdEstrategia) + ", Tendencia =" + (trade.Tendencia == null ? 0 : trade.Tendencia) + ", Observaciones = '" + trade.Observaciones + "' WHERE Id = " + trade.Id + ";";
                    SQLiteCommand comando = new SQLiteCommand(query, _conn);
                    comando.ExecuteNonQuery();

                    transaction.Commit();
                    _conn.Close();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _conn.Close();
                    throw ex;
                }
            }

        }

    }
}