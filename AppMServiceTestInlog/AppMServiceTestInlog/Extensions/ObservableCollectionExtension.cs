namespace AppMServiceTestInlog.Extensions
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public static class ObservableCollectionExtension
	{
		public static ObservableCollection<TModel> ToObservableCollection<TModel>(this IEnumerable<TModel> collection)
		{
			var observableColletion = new ObservableCollection<TModel>();

			foreach (var model in collection)
			{
				observableColletion.Add(model);
			}

			return observableColletion;
		}
	}
}
