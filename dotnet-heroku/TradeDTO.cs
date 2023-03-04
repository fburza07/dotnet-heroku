
    public class TradeDTO
    {
        public long Id { get; set; }

        public double? GananciaPerdida { get; set; }

        public int? Cantidad { get; set; }

        public double? PrecioEntrada { get; set; }

        public double? UltimoPrecio { get; set; }

        public string? Estrategia { get; set; }

        public string? TipoOperacion { get; set; }

        public long InstrumentId { get; set; }

        public string? ExecutionId { get; set; }

        public string? OrderId { get; set; }

        public string? Observaciones { get; set; }

        public long? FechaHora { get; set; }

        public string? Tendencia { get; set; }
}
