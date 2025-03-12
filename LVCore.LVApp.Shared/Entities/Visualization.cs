namespace LVCore.LVApp.Shared.Entities
{
    using LVCore.LVApp.Shared.Attributes;

    [Table("COM_Visualization")]
    public class Visualization
    {
        [Column("vis_ID", true)] // Primary Key
        public int vis_ID { get; set; }

        [Column("vis_Code")]
        public string vis_Code { get; set; }

        [Column("vis_VisualTypeID")]
        public int? vis_VisualTypeID { get; set; }

        [Column("vis_VisualCategoryID")]
        public int? vis_VisualCategoryID { get; set; }

        [Column("vis_ShowTitleLED")]
        public int? vis_ShowTitleLED { get; set; }

        [Column("vis_ShowDetailsLED")]
        public int? vis_ShowDetailsLED { get; set; }

        [Column("vis_ShowActionLED")]
        public int? vis_ShowActionLED { get; set; }

        [Column("vis_ActionID")]
        public int? vis_ActionID { get; set; }

        [Column("vis_DatasourceID")]
        public int? vis_DatasourceID { get; set; }

        [Column("vis_Properties")]
        public string vis_Properties { get; set; }

        [Column("vis_DomainID")]
        public int? vis_DomainID { get; set; }

    }
}