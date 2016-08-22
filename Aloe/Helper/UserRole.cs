

using System;
using System.EnterpriseServices;
using System.Reflection;
using System.Runtime.Serialization;


namespace Aloe.Helper
{
    /// <summary>
    /// WorkFlow/App Status
    /// </summary>
    public enum Status
    {
        Initial     = 0,
        Modified    = 1,
        Accept      = 2,
        Reject      = 3,
        InUse       = 4,
        Review      = 5,
        Amendment   = 6,
        Completed   = 7, 
        Finished    = 8,
        Created     = 9,
        Cancelled   = 10
    }
    /// <summary>
    /// Transaction Status
    /// </summary>
    public enum TStatus
    {
        Initial             = 0,
        Saved               = 1,
        Updated             = 2,
        Reverted            = 3,
        Error               = 4,
        InProgress          = 5,
        Completed           = 6,
        Finished            = 7,
        Cancel              = 8,
        InCompleteReview    = 9
    }
    /// <summary>
    /// Transaction Status Messages
    /// </summary>
    public enum TStatusMsg
    {
        [EnumMember(Value = null)]
        Initial = TStatus.Initial,
        [EnumMember(Value = "Records saved successfully")]
        Saved = TStatus.Saved,
        [EnumMember(Value = "Records updated successfully")]
        Updated = TStatus.Updated,
        [EnumMember(Value = "Deal is closed")]
        Finished = TStatus.Finished,
        [EnumMember(Value = "Deal is Canceled")]
        Cancelled = TStatus.Cancel,
        [EnumMember(Value = "Some errors occured")]
        Error = TStatus.Error,
        [EnumMember(Value = "Some deal points need to review")]
        InCompleteReview = TStatus.InCompleteReview

    }
    /// <summary>
    /// User Permissions
    /// </summary>
    public enum UPermission
    {
        CreateDeal          = 0,
        UpdateDeal          = 1,
        UpdateIntialTerms   = 2,   // Amendment
        UpdateClientReview  = 3,
        Amendment           = 4,
        DealCancel          = 5
    }
    /// <summary>
    /// User Permissions Messages
    /// </summary>
    public enum UPermissionMsg
    {
        [EnumMember(Value = "You are not allowed to create a Deal")]
        UCreateDeal = UPermission.CreateDeal,
        [EnumMember(Value = "You are not allowed to update a Deal")]
        UpdateDeal= UPermission.UpdateDeal,
        [EnumMember(Value = "You are not allowed to update Initial Terms")]
        UpdateIntialTerms = UPermission.UpdateIntialTerms,
        [EnumMember(Value = "You are not allowed to update Client Reviews")]
        UpdateClientReview = UPermission.UpdateClientReview,
        [EnumMember(Value = "You are not allowed to make Amendments")]
        Amendment = UPermission.Amendment,
        [EnumMember(Value = "You are not allowed to cancel the Deal")]
        DealCancel = UPermission.DealCancel

    }
    /// <summary>
    /// Deal Messages Status
    /// </summary>
    public enum DMsgStatus
    {
        Unread      = 11,
        Read        = 12,
        Inactive    = 13
    }
    /// <summary>
    /// Deal Messages
    /// </summary>
    public enum DealMessage
    {
        [EnumMember(Value = "Your unread messages")]
        Unread = DMsgStatus.Unread,
        [EnumMember(Value = "Your read messages")]
        Read = DMsgStatus.Unread,
        [EnumMember(Value = "Your inactive messages")]
        Inactive = DMsgStatus.Inactive
    }
    public enum AppRole
    {
        Landlord = Roles.SuperUser,
        Listing_Agent = Roles.Admin,
        Tenant_Agent = Roles.User,
        Tenant = Roles.Guest
    }
    public enum Roles
    {
        /// <summary> Listing/Landlord Agent </summary>
        Admin,
        /// <summary> landlord </summary>
        SuperUser,
        /// <summary> User : Tenant Agent </summary>
        User,
        /// <summary> Tenant </summary>
        Guest
    }
    public enum UserRole
    {
        /// <summary> landlord </summary>
        SuperUser = 1,   
        /// <summary> Listing/Landlord Agent </summary>
        Admin = 2,
        /// <summary> User : Tenant Agent </summary>
        User = 3,
        /// <summary> Tenant </summary>
        Guest = 4,     
    }
    /// ** Policies **
    /// --------------
    ///  SuperUser/Landlord : 
    ///         Can only View the Deal , Not allow to Review/Modify Deal Points
    ///         Can only Speak with Admin/Listing Agent
    ///  --------
    ///  Admin/Listing-Agent : 
    ///         Can create the deal adn after ClientReview it can also be able to Modify the deal points 
    ///         Can only speak with Lanlord & TenantAgent
    ///         While Creating DEAL: Should Know about (SuperUser/Landlord),(User/Tenant Agent) & (Guest/Tenant)
    ///  --------
    ///  User/Tenant Agent: 
    ///         Can review the Deal 
    ///         Can only Speak with (Admin/Listing Agent) & (Guest/Tenant)
    ///         While Creating DEAL: Should Know about (SuperUser/Landlord),(Admin/Listing-Agent) & (Guest/Tenant)
    ///  --------
    /// Guest/Tenant: 
    ///         Can only view the Deal 
    ///         Can only Speak with User/Tenant Agent
    ///  --------
    ///  ********
    ///  

