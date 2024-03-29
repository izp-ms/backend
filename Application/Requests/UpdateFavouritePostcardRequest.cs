﻿namespace Application.Requests;

public class PostcardIdWithOrderId
{
    public int PostcardId { get; set; }
    public int OrderId { get; set; }
}

public class UpdateFavouritePostcardRequest
{
    public int UserId { get; set; } = 0;
    public IEnumerable<PostcardIdWithOrderId> PostcardIdsWithOrders { get; set; } = new List<PostcardIdWithOrderId>();
}
