namespace Aloe.Models
{
    using Aloe.Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class DealM
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int PropertyID { get; set; }
        public int TemplateID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(128)]
        public string AgentID { get; set; }
        [Required]
        [StringLength(128)]
        public string TenantID { get; set; }

        [Required]
        [StringLength(128)]
        public string LandlordID { get; set; }
        public string Property { get; set; }
        public int Status { get; set; }
        public DealM() { AgentID = ""; }
        //public List<LOITemplatePointEntity> LOITempPoints { get; set; }
        //public DealM()
        //{
        //    LOITempPoints = new List<LOITemplatePointEntity>();
        //}
    }
    public class DealPointsM 
    {
       
        public int ID { get; set; }
        [StringLength(500)]
        public string DealReview { get; set; }
        public int TemplatePointsID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte Status { get; set; }
    }
    public class LOITemplatePointM : DealPointsM
    {
        public int ID { get; set; }
        public int TempId { get; set; }
        public string DealPoint { get; set; }
        public string InitialValue { get; set; }
        public string DReview { get; set; }
        public int TemplateID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte Status { get; set; }
        public List<LOITemplatePointM> LoiTemPointsM { get; set; }
        public bool CheckAll { get; set; }  /// Selection of all/list
        public string EncrptedTempID { get; set; }
        public Aloe.Helper.Status DpStatus { get; set; }
        public int CDealID { get; set; }
        public bool UserAllow { get; set; }
        public bool ClientT { get; set; }
        public LOITemplatePointM()
        {
            LoiTemPointsM = new List<LOITemplatePointM>();
            DpStatus = Aloe.Helper.Status.Initial;
        }

    }
    public class DealPreviousSelection
    {
        public string Template { get; set; }
        public string CreatedDate { get; set; }
        public string Agent { get; set; }
        public string Landlord { get; set; }
        public string Tenant { get; set; }
        public string Property { get; set; }
    }
    public class DealTitle
    {
        public string TempName { get; set; }
        public string PropertyName { get; set; }
        public string LandLordName { get; set; }
        public string TenantName { get; set; }
        public string ListingAgent { get; set; }
        public string Agent { get; set; }
    }
    public class CancelledDeal 
    {
        public List<DealM> Deals { get; set; }
    }
    public class DealDetail : LOITemplatePointM
    {
        public DealTitle MDetail { get; set; }
        public DealDetail()
        { MDetail = new DealTitle(); }
    }
    public class DealMessageM
    {
        public int ID { get; set; }
        public int DealID { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string Receiver { get; set; }
        public string ReceiverName { get; set; }
        public string Message { get; set; }
        public DMsgStatus Status { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ModifyDate { get; set; }
        
    }
    public class DealHistoryM
    {
        public int DHid { get; set; }
        public string DHMsg { get; set; }
        public DateTime DHLogDate { get; set; }
    }

}
