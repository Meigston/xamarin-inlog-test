namespace AppXamarinTestThemoviedb.Extensions
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Reflection;

	public static class EnumExtensions
	{
		public static string GetDescription<T>(this T value)
		{
			var enumType = typeof(T);

			foreach (var fieldInfo in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				if (!fieldInfo.Name.Equals(value.ToString()))
				{
					continue;
				}

				var data = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (data.Length == 1)
				{
					var description = ((DescriptionAttribute)data[0]).Description;
					return !string.IsNullOrEmpty(description) ? description : fieldInfo.Name;
				}

				data = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

				if (data.Length == 1)
				{
					var description = ((DisplayAttribute)data[0]).Description;

					return !string.IsNullOrEmpty(description) ? description : fieldInfo.Name;
				}


				return fieldInfo.Name;
			}

			return value.ToString();
		}
	}
}
