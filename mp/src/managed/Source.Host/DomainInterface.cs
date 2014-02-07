using System;
using System.Reflection;

using Source.Tier0;

namespace Source.Host
{
	public class DomainInterface : MarshalByRefObject
	{

		public DomainInterface()
		{
			Debug.DevMsg("[Source.Host.exe::DomainInterface] New AppDomain created\n");
			Debug.DevMsg("\tFriendlyName: {0}\n", AppDomain.CurrentDomain.FriendlyName);
			Debug.DevMsg("\tAssembly: {0}\n", Assembly.GetExecutingAssembly().Location);
			Debug.DevMsg("\tBaseDir: {0}\n", AppDomain.CurrentDomain.BaseDirectory);
		}

		~DomainInterface()
		{
			Debug.DevMsg("[Source.Host.exe::DomainInterface] AppDomain destroyed\n");
			Debug.DevMsg("\tFriendlyName: {0}\n", AppDomain.CurrentDomain.FriendlyName);
			Debug.DevMsg("\tAssembly: {0}\n", Assembly.GetExecutingAssembly().Location);
			Debug.DevMsg("\tBaseDir: {0}\n", AppDomain.CurrentDomain.BaseDirectory);
		}
	}
}

