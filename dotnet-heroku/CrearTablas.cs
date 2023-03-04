using System.Data.SQLite;

namespace WebUI
{
    public class CrearTablas : BaseRepository
    {
        private SQLiteConnection _conn = null;
        public CrearTablas(IConfiguration configuration) : base(configuration)
        {
        }
        public async void CrearTablasInicio()
        {

            using (_conn = GetConnection())
            {
                await _conn.OpenAsync();

                try
                {
                    if (!ExisteTabla("Tendencias", _conn))
                    {
                        string createTableQuery = "CREATE TABLE Tendencias (Id INTEGER PRIMARY KEY AUTOINCREMENT, Descripcion TEXT);";
                        SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, _conn);

                        createTableCommand.ExecuteNonQuery();
                    }

                    if (!ExisteTabla("Estrategias", _conn))
                    {
                        string createTableQuery = "CREATE TABLE Estrategias (Id INTEGER PRIMARY KEY AUTOINCREMENT, Descripcion TEXT);";
                        SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, _conn);

                        createTableCommand.ExecuteNonQuery();
                    }

                    if (!ExisteTabla("Trades", _conn))
                    {
                        string createTableQuery = @"CREATE TABLE Trades (Id INTEGER PRIMARY KEY AUTOINCREMENT, 
					                                                    GananciaPerdida DECIMAL(18,2), 
					                                                    Cantidad INTEGER, 
					                                                    PrecioEntrada DECIMAL(18,2), 
					                                                    UltimoPrecio DECIMAL(18,2),
					                                                    IdEstrategia INTEGER, 
					                                                    TipoOperacion TEXT,
					                                                    InstrumentId INTEGER,
					                                                    ExecutionId TEXT,
					                                                    OrderId TEXT,
                                                                        Observaciones TEXT,
                                                                        FechaHora DATETIME);";
                        SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, _conn);

                        createTableCommand.ExecuteNonQuery();
                    }

                    if (ExisteTrigger(_conn, "insertar_trades"))
                    {

                        string createTrigger1Query = @"CREATE TRIGGER insertar_trades
                                                        AFTER INSERT ON Executions
                                                        FOR EACH ROW                                                                                                                                                         
                                                        WHEN NEW.IsEntry = 1 
                                                        BEGIN 									

	                                                        INSERT INTO Trades (GananciaPerdida, Cantidad, PrecioEntrada, UltimoPrecio,TipoOperacion, InstrumentId, ExecutionId, OrderId, FechaHora)
	                                                        SELECT 0 as GananciaPerdida, o.Filled as Cantidad, e.price AS PrecioEntrada, e.price AS UltimoPrecio, CASE WHEN e.Position < 0 THEN 'SHORT' ELSE 'LONG' END as TipOperacion, e.Instrument as InstrumentID, e.ExecutionId, e.OrderId, CAST((((JulianDay('now', 'localtime') - 2440587.5)*86400.0) + 62135596800) * 10000000 AS BIGINT)  as FechaHora
	                                                        FROM Executions e 
		                                                        INNER JOIN Orders o ON o.OrderId = e.OrderId
	                                                        WHERE e.Id = NEW.Id AND NOT EXISTS (SELECT 1 FROM Trades tr WHERE tr.OrderId = NEW.OrderId);

                                                        END";
                        SQLiteCommand createTrigger1Command = new SQLiteCommand(createTrigger1Query, _conn);

                        createTrigger1Command.ExecuteNonQuery();
                    }

                    if (ExisteTrigger(_conn, "actualizar_trades"))
                    {

                        string createTableQuery = @"CREATE TRIGGER actualizar_trades
	                                                AFTER INSERT ON Executions
	                                                FOR EACH ROW                                                                                                                                                         
	                                                WHEN NEW.IsEntry = 0 
	                                                BEGIN
	
		                                                UPDATE Trades SET Cantidad = (SELECT o.Filled FROM Orders o WHERE o.OrderId = OrderId), UltimoPrecio = NEW.price, GananciaPerdida = GananciaPerdida + 
			                                                (SELECT CASE WHEN TipoOperacion = 'LONG' THEN 
						                                                (NEW.price - t.PrecioEntrada) * NEW.Quantity
					                                                ELSE 
					                                                    (t.PrecioEntrada - NEW.price) * NEW.Quantity
					                                                END
			                                                 FROM Trades t			 
			                                                 WHERE t.id = (SELECT MAX(id) FROM Trades))
		                                                WHERE Id = (SELECT MAX(id) FROM Trades);  
		
	                                                END;";
                        SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, _conn);

                        createTableCommand.ExecuteNonQuery();
                    }

                    _conn.Close();

                }
                catch (Exception ex)
                {
                    _conn.Close();
                    throw ex;
                }
            }
        }

        private bool ExisteTabla(string name, SQLiteConnection _conn)
        {
            var cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{name}'", _conn);
            var reader = cmd.ExecuteReader();
            return reader.HasRows;

        }

        private bool ExisteTrigger(SQLiteConnection _conn, string NombreTrigger)
        {
            var cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type = 'trigger' AND name = '" + NombreTrigger + "'", _conn);
            var reader = cmd.ExecuteReader();
            return reader.HasRows;
         }

    }

}

