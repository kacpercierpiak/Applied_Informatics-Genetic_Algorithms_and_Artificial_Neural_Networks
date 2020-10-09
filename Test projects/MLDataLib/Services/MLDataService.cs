using MLDataLib.Repositories;

namespace MLDataLib.Services
{
    public class MLDataService
    {
        public enum MLRepos { Iris };
        private IrisMLRepository _irisMLRepository { get; set; } = new IrisMLRepository();
       
        public dynamic GetMLData(MLRepos mLRepos)
        {
            return mLRepos switch
            {
                MLRepos.Iris => _irisMLRepository.GetDataFromFiles(),
                _ => throw new System.ArgumentException("Can't find data", "mLRepos")
            };
        }
    }
}
