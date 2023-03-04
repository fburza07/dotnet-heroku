using System;
using System.Collections.Generic;


public partial class User
{
    public long Id { get; set; }

    public long? IsMonitor { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public long? ManagesAllAccounts { get; set; }

    public long? MaxConcurrentLogins { get; set; }

    public string? Address { get; set; }

    public long? CanManageAccounts { get; set; }

    public long? CanManageMessages { get; set; }

    public long? CanManagePermissions { get; set; }

    public long? CanManagePlatform { get; set; }

    public long? CanManageRisk { get; set; }

    public long? CanManageTreasury { get; set; }

    public long? CanManageUsers { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Email1 { get; set; }

    public string? Email2 { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PostalCode { get; set; }

    public string? State { get; set; }

    public long? IsEnabled { get; set; }

    public long? IsProfessional { get; set; }

    public long? NumSimulationAccounts { get; set; }

    public double? ApiRateMultiplier { get; set; }

    public long? MaxMarketDataSubscriptions { get; set; }

    public long? MaxMarketDepthSubscriptions { get; set; }

    public long? CanRunReports { get; set; }

    public byte[]? UserData { get; set; }

    public long? CanViewAccounts { get; set; }

    public long? CanViewUsers { get; set; }

    public string? Email3 { get; set; }

    public string? Company { get; set; }

    public long? CqgPurchaseEligible { get; set; }

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public long? ManagedProvider { get; set; }

    public long? CreatedByProvider { get; set; }

    public long? RegisteredTime { get; set; }

    public long? ExpirationTime { get; set; }

    public string? Auth0UserId { get; set; }
}
