using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers
{
    public sealed class DdContextProvider
    {
        //private static DdContextProvider instance;

        //public static DdContextProvider Instance
        //{
        //    get 
        //    {
        //        if (instance == null)
        //        {
        //            instance = new DdContextProvider();
        //        }

        //        return instance;
        //    }
        //}

        public static Persistence.EF.Context.ApplicationDbContext GetDbContext()
        {
            var optionBuilder = new DbContextOptionsBuilder();

            optionBuilder.UseSqlite(string.Format("Filename={0}", Helpers.LocalFileStorageHelper.GetDatabaseFilePath()));

            return new Persistence.EF.Context.ApplicationDbContext(optionBuilder.Options);
        }

        //private Persistence.EF.Context.ApplicationDbContext dbContext;

        //private DdContextProvider()
        //{
        //}

        //public Persistence.EF.Context.ApplicationDbContext CachedDbContext
        //{
        //    get
        //    {
        //        if (dbContext == null)
        //        {
        //            var optionBuilder = new DbContextOptionsBuilder();

        //            optionBuilder.UseSqlite(string.Format("Filename={0}", Helpers.LocalFileStorageHelper.GetDatabaseFilePath()));

        //            return new Persistence.EF.Context.ApplicationDbContext(optionBuilder.Options);
        //        }

        //        return dbContext;
        //    }
        //}

        //public void ReleaseDbContext()
        //{
        //    dbContext?.Dispose();

        //    dbContext = null;
        //}
    }
}
