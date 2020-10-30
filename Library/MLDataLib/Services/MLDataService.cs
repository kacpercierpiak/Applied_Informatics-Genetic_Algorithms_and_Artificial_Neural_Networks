using MLDataLib.Repositories;

namespace MLDataLib.Services
{
    public class MLDataService
    {
        public enum MLRepos { Iris, Lab1 };
        private IrisMLRepository _irisMLRepository { get; set; } = new IrisMLRepository();
        private Lab1WTARepository _lab1WTARepository { get; set; } = new Lab1WTARepository();

        public dynamic GetMLData(MLRepos mLRepos)
        {
            return mLRepos switch
            {
                MLRepos.Iris => _irisMLRepository.GetDataFromFiles(),
                MLRepos.Lab1 => _lab1WTARepository.GetDataFromFiles(),
                _ => throw new System.ArgumentException("Can't find data", "mLRepos")
            };
        }
    }
}
