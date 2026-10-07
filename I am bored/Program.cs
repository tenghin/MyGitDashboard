using I_am_bored;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Xml.Schema;

class Run
{
	String SystemVersion = "1.00 Alpha";

	static void Main(string[] args)
	{

        DotNetEnv.Env.Load();

        var fileSystem = new FileSystem();

        bool Booting = true;
		while (Booting == true)
		{
			Console.Write("\rBooting...   ");
			Thread.Sleep(1000);
			Console.Write("\rBooting....  ");
			Thread.Sleep(1000);
			Console.Write("\rBooting..... ");
			Thread.Sleep(1000);
			Console.Write("\rBooting......");
			Thread.Sleep(1000);

			Booting = false;
		}

		Console.WriteLine("\rWelcome to the System");

        string rootPath = Path.GetFullPath(@"Storage C");

        Global.RootPath = rootPath;

        bool authorized = false;
		bool value = false;
		bool loop = true;

		while (loop)
		{
			while (authorized == false)
			{
				Login login = new Login(rootPath);

				User? user = login.UserPrompt();

				if (user != null)
				{
                    Global.CurrentUser = user;

                    authorized = true;
					break;
				}
			}
			
			//to run most commands
			Shell shell = new Shell();
			shell.Run();

			//to logout by changing to loop again or not
            if (Global.CurrentUser == null)
            {
                authorized = false;
                Console.WriteLine();
                continue;
            }

            loop = Global.Exit(value);

        }
    }

}

