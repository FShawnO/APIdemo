﻿namespace APIdemo.Models.DTO
{
    public class SpotsPagingDTO
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<SpotImagesSpot>? SpotsResult
        {
            get; set;
        }
    }
}
