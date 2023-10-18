using Newtonsoft.Json;

namespace AccountPayable.Core.Util
{
	public static class LoggerExtensions
	{
		public static string ToDump(this object obj)
		{
			return JsonConvert.SerializeObject(obj);
        }
	}
}

