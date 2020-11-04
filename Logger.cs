using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD
{
	class Logger
	{
		public enum LogLevel
		{
			NO = 0,
			ERROR = 1,
			WARNING = 2,
			INFO = 3,
			DEBUG = 4
		}

		private static String FilePath;
		private static LogLevel _LogLevel;
		private static bool _LogToFile;
		private static bool _LogTime;

		private static void s_log( LogLevel MsgLevel, String ObjName, String Message )
		{
			if( MsgLevel == LogLevel.NO )
				return;

			if( _LogLevel < MsgLevel )
				return;

			String output = "";

			if( _LogTime )
			{
				DateTime Time = System.DateTime.Now;
				output = "<" + Time.Hour + ":" + Time.Minute + ":" + Time.Second + "> ";
			}

			output += s_getPrefix( MsgLevel ) + " <" + ObjName + "> " + Message;

			Console.ForegroundColor = s_getColor( MsgLevel );
			Console.WriteLine( output );
			Console.ForegroundColor = ConsoleColor.Gray;

			if( _LogToFile )
			{
				s_logToFile( output );
			}

		}
		private static void s_logToFile( String Message )
		{
			File.AppendAllText( FilePath, Message );
		}
		private static String s_getPrefix( LogLevel MsgLevel )
		{
			switch( MsgLevel )
			{
				case LogLevel.NO:
					return "";
					break;
				case LogLevel.ERROR:
					return "<ERROR>";
					break;
				case LogLevel.WARNING:
					return "<WARNING>";
					break;
				case LogLevel.INFO:
					return "<INFO>";
					break;
				case LogLevel.DEBUG:
					return "<DEBUG>";
					break;
				default:
					return "";
					break;
			}
		}

		private static ConsoleColor s_getColor( LogLevel MsgLevel )
		{
			switch( MsgLevel )
			{
				case LogLevel.NO:
					return ConsoleColor.White;
					break;
				case LogLevel.ERROR:
					return ConsoleColor.Red;
					break;
				case LogLevel.WARNING:
					return ConsoleColor.Yellow;
					break;
				case LogLevel.INFO:
					return ConsoleColor.White;
					break;
				case LogLevel.DEBUG:
					return ConsoleColor.Blue;
					break;
				default:
					return ConsoleColor.White;
					break;
			}
		}

		public static void init( )
		{
			DateTime DateDT = DateTime.Now;
			String Date;
			Date = DateDT.Day.ToString( ) + "-" + DateDT.Month.ToString( ) + "-" + DateDT.Year.ToString( );

			for( int i = 1 ; ; i++ )
			{
				FilePath = "logs/" + Date + "-" + i.ToString( ) + ".txt";

				if( File.Exists( FilePath ) )
					continue;
				else
					break;
			}
		}

		public static void setLogLevel( LogLevel logLevel )
		{
			_LogLevel = logLevel;
		}

		public static void LogTime( bool Is )
		{
			_LogTime = Is;
		}

		public static void LogToFile( bool Is )
		{
			_LogToFile = Is;
		}

		public static Logger getInstance( String ObjName )
		{
			return new Logger( ObjName );
		}

		public static void TestLogger( )
		{
			s_log( Logger.LogLevel.NO, "Logger", "NO" );
			s_log( Logger.LogLevel.ERROR, "Logger", "ERROR" );
			s_log( Logger.LogLevel.WARNING, "Logger", "WARNING" );
			s_log( Logger.LogLevel.INFO, "Logger", "INFO" );
			s_log( Logger.LogLevel.DEBUG, "Logger", "DEBUG" );
		}

		private String ObjectName;

		public Logger( String ObjName )
		{
			ObjectName = ObjName;
		}

		public void log( LogLevel MsgLevel, String Message )
		{
			s_log( MsgLevel, ObjectName, Message );
		}


	}
}
