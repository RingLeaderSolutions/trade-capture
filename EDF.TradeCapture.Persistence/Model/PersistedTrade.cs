using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Automatonymous;

namespace EDF.TradeCapture.Persistence.Model
{
    [Table("QuotesState")]
    public class PersistedTrade: SagaStateMachineInstance
    {
        [Key, Column("CorrelationId")]
        public Guid CorrelationId { get; set; }

        [Column("TradeId")]
        [Required]
        [MaxLength(30)]
        [Index("IX_TradeId", 1, IsUnique = true)]
        public string TradeId { get; set; }

        [Column("State")]
        [Required]
        public string CurrentState { get; set; }

    }
}
