using System;
using System.Collections.Generic;
namespace Aloe.Models
{
  
    public class LOITemplateEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<LOITemplatePointEntity> LOITempPoints { get; set; }
        public string Question { get; set; }
        public LOITemplateEntity()
        {
            LOITempPoints = new List<LOITemplatePointEntity>();
        }
    }


    public class LOITemplatePointEntity
    {
        public int ID { get; set; }
        public string DealPoint { get; set; }
        public string InitialValue { get; set; }
        public int TemplateID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte Status { get; set; }

    }







}
