namespace TEST.AMRCloud.Services.Scoping.Domain.Common;

/// <summary>
/// Centralized constants for the Scoping service.
/// All shared constants must be defined here to avoid magic strings across the codebase.
/// </summary>
public static class ApplicationConstant
{
    public const string ScopingCompletedStatus = "Completed";
    public const string ScopingPendingStatus = "Pending";
    public const string ScopingInProgressStatus = "InProgress";
    public const string ScopingRejectedStatus = "Rejected";

    public const int DefaultPageSize = 50;
    public const int MaxPageSize = 500;

    public const string DefaultSortField = "CreatedDate";

    // Engagement statuses
    public const string EngagementActiveStatus = "Active";
    public const string EngagementClosedStatus = "Closed";

    // Fund statuses
    public const string FundActiveStatus = "Active";
    public const string FundInactiveStatus = "Inactive";
}
