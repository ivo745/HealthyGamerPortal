
namespace HealthyGamerPortal.Common.Models
{
    /// <summary>
    /// Class that holds the results of a status action.
    /// </summary>
    public class StatusResult
    {
        /// <summary>
        /// Indicates the status check of the CRS service.
        /// </summary>
        public string Crs { get; set; }

        /// <summary>
        /// Indicates the status check of the Experian service.
        /// </summary>
        public string Experian { get; set; }

        /// <summary>
        /// Indicates the status check of the BKR service.
        /// </summary>
        public string Bkr { get; set; }

        /// <summary>
        /// Indicates the status check of the users income.
        /// </summary>
        public string Income { get; set; }

        /// <summary>
        /// Indicates if a user is approved for credit.
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Indicates if a user is allowed to see goods.
        /// </summary>
        public bool ShowGoods { get; set; }

        /// <summary>
        /// Indicates if a user is allowed to see their credit.
        /// </summary>
        public bool ShowCredit { get; set; }
    }
}