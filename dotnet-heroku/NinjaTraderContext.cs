using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public partial class NinjaTraderContext : DbContext
{
    public NinjaTraderContext()
    {
    }
    public IConfiguration Configuration { get; }

    public NinjaTraderContext(DbContextOptions<NinjaTraderContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountItem> AccountItems { get; set; }

    public virtual DbSet<Execution> Executions { get; set; }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<InstrumentList> InstrumentLists { get; set; }

    public virtual DbSet<JournalEntry> JournalEntries { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MasterInstrument> MasterInstruments { get; set; }

    public virtual DbSet<Estrategia> Estrategias { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderUpdate> OrderUpdates { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Strategy> Strategies { get; set; }

    public virtual DbSet<Strategy2Account> Strategy2Accounts { get; set; }

    public virtual DbSet<Strategy2Instrument> Strategy2Instruments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<User2Account> User2Accounts { get; set; }

    public virtual DbSet<User2MarketDataEntitlement> User2MarketDataEntitlements { get; set; }

    public virtual DbSet<Version> Versions { get; set; }
    public virtual DbSet<Trade> Trades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=\\LAPTOP-GQHLV798\\Documentos\\NinjaTrader 8\\db\\NinjaTrader.sqlite;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.Fcm, e.Provider }, "Accounts_ui0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccountStatus).HasColumnType("INT");
            entity.Property(e => e.DisplayName).HasColumnType("text(250)");
            entity.Property(e => e.Fee).HasColumnType("text(50)");
            entity.Property(e => e.LiquidationState).HasColumnType("INT");
            entity.Property(e => e.MaxOrderSize).HasColumnType("INT");
            entity.Property(e => e.MaxPositionSize).HasColumnType("INT");
        });

        modelBuilder.Entity<AccountItem>(entity =>
        {
            entity.HasKey(e => new { e.Account, e.ItemType, e.Currency });

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.AccountItems).HasForeignKey(d => d.Account);
        });


        modelBuilder.Entity<Execution>(entity =>
        {
            entity.HasIndex(e => e.Instrument, "Executions_i0");

            entity.HasIndex(e => e.Account, "Executions_i1");

            entity.HasIndex(e => e.Time, "Executions_i2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ServerName).HasColumnType("text(20)");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.Executions).HasForeignKey(d => d.Account);

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.Executions).HasForeignKey(d => d.Instrument);
        });

        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.HasIndex(e => e.MasterInstrument, "Instruments_i0");

            entity.HasIndex(e => new { e.MasterInstrument, e.Exchange, e.Expiry, e.StrikePrice, e.Right }, "Instruments_ui0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MasterInstrumentNavigation).WithMany(p => p.Instruments).HasForeignKey(d => d.MasterInstrument);

            entity.HasMany(d => d.InstrumentLists).WithMany(p => p.Instruments)
                .UsingEntity<Dictionary<string, object>>(
                    "Instrument2InstrumentList",
                    r => r.HasOne<InstrumentList>().WithMany().HasForeignKey("InstrumentList"),
                    l => l.HasOne<Instrument>().WithMany().HasForeignKey("Instrument"),
                    j =>
                    {
                        j.HasKey("Instrument", "InstrumentList");
                        j.ToTable("Instrument2InstrumentList");
                        j.HasIndex(new[] { "InstrumentList" }, "Instrument2InstrumentList_i0");
                    });
        });

        modelBuilder.Entity<InstrumentList>(entity =>
        {
            entity.HasIndex(e => e.Name, "InstrumentLists_ui0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasIndex(e => e.Execution, "JournalEntries_i0");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ExecutionNavigation).WithMany(p => p.JournalEntries)
                .HasForeignKey(d => d.Execution)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.Time, "Logs_i0");

            entity.HasIndex(e => e.User, "Logs_i1");

            entity.Property(e => e.LogCategory).HasColumnType("INT");
            entity.Property(e => e.LogLevel).HasColumnType("INT");
            entity.Property(e => e.ServerName).HasColumnType("text(20)");
        });

        modelBuilder.Entity<MasterInstrument>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.InstrumentType }, "MasterInstruments_ui0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Estrategia>();

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.Instrument, "Orders_i0");

            entity.HasIndex(e => e.Account, "Orders_i1");

            entity.HasIndex(e => e.OrderId, "Orders_i2");

            entity.HasIndex(e => e.Time, "Orders_i3");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.Orders).HasForeignKey(d => d.Account);

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.Orders).HasForeignKey(d => d.Instrument);
        });

        modelBuilder.Entity<OrderUpdate>(entity =>
        {
            entity.HasKey(e => new { e.Order, e.Nr });

            entity.Property(e => e.ServerName).HasColumnType("text(20)");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.OrderUpdates).HasForeignKey(d => d.Order);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.Instrument, "Positions_i0");

            entity.HasOne(d => d.AccountNavigation).WithMany().HasForeignKey(d => d.Account);

            entity.HasOne(d => d.InstrumentNavigation).WithMany().HasForeignKey(d => d.Instrument);
        });

        modelBuilder.Entity<Strategy>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Executions).WithMany(p => p.Strategies)
                .UsingEntity<Dictionary<string, object>>(
                    "Strategy2Execution",
                    r => r.HasOne<Execution>().WithMany().HasForeignKey("Execution"),
                    l => l.HasOne<Strategy>().WithMany().HasForeignKey("Strategy"),
                    j =>
                    {
                        j.HasKey("Strategy", "Execution");
                        j.ToTable("Strategy2Execution");
                        j.HasIndex(new[] { "Execution" }, "Strategy2Execution_i0");
                    });

            entity.HasMany(d => d.Orders).WithMany(p => p.Strategies)
                .UsingEntity<Dictionary<string, object>>(
                    "Strategy2Order",
                    r => r.HasOne<Order>().WithMany().HasForeignKey("Order"),
                    l => l.HasOne<Strategy>().WithMany().HasForeignKey("Strategy"),
                    j =>
                    {
                        j.HasKey("Strategy", "Order");
                        j.ToTable("Strategy2Order");
                        j.HasIndex(new[] { "Order" }, "Strategy2Order_i0");
                    });
        });

        modelBuilder.Entity<Strategy2Account>(entity =>
        {
            entity.HasKey(e => new { e.Strategy, e.Account });

            entity.ToTable("Strategy2Account");

            entity.HasIndex(e => e.Account, "Strategy2Account_i0");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.Strategy2Accounts).HasForeignKey(d => d.Account);

            entity.HasOne(d => d.StrategyNavigation).WithMany(p => p.Strategy2Accounts).HasForeignKey(d => d.Strategy);
        });

        modelBuilder.Entity<Strategy2Instrument>(entity =>
        {
            entity.HasKey(e => new { e.Strategy, e.Instrument, e.Nr });

            entity.ToTable("Strategy2Instrument");

            entity.HasIndex(e => e.Instrument, "Strategy2Instrument_i0");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.Strategy2Instruments).HasForeignKey(d => d.Instrument);

            entity.HasOne(d => d.StrategyNavigation).WithMany(p => p.Strategy2Instruments).HasForeignKey(d => d.Strategy);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Name, "Users_ui0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasColumnType("text(100)");
            entity.Property(e => e.Auth0UserId).HasColumnType("text(100)");
            entity.Property(e => e.City).HasColumnType("text(100)");
            entity.Property(e => e.Company).HasColumnType("text(100)");
            entity.Property(e => e.Country).HasColumnType("text(100)");
            entity.Property(e => e.CreatedByProvider).HasColumnType("INT");
            entity.Property(e => e.Description).HasColumnType("text(250)");
            entity.Property(e => e.Email1).HasColumnType("text(100)");
            entity.Property(e => e.Email2).HasColumnType("text(100)");
            entity.Property(e => e.Email3).HasColumnType("text(100)");
            entity.Property(e => e.FirstName).HasColumnType("text(100)");
            entity.Property(e => e.LastName).HasColumnType("text(100)");
            entity.Property(e => e.ManagedProvider).HasColumnType("INT");
            entity.Property(e => e.MaxMarketDataSubscriptions).HasColumnType("INT");
            entity.Property(e => e.MaxMarketDepthSubscriptions).HasColumnType("INT");
            entity.Property(e => e.NumSimulationAccounts).HasColumnType("INT");
            entity.Property(e => e.Phone).HasColumnType("text(30)");
            entity.Property(e => e.PostalCode).HasColumnType("text(20)");
            entity.Property(e => e.State).HasColumnType("text(100)");
        });

        modelBuilder.Entity<User2Account>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User2Account");

            entity.HasIndex(e => e.User, "User2Account_i0");

            entity.HasOne(d => d.AccountNavigation).WithMany().HasForeignKey(d => d.Account);

            entity.HasOne(d => d.UserNavigation).WithMany().HasForeignKey(d => d.User);
        });

        modelBuilder.Entity<User2MarketDataEntitlement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User2MarketDataEntitlement");

            entity.HasIndex(e => e.User, "User2MarketDataEntitlement_i0");

            entity.Property(e => e.MarketDataEntitlement).HasColumnType("text(250)");

            entity.HasOne(d => d.UserNavigation).WithMany().HasForeignKey(d => d.User);
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.Version1);

            entity.Property(e => e.Version1)
                .ValueGeneratedNever()
                .HasColumnName("Version");
        });

        modelBuilder.Entity<Trade>(entity =>
        {
            entity.HasKey(e => new { e.Id });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
