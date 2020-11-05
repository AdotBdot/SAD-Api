using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD
{
	class Program
	{
		static void Main( string[ ] args )
		{
			Logger.init( );
			Logger.LogToFile( false );
			Logger.LogTime( true );
			Logger.setLogLevel( Logger.LogLevel.DEBUG );

			SADApi sad = new SADApi( );

			/*String Login, Psswd;

			Console.WriteLine( "Podaj login: " );
			Login = Console.ReadLine( );
			Console.WriteLine( "Podaj hasło: " );
			Psswd = Console.ReadLine( );

			sad.login( Login, Psswd );*/

			sad.login( "abuchowski", "Airbusa380a" );

			List<SADApi.Subject> subjects = sad.GetSubjects( );

			foreach( SADApi.Subject Sub in subjects )
			{
				String output;
				output = Sub.Name + " ";
				foreach( String s in Sub.Forms )
				{
					output += "[" + s + "] ";
				}
				output += Sub.Points;

				Console.WriteLine( output );
			}

			Console.ReadKey( );
		}
	}

}
