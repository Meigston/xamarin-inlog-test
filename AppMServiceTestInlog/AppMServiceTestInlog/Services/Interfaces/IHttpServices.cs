namespace AppMServiceTestInlog.Services.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IHttpServices
	{
		Task<TModel> Get<TModel>(string controler = "", Dictionary<string, object> queryParams = null);
	}
}
