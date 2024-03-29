﻿using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public interface IUniversityRawDataRepository
    {
        public DbSet<UniversityRawData> UniversityRawDatas();
        public void AddToContext(UniversityRawData universityRawData);
        public void AddToContext(List<UniversityRawData> universityRawDatas);
    }
}
