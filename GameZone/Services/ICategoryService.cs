﻿namespace GameZone.Services
{
    public interface ICategoryService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
