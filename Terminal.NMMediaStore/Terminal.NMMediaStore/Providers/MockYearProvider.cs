namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers
{
    public class MockYearProvider : Facade.IYearProviderFacade
    {
        public async System.Threading.Tasks.Task<int[]> GetRelevantAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        public async System.Threading.Tasks.Task<int[]> GetAllAsync()
        {
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

            return BuildMockList();
        }

        private int[] BuildMockList()
        {
            return new int[]
            {
                2020, 2019, 2018, 2017, 2016, 2015, 2013, 2010, 2009, 2007, 2005, 2004, 2000, 1999, 1998, 1991, 1987, 1974, 1970, 1969, 1963, 1962, 1960, 1959
            };
        }
    }
}
