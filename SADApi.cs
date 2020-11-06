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

		public struct SubjectForms
		{
			public String Name;
			public List<String> Forms;
			public String Points;
		}
		public struct SubjectSchedule
		{
			public String Name;
			public String Course;
			public String Classroom;
		}
		public struct RegisteredAddress
		{
			public String Country;
			public String Province;
			public String District;
			public String Town;
			public String Commune;
			public String Street;
			public String HouseNum;
			public String FlatNum;
			public String PostCode;
		}
		public struct ContactAddress
		{
			public String Country;
			public String Province;
			public String District;
			public String Town;
			public String Commune;
			public String Street;
			public String HouseNum;
			public String FlatNum;
			public String PostCode;
		}
		public struct PersonalData
		{
			public String Name;
			public String OtherNames;
			public String Surname;
			public String FamilyName;
			public String Nick;
			public String EMail;
			public String Sex;

			public String DateofBirth;
			public String Country;
			public String Province;
			public String Town;
		}

		private Logger Lgr;
		private IWebDriver Driver;
		private string SAD_URL = "https://zsa-zgora.sad.edu.pl/";
		private bool LoggedIn = false;
		private Page CurrentPage;

		public SADApi()
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
		private void refresh()
		{
			Driver.Navigate( ).Refresh( );
		}
		private bool isLoggedIn()
		{
			if( LoggedIn == false )
			{
				Lgr.log( Logger.LogLevel.ERROR, "Haven't logged in yet" );
				return false;
			}
			else
				return true;
		}
		private void goToMainPage()
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
			if( LoggedIn )
			{
				Lgr.log( Logger.LogLevel.INFO, "You are already logged In!" );
				return;
			}

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
		public void logout()
		{
			if( !isLoggedIn( ) )
				return;

			IWebElement LogoutButton = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[1]/td[3]/table/tbody/tr/td/table/tbody/tr[2]/td/div/button" ) );
			LogoutButton.Submit( );

			Lgr.log( Logger.LogLevel.INFO, "Logged Out" );

			LoggedIn = false;
			CurrentPage = Page.Login;
		}
		public List<SubjectForms> GetSubjectsForms()
		{
			if( !isLoggedIn( ) )
				return new List<SubjectForms>( );

			Lgr.log( Logger.LogLevel.DEBUG, "Getting Forms..." );

			goTo( Page.Assessments );

			List<SubjectForms> Subjects = new List<SubjectForms>( );

			for( byte i = 2 ; ; i++ )
			{
				SubjectForms subject = new SubjectForms( );

				//Subject Name
				IWebElement ESubjectName = null;
				try
				{
					ESubjectName = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[1]" ) );
				}
				catch( NoSuchElementException e )
				{
					Lgr.log( Logger.LogLevel.DEBUG, "Name: " + e.Message );
					break;
				}
				if( ESubjectName.Text == "" )
					break;

				subject.Name = ESubjectName.Text.Remove( ESubjectName.Text.IndexOf( " (zajęcia obowiązkowe)" ), 22 );

				//Subject Forms
				List<String> SubjectForms = new List<String>( );
				bool NoException = true;
				IWebElement EFirstForm = null;
				try
				{
					EFirstForm = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[2]/table[1]/tbody/tr/td" ) );
				}
				catch( NoSuchElementException )
				{
					try
					{
						EFirstForm = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[2]/table[1]/tbody/tr/td[1]" ) );
					}
					catch( Exception e2 )
					{
						Lgr.log( Logger.LogLevel.DEBUG, "FirstForm: " + e2.Message );
						NoException = false;
					}
				}
				if( NoException == true )
				{
					SubjectForms.Add( EFirstForm.Text );
					String ID = EFirstForm.GetAttribute( "id" ).Remove( 0, EFirstForm.GetAttribute( "id" ).LastIndexOf( "t" ) + 1 );
					String PreId = EFirstForm.GetAttribute( "id" ).Remove( EFirstForm.GetAttribute( "id" ).LastIndexOf( ID ), ID.Length );
					for( int j = Int32.Parse( ID ) + 1 ; ; j++ )
					{
						IWebElement ESubjectForm = null;
						try
						{
							ESubjectForm = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[3]/td/div/div/div[1]/table/tbody/tr[" + i + "]/td[2]/table[*]/tbody/tr/td[@id='" + PreId + j + "']" ) );
						}
						catch( NoSuchElementException e )
						{
							Lgr.log( Logger.LogLevel.DEBUG, "Forms: " + e.Message );
							break;
						}
						SubjectForms.Add( ESubjectForm.Text );
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
		public List<List<SubjectSchedule>> GetSchedule()
		{
			if( !isLoggedIn( ) )
				return new List<List<SubjectSchedule>>( );

			Lgr.log( Logger.LogLevel.DEBUG, "Getting Getting Shedule..." );

			goTo( Page.Schedule );

			List<List<SubjectSchedule>> Schedule = new List<List<SubjectSchedule>>( );

			//sa-i-j
			for( int i = 0 ; i < 5 ; i++ )
			{
				List<SubjectSchedule> Day = new List<SubjectSchedule>( );
				for( int j = 0 ; ; j++ )
				{
					IWebElement ESubject = null;
					try
					{
						ESubject = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td/div/div/table/tbody/tr/td[2]/div/table/tbody/tr[*]/td[@id='sa-" + j + "-" + i + "']/table/tbody/tr/td/div/table/tbody" ) );
					}
					catch( NoSuchElementException )
					{
						try
						{
							ESubject = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td/div/div/table/tbody/tr/td[2]/div/table/tbody/tr[*]/td[@id='sa-" + j + "-" + i + "']/table/tbody/tr/td[1]/div/table/tbody" ) );
						}
						catch( Exception e )
						{
							Lgr.log( Logger.LogLevel.DEBUG, "Shedule: " + e.Message );
							break;
						}
					}

					SubjectSchedule Subject;

					//getting Name, Course, and Classroom

					String Name = ESubject.FindElement( By.XPath( "./tr[1]/td" ) ).Text;
					if( Name.IndexOf( "(" ) > 0 )
						Name = Name.Remove( Name.IndexOf( "(" ), Name.Length - Name.IndexOf( "(" ) );
					if( Name.IndexOf( "." ) > 0 )
						Name = Name.Remove( Name.IndexOf( "." ), Name.Length - Name.IndexOf( "." ) );

					String Course = ESubject.FindElement( By.XPath( "./tr[2]/td" ) ).Text;

					String Classroom = ESubject.FindElement( By.XPath( "./tr[3]/td" ) ).Text;
					if( Classroom.IndexOf( "(" ) > 0 )
						Classroom = Classroom.Remove( Classroom.IndexOf( "(" ), Classroom.Length - Classroom.IndexOf( "(" ) );
					if( Classroom.LastIndexOf( "." ) > 0 )
						Classroom = Classroom.Remove( Classroom.LastIndexOf( "." ) - 2, Classroom.Length - Classroom.LastIndexOf( "." ) + 2 );

					Subject.Name = Name;
					Subject.Course = Course;
					Subject.Classroom = Classroom;

					Day.Add( Subject );

				}
				Schedule.Add( Day );
			}

			return Schedule;
		}
		//TODO: Add exceptions
		public RegisteredAddress GetRegisteredAddress()
		{
			if( !isLoggedIn( ) )
				return new RegisteredAddress( );

			Lgr.log( Logger.LogLevel.DEBUG, "Getting RegisteredAddress..." );

			goTo( Page.Personal );

			RegisteredAddress registeredAddress = new RegisteredAddress( );

			registeredAddress.Country = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[1]/td[2]/div/div/div" ) ).Text;
			registeredAddress.Province = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[2]/td[2]/div/div/div" ) ).Text;
			registeredAddress.District = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[3]/td[2]/div/div/div" ) ).Text;
			registeredAddress.Commune = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[4]/td[2]/div/div/div" ) ).Text;
			registeredAddress.Town = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[5]/td[2]/div/div/div" ) ).Text;
			registeredAddress.Street = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[6]/td[2]/div/div/div" ) ).Text;
			registeredAddress.HouseNum = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[7]/td[2]/div/div/div" ) ).Text;
			registeredAddress.FlatNum = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[8]/td[2]/div/div/div" ) ).Text;
			registeredAddress.PostCode = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[2]/table/tbody/tr[9]/td[2]/div/div/div" ) ).Text;

			return registeredAddress;
		}
		//TODO: Add exceptions
		public ContactAddress GetContactAddress()
		{
			if( !isLoggedIn( ) )
				return new ContactAddress( );

			Lgr.log( Logger.LogLevel.DEBUG, "Getting ContactAddress..." );

			goTo( Page.Personal );

			ContactAddress contactAddress = new ContactAddress( );

			contactAddress.Country = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[1]/td[2]/div/div/div" ) ).Text;
			contactAddress.Province = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[2]/td[2]/div/div/div" ) ).Text;
			contactAddress.District = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[3]/td[2]/div/div/div" ) ).Text;
			contactAddress.Commune = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[4]/td[2]/div/div/div" ) ).Text;
			contactAddress.Town = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[5]/td[2]/div/div/div" ) ).Text;
			contactAddress.Street = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[6]/td[2]/div/div/div" ) ).Text;
			contactAddress.HouseNum = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[7]/td[2]/div/div/div" ) ).Text;
			contactAddress.FlatNum = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[8]/td[2]/div/div/div" ) ).Text;
			contactAddress.PostCode = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[4]/td[1]/table/tbody/tr[9]/td[2]/div/div/div" ) ).Text;

			return contactAddress;
		}
		//TODO: Add exceptions
		public PersonalData GetPersonalData()
		{
			if( !isLoggedIn( ) )
				return new PersonalData( );

			Lgr.log( Logger.LogLevel.DEBUG, "Getting PersonalData..." );

			goTo( Page.Personal );

			PersonalData personalData = new PersonalData( );

			personalData.Name = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[1]/td[2]/div/div/div" ) ).Text;
			personalData.OtherNames = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[2]/td[2]/div/div/div" ) ).Text;
			personalData.Surname = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[3]/td[2]/div/div/div" ) ).Text;
			personalData.FamilyName = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[4]/td[2]/div/div/div" ) ).Text;
			personalData.Nick = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[5]/td[2]/div/div/div" ) ).Text;
			personalData.EMail = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[6]/td[2]/div/div/div" ) ).Text;
			personalData.Sex = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[1]/table/tbody/tr[7]/td[2]/div/div/div" ) ).Text;

			personalData.DateofBirth = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/td[2]/div/div/div" ) ).Text;
			personalData.Country = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[2]/td[2]/div/div/div" ) ).Text;
			personalData.Province = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[3]/td[2]/div/div/div" ) ).Text;
			personalData.Town = Driver.FindElement( By.XPath( "/html/body/table/tbody/tr[1]/td/table/tbody/tr[3]/td/div[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[4]/td[2]/div/div/div" ) ).Text;

			return personalData;
		}
	}
}
