namespace AppXamarinTestThemoviedb.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;

	using AppXamarinTestThemoviedb.Extensions;
	using AppXamarinTestThemoviedb.Services.Enums;
	using AppXamarinTestThemoviedb.Services.Interfaces;

	using Newtonsoft.Json;

	using Polly;

	public class HttpServices : IHttpServices
	{
		private readonly string baseUrlApi;

		private readonly string mediaTypeHeader;

		public HttpServices(string urlApi, MediaTypeHeader mediaType)
		{
			this.baseUrlApi = urlApi;
			this.mediaTypeHeader = mediaType.GetDescription();

			this.HttpErrorCode = new List<string> { "400", "401", "403", "500" };
		}

		private List<string> HttpErrorCode { get; }

		public async Task<TModel> Get<TModel>(string controller = "", Dictionary<string, object> queryParams = null)
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.mediaTypeHeader));

			var url = new Uri(this.baseUrlApi);

			return await Policy
						.Handle<HttpRequestException>(e => this.HttpErrorCode.Contains(e.Message))
						.WaitAndRetryAsync(
							retryCount: 3,
							sleepDurationProvider: retry => TimeSpan.FromSeconds(Math.Pow(3, retry)),
							onRetry: (e, time) =>
							   {
								   throw e;
							   })
						.ExecuteAsync(
							async () =>
								{
									controller = controller.StartsWith(@"/") ? controller.Substring(1) : controller;
									var urlLast = $"{new Uri(url, controller).AbsoluteUri}?{this.StringJoinQueryParams(queryParams)}";
									var result = await httpClient.GetStringAsync(urlLast);
									return JsonConvert.DeserializeObject<TModel>(result);
								});
		}

		private string StringJoinQueryParams<TKey, TValue>(Dictionary<TKey, TValue> headerParams)
		{
			return headerParams != null ? string.Join("&", headerParams.Select(item => $"{item.Key}={item.Value}")) : string.Empty;
		}
	}
}
