﻿using BlazorWebRtc.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc.Domain
{
    public enum Status
    {
        pending = 0,
        accept = 1,
        denied = 2
    }

    public class Request : BaseEntity
    {
        public Status Status { get; set; } = Status.pending;

        [ForeignKey(nameof(SenderUser))]
        public Guid SenderUserId { get; set; }
        [ForeignKey(nameof(ReceiverUser))]
        public Guid ReceiverUserId { get; set; }

        public User SenderUser { get; set; }
        public User ReceiverUser { get; set; }
    }
}





