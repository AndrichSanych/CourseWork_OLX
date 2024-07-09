﻿
namespace BusinessLogic.DTOs
{
    public class AdvertDto
    {
        public string UserId { get; set; } = string.Empty;

        public int CityId { get; set; }

        public string CityName { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsNew { get; set; }

        public bool IsVip { get; set; }

        public decimal Price { get; set; }
    }
}
