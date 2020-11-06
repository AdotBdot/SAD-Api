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
		static void Main( string[] args )
		{
			Logger.init( );
			Logger.LogToFile( false );
			Logger.LogTime( true );
			Logger.setLogLevel( Logger.LogLevel.DEBUG );

			SADApi sad = new SADApi( );

			String Login, Psswd;

			Console.WriteLine( "Podaj login: " );
			Login = Console.ReadLine( );
			Console.WriteLine( "Podaj hasło: " );
			Psswd = Console.ReadLine( );

			sad.login( Login, Psswd );

			/*	List<List<SADApi.SubjectSchedule>> subjects = sad.GetSchedule();
				for( int i = 0 ; i < subjects.Count ; i++ )
				{
					for( int j = 0 ; j < subjects[ i ].Count ; j++ )
					{
						Console.WriteLine( subjects[ i ][ j ].Name + " " + subjects[ i ][ j ].Course + " " + subjects[ i ][ j ].Classroom );
					}
				}*/

			Console.WriteLine( "" );

			List<SADApi.SubjectForms> subjects2 = sad.GetSubjectsForms( );

			foreach( SADApi.SubjectForms Sub in subjects2 )
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