    public enum DealHistory
    {
        DealCreated             = 0,
        DealNewPoints           = 1,
        DealPointsinitialized   = 2,
        DealPointsUpdated       = 3,
        DealPointsReview        = 4,
        DealPointsReviewed      = 5,
        DealPointAcptRejt       = 6,
        DealAmendment           = 7,
        DealAmendmented         = 8,
        DealReviewAmendment     = 9,
        DealComplete            = 10,
        DealCancelled           = 11,
        DealCancelling          = 12,
        DealFinished            = 13,
        joinsentence            = 14,
        ClientComment           = 15
    }
    /// <summary>
    /// Deal History Messages
    /// </summary>
    public enum DealHistoryMsg
    {
        [EnumMember(Value = "A new deal is created by ")]
        DealCreated = DealHistory.DealCreated                                           ,
        [EnumMember(Value = "New deal points created by ")]
        DealNewPoints = DealHistory.DealNewPoints                                       ,
        [EnumMember(Value = "Deal points defined by ")]
        DealPointsinitialized = DealHistory.DealPointsinitialized                       ,
        [EnumMember(Value = "Deal points updated by ")]
        DealPointsUpdated = DealHistory.DealPointsUpdated                               ,
        [EnumMember(Value = "Deal points submitted for review by ")]
        DealPointsReview = DealHistory.DealPointsReview                                 ,
        [EnumMember(Value = "Deal points reviewed by ")]
        DealPointsReviewed = DealHistory.DealPointsReviewed                             ,
        [EnumMember(Value = "Deal point ")]
        DealPointAcptRejt = DealHistory.DealPointAcptRejt                               ,
        [EnumMember(Value = "Deal points submitted for amendment by ")]
        DealAmendment = DealHistory.DealAmendment                                       ,
        [EnumMember(Value = "Deal points updated during amendment by ")]
        DealReviewAmendment = DealHistory.DealReviewAmendment                           ,
        [EnumMember(Value = "Deal points submitted for review after amendment by ")]
        DealAmendmented = DealHistory.DealAmendmented                                   ,
        [EnumMember(Value = "Deal points review is completed by ")]
        DealComplete = DealHistory.DealComplete                                         ,
        [EnumMember(Value = "Deal is cancelled by ")]
        DealCancelled = DealHistory.DealCancelled                                       ,
        [EnumMember(Value = "Deal is submitted to cancel by ")]
        DealCancelling = DealHistory.DealCancelling                                     ,
        [EnumMember(Value = "Deal is closed by ")]
        DealFinished = DealHistory.DealFinished                                         ,
        [EnumMember(Value = " by ")]
        joinsentence = DealHistory.joinsentence                                         ,
        [EnumMember(Value = " got comment(s) ")]
        ClientComment = DealHistory.ClientComment
    }
    public enum EntityNames
    {
            loitemplates            = 0,
            loitemplatepoints       = 1,
            DealPoints              = 3,
            Deals                   = 2
    }
}