using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SAD
{
	class SADApi
	{
		enum Page
		{
			Login,
			//jak się zalogujesz to masz 3 guziki
			Main,
			Messages,
			Options,
			//jak wejdziesz w ucznia
			About,
			Schedule,
			Assessments,
			Frequency,
			Personal
		}

		public struct Subject
		{
			public String Name;
			public List<String> Forms;
			public String Points;
		}

		private Logger Lgr;
		private IWebDriver Driver;
		private string SAD_URL = "https://zsa-zgora.sad.edu.pl/";
		private bool LoggedIn = false;
		private Page CurrentPage;

		public SADApi( )
		{
			Lgr = new Logger( "SADAPI" );

			//init Browser Options
			FirefoxOptions options = new FirefoxOptions( );
			options.BrowserExecutableLocation = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
			options.AddArgument( "--headless" );

			//init driver
			try
			{
				Driver = new FirefoxDriver( "./", options );
				Driver.Url = SAD_URL;
			}
			catch( Exception e )
			{
				Lgr.log( Logger.LogLevel.ERROR, e.Message );
				return;
			}

			CurrentPage = Page.Login;
		}

		private void waitForPageLoad( String InitialUrl )
		{
			while( Driver.Url == InitialUrl )
			{
				System.Threading.Thread.Sleep( 200 );
			}
		}
		private void refresh( )
		{
			Driver.Navigate( ).Refresh( );
		}
		private bool isLoggedIn( )
		{
			if( LoggedIn == false )
			{
				Lgr.log( Logger.LogLevel.ERROR, "Haven't logged in yet" );
				return false;
			}
			else
				return true;
		}
		private void goToMainPage( )
		{
			if( !isLoggedIn( ) )
				return;

			if( CurrentPage == Page.Main )
				return;

			String InitUrl = Driver.Url;
			IWebElement SADButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr/td[1]/div/a" ) );
			SADButton.Click( );
			waitForPageLoad( InitUrl );
			Lgr.log( Logger.LogLevel.DEBUG, "Moved to Main page" );
		}
		private void goTo( Page page )
		{
			if( !isLoggedIn( ) )
				return;

			String InitUrl = Driver.Url;
			if( page == CurrentPage )
				return;

			goToMainPage( );

			switch( page )
			{
				case Page.Messages:
				{
					IWebElement MessagesButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[1]/div[1]/ul/li[2]/a" ) );
					MessagesButton.Click( );

					CurrentPage = Page.Messages;
				}
				break;
				case Page.Options:
				{
					IWebElement MessagesButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[1]/div[1]/ul/li[3]/a" ) );
					MessagesButton.Click( );

					CurrentPage = Page.Options;
				}
				break;
				case Page.About:
				{
					IWebElement UczenButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr/td[2]/table/tbody/tr[2]/td/div/button" ) );
					UczenButton.Submit( );

					CurrentPage = Page.About;
				}
				break;
				case Page.Schedule:
				{
					IWebElement UczenButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr/td[2]/table/tbody/tr[2]/td/div/button" ) );
					UczenButton.Submit( );
					waitForPageLoad( InitUrl );
					InitUrl = Driver.Url;
					IWebElement ScheduleButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[1]/div[1]/ul/li[3]/a" ) );
					ScheduleButton.Click( );

					CurrentPage = Page.Schedule;
				}
				break;
				case Page.Assessments:
				{
					IWebElement UczenButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr/td[2]/table/tbody/tr[2]/td/div/button" ) );
					UczenButton.Submit( );
					waitForPageLoad( InitUrl );
					InitUrl = Driver.Url;
					IWebElement AssessmetnsButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[1]/div[1]/ul/li[4]/a" ) );
					AssessmetnsButton.Click( );

					CurrentPage = Page.Assessments;
				}
				break;
				case Page.Frequency:
				{
					IWebElement UczenButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr/td[2]/table/tbody/tr[2]/td/div/button" ) );
					UczenButton.Submit( );
					waitForPageLoad( InitUrl );
					InitUrl = Driver.Url;
					IWebElement FrequencyButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[1]/div[1]/ul/li[5]/a" ) );
					FrequencyButton.Click( );

					CurrentPage = Page.Frequency;
				}
				break;
				case Page.Personal:
				{
					IWebElement UczenButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr/td[2]/table/tbody/tr[2]/td/div/button" ) );
					UczenButton.Submit( );
					waitForPageLoad( InitUrl );
					InitUrl = Driver.Url;
					IWebElement PersonalButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[1]/div[1]/ul/li[13]/a" ) );
					PersonalButton.Click( );

					CurrentPage = Page.Personal;
				}
				break;

			}
			waitForPageLoad( InitUrl );
			Lgr.log( Logger.LogLevel.DEBUG, "Moved to: " + CurrentPage.ToString( ) );
		}
		public void login( String Login, String Password )
		{
			String InitUrl = Driver.Url;

			IWebElement LoginInput = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr/td/table/tbody/tr[3]/td/table[2]/tbody/tr[1]/td[2]/div/input" ) );
			IWebElement PasswordInput = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr/td/table/tbody/tr[3]/td/table[2]/tbody/tr[2]/td[2]/div/input" ) );

			LoginInput.SendKeys( Login );
			PasswordInput.SendKeys( Password );

			IWebElement LoginButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr/td/div/button" ) );
			LoginButton.Submit( );

			waitForPageLoad( InitUrl );

			Lgr.log( Logger.LogLevel.INFO, "Logged In" );

			LoggedIn = true;
			CurrentPage = Page.Main;
		}
		public void logout( )
		{
			if( !isLoggedIn( ) )
				return;

			IWebElement LogoutButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[1]/td[3]/table/tbody/tr/td/table/tbody/tr[2]/td/div/button" ) );
			LogoutButton.Submit( );

			Lgr.log( Logger.LogLevel.INFO, "Logged Out" );

			LoggedIn = false;
			CurrentPage = Page.Login;
		}
		public List<Subject> GetSubjects( )
		{
			if( !isLoggedIn( ) )
				return new List<Subject>( );

			goTo( Page.Assessments );
			List<Subject> Subjects = new List<Subject>( );
			for( byte i = 2 ; ; i++ )
			{
				Subject subject = new Subject( );

				//Subject Name
				IWebElement SubjectName;
				try
				{
					SubjectName = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[1]" ) );
				}
				catch( NoSuchElementException e )
				{
					Lgr.log( Logger.LogLevel.DEBUG, "Name: " + e.Message );
					break;
				}
				if( SubjectName.Text == "" )
					break;

				subject.Name = SubjectName.Text.Remove( SubjectName.Text.IndexOf( " (zajęcia obowiązkowe)" ), 22 );

				//Subject Forms
				List<String> SubjectForms = new List<String>( );
				bool NoException = true;
				IWebElement FirstForm = null;
				try
				{
					FirstForm = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[2]/table[1]/tbody/tr/td" ) );
				}
				catch( NoSuchElementException e )
				{
					try
					{
						FirstForm = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[2]/table[1]/tbody/tr/td[1]" ) );
					}
					catch( Exception e2 )
					{
						Lgr.log( Logger.LogLevel.DEBUG, "FirstForm: " + e2.Message );
						NoException = false;
					}
				}
				if( NoException == true )
				{
					SubjectForms.Add( FirstForm.Text );
					String ID = FirstForm.GetAttribute( "id" ).Remove( 0, FirstForm.GetAttribute( "id" ).LastIndexOf( "t" ) + 1 );
					String PreId = FirstForm.GetAttribute( "id" ).Remove( FirstForm.GetAttribute( "id" ).LastIndexOf( ID ), ID.Length );
					for( int j = Int32.Parse( ID ) + 1 ; ; j++ )
					{
						IWebElement SubjectForm;
						try
						{
							SubjectForm = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[2]/table[*]/tbody/tr/td[@id='" + PreId + j + "']" ) );
						}
						catch( NoSuchElementException e )
						{
							Lgr.log( Logger.LogLevel.DEBUG, "Forms: " + e.Message );
							break;
						}
						SubjectForms.Add( SubjectForm.Text );
					}
				}
				subject.Forms = SubjectForms;

				//Subject Points
				IWebElement SubjectPoints = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[3]" ) );
				subject.Points = SubjectPoints.Text;

				Subjects.Add( subject );
			}
			Lgr.log( Logger.LogLevel.DEBUG, "Getting subjects finished" );
			return Subjects;
		}

	}
}
