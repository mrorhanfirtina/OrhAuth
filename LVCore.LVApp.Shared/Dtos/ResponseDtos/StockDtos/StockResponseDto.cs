namespace LVCore.LVApp.Shared.Dtos.ResponseDtos.StockDtos
{
    public class StockResponseDto
    {
        public int? LocationID { get; set; }
        public int? ProductID { get; set; }
        public int? DepositorID { get; set; }
        public int? ContainerID { get; set; }
        public int? CUItemUnitID { get; set; }
        public decimal? CUQuantity { get; set; }
        public decimal? CUQuantityFree { get; set; }
        public int? UnsuitReasonID { get; set; }
        public int? ReserveReasonID { get; set; }
        public int LogisticUnitID { get; set; } // NOT NULL
        public int? DomainID { get; set; }
    }
}
